using magnovault.Models;
using System.Data.Entity;

namespace magnovault.Data
{
     public class DataDbContext : DbContext
     {
          public DataDbContext() : base("DataDbContext") { }

          public virtual DbSet<BusinessRules> BusinessRules { get; set; }
          public virtual DbSet<InvestorContact> InvestorContacts { get; set; }
          public virtual DbSet<MediaContact> MediaContacts { get; set; }
          public virtual DbSet<Order> Orders { get; set; }
          public virtual DbSet<OrderItem> OrderItems { get; set; }
          public virtual DbSet<Product> Products { get; set; }
          public virtual DbSet<PublicContact> PublicContacts { get; set; }
          public virtual DbSet<RetailContact> RetailContacts { get; set; }
          public virtual DbSet<TaxRate> TaxRates { get; set; }

          public TaxRate[] DefaultRates = new TaxRate[]
          {
          new TaxRate { State = "AL", Rate = 0.07500M },
          new TaxRate { State = "AK", Rate = 0.07500M },
          new TaxRate { State = "AZ", Rate = 0.07500M },
          new TaxRate { State = "AR", Rate = 0.07500M },
          new TaxRate { State = "CA", Rate = 0.07500M },
          new TaxRate { State = "CO", Rate = 0.07500M },
          new TaxRate { State = "CT", Rate = 0.07500M },
          new TaxRate { State = "DE", Rate = 0.07500M },
          new TaxRate { State = "DC", Rate = 0.07500M },
          new TaxRate { State = "FL", Rate = 0.07500M },
          new TaxRate { State = "GA", Rate = 0.07500M },
          new TaxRate { State = "HI", Rate = 0.07500M },
          new TaxRate { State = "ID", Rate = 0.07500M },
          new TaxRate { State = "IL", Rate = 0.07500M },
          new TaxRate { State = "IN", Rate = 0.07500M },
          new TaxRate { State = "IA", Rate = 0.07500M },
          new TaxRate { State = "KS", Rate = 0.07500M },
          new TaxRate { State = "KY", Rate = 0.07500M },
          new TaxRate { State = "LA", Rate = 0.07500M },
          new TaxRate { State = "ME", Rate = 0.07500M },
          new TaxRate { State = "MD", Rate = 0.07500M },
          new TaxRate { State = "MA", Rate = 0.07500M },
          new TaxRate { State = "MI", Rate = 0.07500M },
          new TaxRate { State = "MN", Rate = 0.07500M },
          new TaxRate { State = "MS", Rate = 0.07500M },
          new TaxRate { State = "MO", Rate = 0.07500M },
          new TaxRate { State = "MT", Rate = 0.07500M },
          new TaxRate { State = "HE", Rate = 0.07500M },
          new TaxRate { State = "NV", Rate = 0.07500M },
          new TaxRate { State = "NH", Rate = 0.07500M },
          new TaxRate { State = "NJ", Rate = 0.07500M },
          new TaxRate { State = "NM", Rate = 0.07500M },
          new TaxRate { State = "NY", Rate = 0.07500M },
          new TaxRate { State = "NC", Rate = 0.07500M },
          new TaxRate { State = "ND", Rate = 0.07500M },
          new TaxRate { State = "OH", Rate = 0.07500M },
          new TaxRate { State = "OK", Rate = 0.07500M },
          new TaxRate { State = "OR", Rate = 0.07500M },
          new TaxRate { State = "PA", Rate = 0.07500M },
          new TaxRate { State = "RI", Rate = 0.07500M },
          new TaxRate { State = "SC", Rate = 0.07500M },
          new TaxRate { State = "SD", Rate = 0.07500M },
          new TaxRate { State = "TN", Rate = 0.07500M },
          new TaxRate { State = "TX", Rate = 0.07500M },
          new TaxRate { State = "UT", Rate = 0.07500M },
          new TaxRate { State = "VT", Rate = 0.07500M },
          new TaxRate { State = "VA", Rate = 0.07500M },
          new TaxRate { State = "WA", Rate = 0.07500M },
          new TaxRate { State = "WV", Rate = 0.07500M },
          new TaxRate { State = "WI", Rate = 0.07500M },
          new TaxRate { State = "WY", Rate = 0.07500M },
          new TaxRate { State = "AS", Rate = 0.07500M },
          new TaxRate { State = "GU", Rate = 0.07500M },
          new TaxRate { State = "MP", Rate = 0.07500M },
          new TaxRate { State = "PR", Rate = 0.07500M },
          new TaxRate { State = "VI", Rate = 0.07500M },
          new TaxRate { State = "UM", Rate = 0.07500M },
          new TaxRate { State = "FM", Rate = 0.07500M },
          new TaxRate { State = "MH", Rate = 0.07500M },
          new TaxRate { State = "PW", Rate = 0.07500M },
          new TaxRate { State = "AA", Rate = 0.07500M },
          new TaxRate { State = "AE", Rate = 0.07500M },
          new TaxRate { State = "AP", Rate = 0.07500M }
          };
     }  //class
}  //namespace
