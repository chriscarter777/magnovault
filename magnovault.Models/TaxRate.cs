using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace magnovault.Models
{
     public class TaxRate
     {
          public int Id { get; set; }
          public string State { get; set; }
          [DisplayFormat(DataFormatString = "{0:0.00000}", ApplyFormatInEditMode = true)]
          public decimal Rate { get; set; }
          public bool AppliesToShipHand { get; set; }
     }
}
