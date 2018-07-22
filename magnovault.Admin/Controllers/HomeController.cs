using AuthorizeNet.Api.Contracts.V1;
using magnovault.Admin.ViewModels;
using magnovault.Authorize.Net;
using magnovault.Data;
using magnovault.Models;
using magnovault.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace magnovault.Admin.Controllers
{
     [Authorize]
     public class HomeController : Controller
     {
          private HomeViewModel model = new HomeViewModel();
          private MagnovaultDbRepository repository = new MagnovaultDbRepository();
          private string authorizeNetApiLoginID = ConfigurationManager.AppSettings["authorizeNetApiLoginID"];
          private string authorizeNetApiTransactionKey = ConfigurationManager.AppSettings["authorizeNetApiTransactionKey"];

          public ActionResult Index()
          {
               return View(model);
          }

          public ActionResult Documentation()
          {
               return View(model);
          }

          #region BusinessRules
          [HttpGet]
          public async Task<ActionResult> BusinessRules()
          {
               model.BusinessRules = await repository.GetBusinessRulesAsync();
               return View("BusinessRules", model);
          }

          [HttpPost]
          public async Task<ActionResult> BusinessRules(HomeViewModel model, int id)
          {
               model.BusinessRules = await repository.UpdateBusinessRulesAsync(model.BusinessRules, true);
               return View("BusinessRules", model);
          }
          #endregion

          #region Customers
          [HttpGet]
          public async Task<ActionResult> Customers()
          {
               model.Customers = await repository.GetCustomersAsync();
               return View("Customers", model);
          }

          [HttpPost]
          public async Task<ActionResult> Customers(HomeViewModel model, int id)
          {
               model.Customers = await repository.UpdateCustomerAsync(model.Customers[id], true);
               return View("Customers", model);
          }

          public async Task<ActionResult> DeleteCustomer(string id)
          {
               AccountController ac = new AccountController();
               ac.DeleteUserAsync(id);
               model.Customers = await repository.GetCustomersAsync();
               return View("Customers", model);
          }

          //this method is an AJAX endpoint for Add/Edit Order
          public async Task<JsonResult> GetCustomer(string id)
          {
               Customer customer = await repository.GetCustomerAsync(id);
               JsonResult response = Json(customer);
               response.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
               return response;
          }

          private async Task<List<SelectListItem>> MakeCustomerSelectList()
          {
               List<SelectListItem> customerSelectList = new List<SelectListItem>();
               Customer[] customers = await repository.GetCustomersAsync();
               foreach (Customer c in customers)
               {
                    string custDetails = c.LastName + ", " + c.FirstName + ":  " + c.Street + ", " + c.City + ", " + c.State;
                    customerSelectList.Add(new SelectListItem { Value = c.UserId, Text = custDetails });
               }
               return customerSelectList;
          }  //MakeCustomerSelectList
          #endregion

          #region InvestorContacts
          [HttpGet]
          public async Task<ActionResult> InvestorContacts()
          {
               model.InvestorContacts = await repository.GetInvestorContactsAsync();
               return View("InvestorContacts", model);
          }

          [HttpPost]
          public async Task<ActionResult> InvestorContacts(HomeViewModel model, int id)
          {
               model.InvestorContacts = await repository.UpdateInvestorContactAsync(model.InvestorContacts[id], true);
               return View("InvestorContacts", model);
          }

          [HttpGet]
          public ActionResult AddInvestorContact()
          {
               return View("AddInvestorContact", model);
          }

          [HttpPost]
          public async Task<ActionResult> AddInvestorContact(HomeViewModel model)
          {
               model.InvestorContacts = await repository.AddInvestorContactAsync(model.NewInvestorContact, true);
               return View("InvestorContacts", model);
          }

          public async Task<ActionResult> DeleteInvestorContact(int id)
          {
               model.InvestorContacts = await repository.DeleteInvestorContactAsync(id, true);
               return View("InvestorContacts", model);
          }
          #endregion

          #region MediaContacts
          [HttpGet]
          public async Task<ActionResult> MediaContacts()
          {
               model.MediaContacts = await repository.GetMediaContactsAsync();
               return View("MediaContacts", model);
          }

          [HttpPost]
          public async Task<ActionResult> MediaContacts(HomeViewModel model, int id)
          {
               model.MediaContacts = await repository.UpdateMediaContactAsync(model.MediaContacts[id], true);
               return View("MediaContacts", model);
          }

          [HttpGet]
          public ActionResult AddMediaContact()
          {
               return View("AddMediaContact", model);
          }

          [HttpPost]
          public async Task<ActionResult> AddMediaContact(HomeViewModel model)
          {
               model.MediaContacts = await repository.AddMediaContactAsync(model.NewMediaContact, true);
               return View("MediaContacts", model);
          }
          public async Task<ActionResult> DeleteMediaContact(int id)
          {
               model.MediaContacts = await repository.DeleteMediaContactAsync(id, true);
               return View("MediaContacts", model);
          }

          #endregion

          #region Orders
          [HttpGet]
          public async Task<ActionResult> Orders()
          {
               model.Orders = await repository.GetOrdersAsync();
               return View("Orders", model);
          }

          [HttpPost]
          public async Task<ActionResult> Orders(HomeViewModel model, int id)
          {
               model.EditOrder = await repository.GetOrderAsync(id);
               model.CustomerList = await MakeCustomerSelectList();
               return View("EditOrder", model);
          }

          [HttpPost]
          public async Task<ActionResult> EditOrder(HomeViewModel model, int id)
          {
               //save the updated order
               model.Orders = await repository.UpdateOrderAsync(model.EditOrder, true);
               return View("Orders", model);
          }

          [HttpGet]
          public async Task<ActionResult> AddOrder()
          {
               model.CustomerList = await MakeCustomerSelectList();
               model.Products = await repository.GetProductsAsync(true);
               model.EditOrder = await CreateNewOrder(model.Products);
               model.CreditCard = new CreditCard();
               return View("AddOrder", model);
          }

          [HttpPost]
          public async Task<ActionResult> AddOrder(HomeViewModel model)
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
                    zip = model.CreditCard.BillZip
               };

               lineItemType[] lineItems = new lineItemType[model.EditOrder.Items.Count + 3];
               for (int i = 0; i < model.EditOrder.Items.Count; i++)
               {
                    lineItems[i] = new lineItemType { itemId = i.ToString(), name = model.EditOrder.Items[i].ProductName + ", " + model.EditOrder.Items[i].ProductShortDescr, quantity = model.EditOrder.Items[i].Quantity, unitPrice = model.EditOrder.Items[i].UnitPrice };
               }
               lineItems[model.EditOrder.Items.Count] = new lineItemType { itemId = model.EditOrder.Items.Count.ToString(), name = "Tax", quantity = 1m, unitPrice = model.EditOrder.Tax };
               lineItems[model.EditOrder.Items.Count + 1] = new lineItemType { itemId = (model.EditOrder.Items.Count + 1).ToString(), name = "Shipping and Handling", quantity = 1m, unitPrice = model.EditOrder.ShipHand };
               lineItems[model.EditOrder.Items.Count + 2] = new lineItemType { itemId = (model.EditOrder.Items.Count + 2).ToString(), name = "Adjustment", quantity = 1m, unitPrice = model.EditOrder.Adjustment };

               createTransactionResponse response = authnetChargeClient.RunCharge(authorizeNetApiLoginID, authorizeNetApiTransactionKey, creditCard, billingAddress, lineItems, model.EditOrder.Total);

               model.ResponseCode = response.transactionResponse.responseCode;
               model.TransactionId = response.transactionResponse.transId;
               model.AuthorizationCode = response.transactionResponse.authCode;

               //save the order in the Db
               model.EditOrder.CardLastFour = model.CreditCard.CardNumber.Substring(model.CreditCard.CardNumber.Length - 4);
               model.EditOrder.TransactionId = model.TransactionId;
               model.EditOrder.ResponseCode = model.ResponseCode;
               model.EditOrder.AuthorizationCode = model.AuthorizationCode;
               model.Orders = await repository.AddOrderAsync(model.EditOrder, true);
               return View("Orders", model);
          }

          public async Task<ActionResult> RefundOrder(int id)
          {
               model.EditOrder = await repository.GetOrderAsync(id);
               RefundTransactionClient authnetRefundClient = new RefundTransactionClient();

               creditCardType creditCard = new creditCardType
               {
                    cardNumber = model.EditOrder.CardLastFour,
                    expirationDate = "XXXX"
               };

               decimal amount = model.EditOrder.Total;

               string transactionId = model.EditOrder.TransactionId;

               createTransactionResponse response = authnetRefundClient.RunRefund(authorizeNetApiLoginID, authorizeNetApiTransactionKey, creditCard, amount, transactionId);

               model.ResponseCode = response.transactionResponse.responseCode;
               model.TransactionId = response.transactionResponse.transId;
               model.AuthorizationCode = response.transactionResponse.authCode;

               //update the order in the Db
               model.EditOrder.Refunded = true;
               model.Orders = await repository.UpdateOrderAsync(model.EditOrder, true);
               return View("Orders", model);
          }

          public async Task<ActionResult> DeleteOrder(int id)
          {
               model.Orders = await repository.DeleteOrderAsync(id, true);
               return View("Orders", model);
          }

          private async Task<ViewOrder> CreateNewOrder(Product[] products)
          {
               BusinessRules busRules = await repository.GetBusinessRulesAsync();
               ViewOrder o = new ViewOrder(busRules, products);
               return o;
          }  //CreateNewOrder

          //this method is an AJAX target for CalculateOrder
          public async Task<ActionResult> CalculateOrder()
          {
               Stream req = Request.InputStream;
               req.Seek(0, System.IO.SeekOrigin.Begin);
               string payload = new StreamReader(req).ReadToEnd();
               AjaxOrder requestOrder = JsonConvert.DeserializeObject<AjaxOrder>(payload);

               BusinessRules busRules = await repository.GetBusinessRulesAsync();
               TaxRate[] taxRates = await repository.GetTaxRatesAsync();

               AjaxOrder responseOrder = OrderCalculator.CalculateOrder(requestOrder, busRules, taxRates);

               JsonResult response = Json(responseOrder);
               response.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
               return response;
          }  //CalculateOrder
          #endregion

          #region Products
          [HttpGet]
          public async Task<ActionResult> Products()
          {
               model.Products = await repository.GetProductsAsync(false);
               return View("Products", model);
          }

          [HttpPost]
          public async Task<ActionResult> Products(HomeViewModel model, int id)
          {
               model.Products = await repository.UpdateProductAsync(model.Products[id], true, false);
               return View("Products", model);
          }

          [HttpGet]
          public async Task<ActionResult> AddProduct()
          {
               model.NewProduct = new Product { Active = true };
               return View("AddProduct", model);
          }

          [HttpPost]
          public async Task<ActionResult> AddProduct(HomeViewModel model)
          {
               model.Products = await repository.AddProductAsync(model.NewProduct, true, false);
               return View("Products", model);
          }

          public async Task<ActionResult> DeleteProduct(int id)
          {
               model.Products = await repository.DeleteProductAsync(id, true, false);
               return View("Products", model);
          }

          #endregion

          #region PublicContacts
          [HttpGet]
          public async Task<ActionResult> PublicContacts()
          {
               model.PublicContacts = await repository.GetPublicContactsAsync();
               return View("PublicContacts", model);
          }

          [HttpPost]
          public async Task<ActionResult> PublicContacts(HomeViewModel model, int id)
          {
               model.PublicContacts = await repository.UpdatePublicContactAsync(model.PublicContacts[id], true);
               return View("PublicContacts", model);
          }

          [HttpGet]
          public ActionResult AddPublicContact()
          {
               return View("AddPublicContact", model);
          }

          [HttpPost]
          public async Task<ActionResult> AddPublicContact(HomeViewModel model)
          {
               model.PublicContacts = await repository.AddPublicContactAsync(model.NewPublicContact, true);
               return View("PublicContacts", model);
          }

          public async Task<ActionResult> DeletePublicContact(int id)
          {
               model.PublicContacts = await repository.DeletePublicContactAsync(id, true);
               return View("PublicContacts", model);
          }
          #endregion

          #region RetailContacts
          [HttpGet]
          public async Task<ActionResult> RetailContacts()
          {
               model.RetailContacts = await repository.GetRetailContactsAsync();
               return View(model);
          }

          [HttpPost]
          public async Task<ActionResult> RetailContacts(HomeViewModel model, int id)
          {
               model.RetailContacts = await repository.UpdateRetailContactAsync(model.RetailContacts[id], true);
               return View("RetailContacts", model);
          }

          [HttpGet]
          public ActionResult AddRetailContact()
          {
               return View("AddRetailContact", model);
          }

          [HttpPost]
          public async Task<ActionResult> AddRetailContact(HomeViewModel model)
          {
               model.RetailContacts = await repository.AddRetailContactAsync(model.NewRetailContact, true);
               return View("RetailContacts", model);
          }

          public async Task<ActionResult> DeleteRetailContact(int id)
          {
               model.RetailContacts = await repository.DeleteRetailContactAsync(id, true);
               return View("RetailContacts", model);
          }

          #endregion

          #region TaxRates
          [HttpGet]
          public async Task<ActionResult> TaxRates()
          {
               model.TaxRates = await repository.GetTaxRatesAsync();
               return View("TaxRates", model);
          }

          [HttpPost]
          public async Task<ActionResult> TaxRates(HomeViewModel model, int id)
          {
               model.TaxRates = await repository.UpdateTaxRateAsync(model.TaxRates[id], true);
               return View("TaxRates", model);
          }
          #endregion

          #region AuthorizeNet Sandbox
          [HttpGet]
          public ActionResult AuthorizeNetSandbox()
          {
               return View(model);
          }


          [HttpPost]
          public ActionResult AuthorizeNetSandbox(HomeViewModel model)
          {
               ChargeCreditCardClient authnetChargeClient = new ChargeCreditCardClient();

               creditCardType creditCard = new creditCardType
               {
                    cardNumber = "4111111111111111",
                    expirationDate = "0718",
                    cardCode = "123"
               };

               customerAddressType billingAddress = new customerAddressType
               {
                    firstName = "John",
                    lastName = "Doe",
                    address = "123 My St",
                    city = "OurTown",
                    zip = "98004"
               };

               lineItemType[] lineItems = new lineItemType[2];
               lineItems[0] = new lineItemType { itemId = "1", name = "t-shirt", quantity = 2, unitPrice = new Decimal(15.00) };
               lineItems[1] = new lineItemType { itemId = "2", name = "snowboard", quantity = 1, unitPrice = new Decimal(450.00) };


               createTransactionResponse response = authnetChargeClient.RunCharge(authorizeNetApiLoginID, authorizeNetApiTransactionKey, creditCard, billingAddress, lineItems, 99.75m);

               model.ResponseCode = response.transactionResponse.responseCode;
               model.TransactionId = response.transactionResponse.transId;
               model.AuthorizationCode = response.transactionResponse.authCode;
               return View(model);


               //RefundTransactionClient authnetRefundClient = new RefundTransactionClient();

               //creditCardType creditCard = new creditCardType
               //{
               //     cardNumber = "1111",
               //     expirationDate = "XXXX"
               //};

               //decimal amount = 99.75m;

               //string transactionId = "12345";

               //createTransactionResponse response = authnetRefundClient.RunRefund(authorizeNetApiLoginID, authorizeNetApiTransactionKey, creditCard, amount, transactionId);

               //model.ResponseCode = Int32.Parse(response.transactionResponse.responseCode);
               //model.TransactionId = response.transactionResponse.transId;
               //model.AuthorizationCode = response.transactionResponse.authCode;
               //return View(model);
          }
          #endregion
     }  //class
}  //namespace