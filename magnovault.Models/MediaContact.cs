using System;
using System.ComponentModel.DataAnnotations;

namespace magnovault.Models
{
     public class MediaContact
     {
          public int Id { get; set; }
          [DataType(DataType.Date)]
          [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
          public DateTime OnDate { get; set; }
          public string FirstName { get; set; }
          public string LastName { get; set; }
          public string Title { get; set; }
          public string Organization { get; set; }
          public string City { get; set; }
          [StringLength(2)]
          public string State { get; set; }
          [DataType(DataType.PhoneNumber)]
          public string Phone { get; set; }
          [DataType(DataType.EmailAddress)]
          public string Email { get; set; }
          public string BestMethod { get; set; }
          public string HowHeard { get; set; }
          [DataType(DataType.MultilineText)]
          public string Message { get; set; }
          public bool Done { get; set; }

          public MediaContact() { }

          public MediaContact(Customer customer)
          {
               if (customer != null)
               {
                    FirstName = customer.FirstName;
                    LastName = customer.LastName;
                    City = customer.City;
                    State = customer.State;
                    Phone = customer.Phone;
                    Email = customer.Email;
                    BestMethod = (String.IsNullOrEmpty(customer.BestMethod)) ? "email" : customer.BestMethod;
               }
          }
     }
}