using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Threading.Tasks;

namespace magnovault.Models
{
     public class ApplicationUser : IdentityUser
     {
          //app uses these fields, in addition to the base IdentityUser fields, to define a customer or contact
          public string FirstName { get; set; }
          public string LastName { get; set; }
          public string Street { get; set; }
          public string City { get; set; }
          [StringLength(2)]
          public string State { get; set; }
          [DataType(DataType.PostalCode)]
          public string Zip { get; set; }
          public string BestMethod { get; set; }

          public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
          {
               // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
               var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
               // Add custom user claims here
               return userIdentity;
          }
     }
}