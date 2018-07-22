using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace magnovault.Models
{
     //this version of Order contains all info needed for initial presentation on the page
     [NotMapped]
     public class ViewOrder : Order
     {
          [DisplayName("First Name")]
          public string FirstName { get; set; }
          [DisplayName("Last Name")]
          public string LastName { get; set; }
          public string Street { get; set; }
          public string City { get; set; }
          [StringLength(2)]
          public string State { get; set; }
          [DataType(DataType.PostalCode)]
          public string Zip { get; set; }
          public string Email { get; set; }
          public string Phone { get; set; }
          [DisplayName("Preferred")]
          public string BestMethod { get; set; }
          public List<ViewOrderItem> Items { get; set; }

          //---------------------------------------------------------------------------------------------
          public ViewOrder() { }  //default ctor

          public ViewOrder(Order o)  //ctor to adapt database class:Order to domain class:ViewOrder
          {
               Id = o.Id;
               OnDate = o.OnDate;
               CustomerId = o.CustomerId;
               Items = new List<ViewOrderItem>();
               ShipFirstName = o.ShipFirstName;
               ShipLastName = o.ShipLastName;
               ShipStreet = o.ShipStreet;
               ShipCity = o.ShipCity;
               ShipState = o.ShipState;
               ShipZip = o.ShipZip;
               Subtotal = o.Subtotal;
               Campaign = o.Campaign;
               Tax = o.Tax;
               ShipHand = o.ShipHand;
               Total = o.Total;
               TransactionId = o.TransactionId;
               AuthorizationCode = o.AuthorizationCode;
               CardLastFour = o.CardLastFour;
               Deferred = o.Deferred;
               Refunded = o.Refunded;
               Done = o.Done;
          }

          public ViewOrder(Order o, Customer c)
          {
               Id = o.Id;
               OnDate = o.OnDate;
               CustomerId = o.CustomerId;

               FirstName = c.FirstName;
               LastName = c.LastName;
               Street = c.Street;
               City = c.City;
               State = c.State;
               Zip = c.Zip;
               Email = c.Email;
               Phone = c.Phone;
               BestMethod = c.BestMethod;


               Items = new List<ViewOrderItem>();
               ShipFirstName = o.ShipFirstName;
               ShipLastName = o.ShipLastName;
               ShipStreet = o.ShipStreet;
               ShipCity = o.ShipCity;
               ShipState = o.ShipState;
               ShipZip = o.ShipZip;
               Subtotal = o.Subtotal;
               Campaign = o.Campaign;
               Tax = o.Tax;
               ShipHand = o.ShipHand;
               Total = o.Total;
               TransactionId = o.TransactionId;
               AuthorizationCode = o.AuthorizationCode;
               CardLastFour = o.CardLastFour;
               Deferred = o.Deferred;
               Refunded = o.Refunded;
               Done = o.Done;
          }


          public ViewOrder(BusinessRules busRules, Product[] products)  //ctor to create an empty order, with items for each product.
          {
               OnDate = DateTime.Now;
               Campaign = busRules.CurrentCampaign;
               Items = new List<ViewOrderItem>();

               if (products != null)
               {
                    foreach (Product product in products)
                    {
                         Items.Add(new ViewOrderItem
                         {
                              ProductId = product.Id,
                              ProductName = product.Name,
                              ProductShortDescr = product.ShortDescr,
                              ProductImagePath = product.ImagePath,
                              UnitPrice = product.Price,
                              Quantity = 0,
                              LineTotal = 0
                         });
                    }
               }
          }

          public ViewOrder(BusinessRules busRules, Customer customer, Product[] products)  //ctor to create an empty order for a known user, with items for each product.
          {
               if (customer != null)
               {
                    CustomerId = customer.UserId;
                    FirstName = customer.FirstName;
                    ShipFirstName = customer.FirstName;
                    LastName = customer.LastName;
                    ShipLastName = customer.LastName;
                    Street = customer.Street;
                    ShipStreet = customer.Street;
                    City = customer.City;
                    ShipCity = customer.City;
                    State = customer.State;
                    ShipState = customer.State;
                    Zip = customer.Zip;
                    ShipZip = customer.Zip;
                    Email = customer.Email;
                    Phone = customer.Phone;
                    BestMethod = customer.BestMethod;
               }

               OnDate = DateTime.Now;
               Campaign = busRules.CurrentCampaign;
               Items = new List<ViewOrderItem>();

               if (products != null)
               {
                    foreach (Product product in products)
                    {
                         Items.Add(new ViewOrderItem
                         {
                              ProductId = product.Id,
                              ProductName = product.Name,
                              ProductShortDescr = product.ShortDescr,
                              ProductImagePath = product.ImagePath,
                              UnitPrice = product.Price,
                              Quantity = 0,
                              LineTotal = 0
                         });
                    }
               }
          }

     }  //class
}  //namespace