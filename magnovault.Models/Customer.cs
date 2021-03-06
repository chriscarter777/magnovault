﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace magnovault.Models
{
     public class Customer
     {
          public string UserId { get; set; }
          public string FirstName { get; set; }
          public string LastName { get; set; }
          public string Street { get; set; }
          public string City { get; set; }
          [StringLength(2)]
          public string State { get; set; }
          [DataType(DataType.PostalCode)]
          public string Zip { get; set; }
          [DataType(DataType.EmailAddress)]
          public string Email { get; set; }
          [DataType(DataType.PhoneNumber)]
          public string Phone { get; set; }
          public string BestMethod { get; set; }
     }
}
