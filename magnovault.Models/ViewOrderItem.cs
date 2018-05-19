using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace magnovault.Models
{
     [NotMapped]
     public class ViewOrderItem : OrderItem
     {
          public string ProductName { get; set; }
          public string ProductShortDescr { get; set; }
          public string ProductImagePath { get; set; }

          public ViewOrderItem() { }  //default ctor

          public ViewOrderItem(OrderItem oi)  //ctor to convert database OrderItem to application domain ViewOrderItem
          {
               Id = oi.Id;
               OrderId = oi.OrderId;
               ProductId = oi.ProductId;
               UnitPrice = oi.UnitPrice;
               Quantity = oi.Quantity;
               LineTotal = oi.LineTotal;
          }

          public ViewOrderItem(Product p)
          {
               ProductId = p.Id;
               ProductName = p.Name;
               ProductShortDescr = p.ShortDescr;
               ProductImagePath = p.ImagePath;
               UnitPrice = p.Price;
          }

          public ViewOrderItem(OrderItem oi, Product p)
          {
               Id = oi.Id;
               OrderId = oi.OrderId;
               ProductId = oi.ProductId;
               ProductName = p.Name;
               ProductShortDescr = p.ShortDescr;
               ProductImagePath = p.ImagePath;
               UnitPrice = oi.UnitPrice;
               Quantity = oi.Quantity;
               LineTotal = oi.LineTotal;
          }
     }
}