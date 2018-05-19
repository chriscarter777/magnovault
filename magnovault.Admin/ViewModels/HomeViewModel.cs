using magnovault.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace magnovault.Admin.ViewModels
{
     public class HomeViewModel
     {
          public BusinessRules BusinessRules { get; set; }
          public Customer[] Customers { get; set; }
          public InvestorContact[] InvestorContacts { get; set; }
          public InvestorContact NewInvestorContact { get; set; }
          public MediaContact[] MediaContacts { get; set; }
          public MediaContact NewMediaContact { get; set; }
          public ViewOrder[] Orders { get; set; }
          public ViewOrder EditOrder { get; set; }
          public CreditCard CreditCard { get; set; }
          public Product[] Products { get; set; }
          public Product NewProduct { get; set; }
          public PublicContact[] PublicContacts { get; set; }
          public PublicContact NewPublicContact { get; set; }
          public RetailContact[] RetailContacts { get; set; }
          public RetailContact NewRetailContact { get; set; }
          public TaxRate[] TaxRates { get; set; }

          public List<SelectListItem> CustomerList { get; set; }
          public string ResponseCode { get; set; }
          public string TransactionId { get; set; }
          public string AuthorizationCode { get; set; }
     }  //class
}  //namespace