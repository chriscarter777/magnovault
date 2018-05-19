using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace magnovault.Models
{
     //this version of Order is a stripped-down version of ViewOrder, containing only the info needed to return an order calculation from the page
     [NotMapped]
     public class AjaxOrder
     {
          public string ShipState { get; set; }
          public string ShipZip { get; set; }
          public AjaxOrderItem[] Items { get; set; }
          public int? Campaign { get; set; }
          public decimal Subtotal { get; set; }
          public decimal Tax { get; set; }
          public decimal ShipHand { get; set; }
          public decimal Adjustment { get; set; }
          public decimal Total { get; set; }
          public bool Deferred { get; set; }
     }
}
