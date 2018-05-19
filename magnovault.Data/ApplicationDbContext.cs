using magnovault.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;


namespace magnovault.Data
{
     public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
     {
          public ApplicationDbContext()
              : base("ApplicationDbContext", throwIfV1Schema: false)
          {
          }

          public static ApplicationDbContext Create()
          {
               return new ApplicationDbContext();
          }
     }  //class
}  //namespace
