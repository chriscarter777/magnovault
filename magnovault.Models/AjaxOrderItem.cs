using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace magnovault.Models
{
     [NotMapped]
     public class AjaxOrderItem
     {
          public string ProductName { get; set; }
          public string ProductShortDescr { get; set; }
          public decimal UnitPrice { get; set; }
          public int Quantity { get; set; }
          public decimal LineTotal { get; set; }
     }
}
