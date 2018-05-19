using System;
using System.ComponentModel.DataAnnotations;

namespace magnovault.Models
{
     public class PublicContact
     {
          public int Id { get; set; }
          [DataType(DataType.Date)]
          [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
          public DateTime OnDate { get; set; }
          public string FirstName { get; set; }
          public string LastName { get; set; }
          public string Street { get; set; }
          public string City { get; set; }
          [StringLength(2)]
          public string State { get; set; }
          [DataType(DataType.PostalCode)]
          public string Zip { get; set; }
          [DataType(DataType.PhoneNumber)]
          public string Phone { get; set; }
          [DataType(DataType.EmailAddress)]
          public string Email { get; set; }
          public string BestMethod { get; set; }
          public string HowHeard { get; set; }
          [DataType(DataType.MultilineText)]
          public string Message { get; set; }
          public bool Done { get; set; }

          public PublicContact() { }

          public PublicContact(Customer customer)
          {
               if (customer != null)
               {
                    FirstName = customer.FirstName;
                    LastName = customer.LastName;
                    City = customer.City;
                    State = customer.State;
                    Zip = customer.Zip;
                    Phone = customer.Phone;
                    Email = customer.Email;
                    BestMethod = (String.IsNullOrEmpty(customer.BestMethod)) ? "email" : customer.BestMethod;
               }
          }
     }
}