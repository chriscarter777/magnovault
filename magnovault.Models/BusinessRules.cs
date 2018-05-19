using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace magnovault.Models
{
     public class BusinessRules
     {
          public int Id { get; set; }
          public decimal ShipHandAmount { get; set; }
          public bool ShipHandPerItem { get; set; }
          public int? CurrentCampaign { get; set; }
          public string DeliveryTime { get; set; }
     }
}
