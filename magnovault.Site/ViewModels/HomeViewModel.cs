using magnovault.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace magnovault.Site.ViewModels
{
     public class HomeViewModel
     {
          public Customer CurrentCustomer { get; set; }
          public InvestorContact InvestorContact { get; set; }
          public MediaContact MediaContact { get; set; }
          public ViewOrder Order { get; set; }
          public CreditCard CreditCard { get; set; }
          public string ResponseCode { get; set; }
          public string TransactionId { get; set; }
          public string AuthorizationCode { get; set; }
          public string OrderFeedback { get; set; }
          public Product[] Products { get; set; }
          public int ProductCount { get; set; }
          public PublicContact PublicContact { get; set; }
          public RetailContact RetailContact { get; set; }

          public string DeliveryTime { get; set; }
          public string VideoLink { get; set; }
     }  //class
}  //namespace