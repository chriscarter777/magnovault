using AuthorizeNet.Api.Contracts.V1;
using magnovault.Authorize.Net;
using magnovault.Models;
using magnovault.Data;
using magnovault.Site.ViewModels;
using magnovault.Utilities;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.IO;
using System.Web.Mvc;
using System.Threading.Tasks;

namespace magnovault.Site.Controllers
{
     public class HomeController : Controller
     {
          private HomeViewModel model = new HomeViewModel();
          private MagnovaultDbRepository repository = new MagnovaultDbRepository();
          private string authorizeNetApiLoginID = ConfigurationManager.AppSettings["authorizeNetApiLoginID"];
          private string authorizeNetApiTransactionKey = ConfigurationManager.AppSettings["authorizeNetApiTransactionKey"];
          private string currentUserId;

          public async Task<ActionResult> Index()
          {
               BusinessRules busRules = await repository.GetBusinessRulesAsync();
               model.Products = await repository.GetProductsAsync(true);
               model.ProductCount = model.Products.Length;
               model.DeliveryTime = busRules.DeliveryTime;
               if (User.Identity.IsAuthenticated)
               {
                    currentUserId = User.Identity.GetUserId();
                    model.CurrentCustomer = await repository.GetCustomerAsync(currentUserId);
                    model.InvestorContact = new InvestorContact(model.CurrentCustomer);
                    model.MediaContact = new MediaContact(model.CurrentCustomer);
                    model.PublicContact = new PublicContact(model.CurrentCustomer);
                    model.RetailContact = new RetailContact(model.CurrentCustomer);
                    model.VideoLink = ConfigurationManager.AppSettings["VideoLink"];
                    ViewOrder deferredOrder = await repository.GetDeferredOrderAsync(currentUserId);
                    if (deferredOrder == null)
                    {
                         model.Order = new ViewOrder(busRules, model.CurrentCustomer, model.Products);
                    }
                    else
                    {
                         model.Order = deferredOrder;
                    }
                    model.CreditCard = new CreditCard(model.CurrentCustomer);
               }
               else
               {
                    currentUserId = null;
                    model.CurrentCustomer = null;
                    model.InvestorContact = new InvestorContact();
                    model.MediaContact = new MediaContact();
                    model.PublicContact = new PublicContact();
                    model.RetailContact = new RetailContact();
                    model.Order = new ViewOrder(busRules, model.Products);
               }
               return View("Index", model);
          }

          #region Order
          //this method is an AJAX target for CalculateOrder
          public async Task<JsonResult> CalculateOrderAsync()
          {
               Stream req = Request.InputStream;
               req.Seek(0, SeekOrigin.Begin);
               string payload = new StreamReader(req).ReadToEnd();
               AjaxOrder requestOrder = JsonConvert.DeserializeObject<AjaxOrder>(payload);

               BusinessRules busRules = await repository.GetBusinessRulesAsync();
               TaxRate[] taxRates = await repository.GetTaxRatesAsync();

               AjaxOrder responseOrder = OrderCalculator.CalculateOrder(requestOrder, busRules, taxRates);

               JsonResult response = Json(responseOrder);
               response.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
               return response;
          }  //CalculateOrderAsync

          [HttpPost]
          public string SubmitOrder(HomeViewModel model)
          {
               //submit the order to processor
               ChargeCreditCardClient authnetChargeClient = new ChargeCreditCardClient();

               creditCardType creditCard = new creditCardType
               {
                    cardNumber = model.CreditCard.CardNumber,
                    expirationDate = String.Format("{0:00}", model.CreditCard.ExpMonth) + String.Format("{0:00}", model.CreditCard.ExpYear),
                    cardCode = model.CreditCard.CardCode
               };

               customerAddressType billingAddress = new customerAddressType
               {
                    firstName = model.CreditCard.BillFirstName,
                    lastName = model.CreditCard.BillLastName,
                    address = model.CreditCard.BillStreet,
                    city = model.CreditCard.BillCity,
                    state = model.CreditCard.BillState,
                    zip = model.CreditCard.BillZip
               };

               int linecount = model.Order.Items.Count;
               lineItemType[] lineItems = new lineItemType[linecount + 3];
               for (int i = 0; i < linecount; i++)
               {
                    lineItems[i] = new lineItemType { itemId = i.ToString(), name = model.Order.Items[i].ProductName + ", " + model.Order.Items[i].ProductShortDescr, quantity = model.Order.Items[i].Quantity, unitPrice = model.Order.Items[i].UnitPrice };
               }
               lineItems[linecount] = new lineItemType { itemId = linecount.ToString(), name = "Tax", quantity = 1m, unitPrice = model.Order.Tax };
               lineItems[linecount + 1] = new lineItemType { itemId = (linecount + 1).ToString(), name = "Shipping and Handling", quantity = 1m, unitPrice = model.Order.ShipHand };
               lineItems[linecount + 2] = new lineItemType { itemId = (linecount + 2).ToString(), name = "Adjustment", quantity = 1m, unitPrice = model.Order.Adjustment };

               string authorizeNetApiLoginID = ConfigurationManager.AppSettings["authorizeNetApiLoginID"];
               string authorizeNetApiTransactionKey = ConfigurationManager.AppSettings["authorizeNetApiTransactionKey"];

               //Debug.WriteLine("If this were real, your charge would be processed HERE");
               //return "HI!";
               createTransactionResponse response = authnetChargeClient.RunCharge(authorizeNetApiLoginID, authorizeNetApiTransactionKey, creditCard, billingAddress, lineItems, model.Order.Total);

               model.ResponseCode = response.transactionResponse.responseCode;
               model.TransactionId = response.transactionResponse.transId;
               model.AuthorizationCode = response.transactionResponse.authCode;

               //save the order in the Db
               model.Order.CardLastFour = model.CreditCard.CardNumber.Substring(model.CreditCard.CardNumber.Length - 4);
               model.Order.TransactionId = model.TransactionId;
               model.Order.ResponseCode = model.ResponseCode;
               model.Order.AuthorizationCode = model.AuthorizationCode;
               repository.AddOrderAsync(model.Order, false);

               string subject = "New MagnoVault order";
               string content = "<p>A new order was submitted:</p><p>" + model.Order.ShipFirstName + " " + model.Order.ShipLastName + "</p><p> + " + linecount + " line items totalling $" + model.Order.Total + "</p>";
               SendEmail(subject, content);

               //return the response from the gateway
               return model.ResponseCode;
          }
          #endregion

          #region InvestorContact
          public string SubmitInvestorContact(InvestorContact contact)
          {
               repository.AddInvestorContactAsync(contact, false);
               string subject = "New MagnoVault order";
               string content = "<p>A new investor contact was submitted:</p><p>" + contact.FirstName + " " + contact.LastName + "</p><p> + " + contact.Title + ", " + contact.Organization + "(" + contact.InvestType + ")" + "</p><p>" + contact.City + ", " + contact.State + "</p><p>" + contact.Phone + "</p><p>" + contact.Email + "</p><p>Prefers: " + contact.BestMethod + "</p>";
               SendEmail(subject, content);
               return "Thank you for your interest in MagnoVault.  Your request has been submitted, and we will be pleased to get back to you shortly!";
          }
          #endregion

          #region MediaContact
          public string SubmitMediaContact(MediaContact contact)
          {
               repository.AddMediaContactAsync(contact, false);
               string subject = "New MagnoVault order";
               string content = "<p>A new media contact was submitted:</p><p>" + contact.FirstName + " " + contact.LastName + "</p><p> + " + contact.Title + ", " + contact.Organization+ "</p><p>" + contact.City + ", " + contact.State + "</p><p>" + contact.Phone + "</p><p>" + contact.Email + "</p><p>Prefers: " + contact.BestMethod + "</p><p>" + contact.Message + "</p>";
               SendEmail(subject, content);
               return "Thank you for your interest in MagnoVault.  Your request has been submitted, and we will be pleased to get back to you shortly!";
          }
          #endregion

          #region PublicContact
          public string SubmitPublicContact(PublicContact contact)
          {
               repository.AddPublicContactAsync(contact, false);
               string subject = "New MagnoVault order";
               string content = "<p>A new public contact was submitted:</p><p>" + contact.FirstName + " " + contact.LastName + "</p><p>" + contact.City + ", " + contact.State + "</p><p>" + contact.Phone + "</p><p>" + contact.Email + "</p><p>Prefers: " + contact.BestMethod + "</p><p>" + contact.Message + "</p>";
               SendEmail(subject, content);
               return "Thank you for your interest in MagnoVault.  Your request has been submitted, and we will be pleased to get back to you shortly!";
          }
          #endregion

          #region RetailContact
          public string SubmitRetailContact(RetailContact contact)
          {
               repository.AddRetailContactAsync(contact, false);
               string subject = "New MagnoVault order";
               string content = "<p>A new retail contact was submitted:</p><p>" + contact.FirstName + " " + contact.LastName + "</p><p> + " + contact.Title + ", " + contact.OrgName + "(" + contact.OrgType + ": " + contact.OrgWebsite + ")" + "</p><p>" + contact.City + ", " + contact.State + "</p><p>" + contact.Phone + "</p><p>" + contact.Email + "</p><p>Prefers: " + contact.BestMethod + "</p><p>" + contact.Message + "</p>";
               SendEmail(subject, content);
               Emailer mailer = new Emailer();
               return "Thank you for your interest in MagnoVault.  Your request has been submitted, and we will be pleased to get back to you shortly!";
          }
          #endregion

          private void SendEmail (string subject, string content)
          {
               Emailer mailer = new Emailer();
               string mailhost = ConfigurationManager.AppSettings["emailHost"];
               int mailport = Int32.Parse(ConfigurationManager.AppSettings["emailPort"]);
               string mailpassword = ConfigurationManager.AppSettings["emailPassword"];
               string mailfrom = ConfigurationManager.AppSettings["emailFrom"];
               string mailto = ConfigurationManager.AppSettings["emailTo"];
               mailer.SendMail(mailhost, mailport, mailpassword, mailfrom, mailto, subject, content);
          }

     }  //controller
}  //namespace