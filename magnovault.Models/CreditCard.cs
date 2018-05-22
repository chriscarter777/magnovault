using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace magnovault.Models
{
     public class CreditCard
     {
          public string BillFirstName { get; set; }
          public string BillLastName { get; set; }
          public string BillStreet { get; set; }
          public string BillCity { get; set; }
          [StringLength(2)]
          public string BillState { get; set; }
          [DataType(DataType.PostalCode)]
          public string BillZip { get; set; }

          public string CardNumber { get; set; }
          [DisplayFormat(DataFormatString = "{0:00}", ApplyFormatInEditMode = true)]
          [IntegerValidator(MinValue = 01, MaxValue = 12)]
          public int ExpMonth { get; set; }
          [DisplayFormat(DataFormatString = "{0:00}", ApplyFormatInEditMode = true)]
          [IntegerValidator(MinValue = 00, MaxValue = 99)]
          public int ExpYear { get; set; }
          public string CardCode { get; set; }

          public CreditCard() { }

          public CreditCard(Customer currentCustomer)
          {
               BillFirstName = currentCustomer.FirstName;
               BillLastName = currentCustomer.LastName;
               BillStreet = currentCustomer.Street;
               BillCity = currentCustomer.City;
               BillState = currentCustomer.State;
               BillZip = currentCustomer.Zip;
          }
     }
}
