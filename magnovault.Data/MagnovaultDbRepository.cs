using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using magnovault.Models;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace magnovault.Data
{
     public class MagnovaultDbRepository : IMagnovaultDbRepository
     {
          private DataDbContext context;
          public MagnovaultDbRepository()
          {
               context = new DataDbContext();
          }  //ctor----------------------------------------------------------------------------------------------

          #region BusinessRules
          public async Task<BusinessRules> GetBusinessRulesAsync()
          {
               using (DataDbContext context = new DataDbContext())
               {
                    BusinessRules rules = context.BusinessRules.FirstOrDefault();
                    return rules;
               }
          }

          public async Task<BusinessRules> UpdateBusinessRulesAsync(BusinessRules updatedBusinessRules, bool returnNewRules)
          {
               using (DataDbContext context = new DataDbContext())
               {
                    var existingBusinessRules = context.BusinessRules.FirstOrDefault();
                    if (existingBusinessRules == null)
                    {
                         context.BusinessRules.Add(updatedBusinessRules);
                         context.SaveChanges();
                    }
                    else
                    {
                         //existingBusinessRules = updatedBusinessRules;
                         context.Entry(existingBusinessRules).CurrentValues.SetValues(updatedBusinessRules);
                         context.SaveChanges();
                    }
                    if (returnNewRules)
                    {
                         return await GetBusinessRulesAsync();
                    }
                    else
                    {
                         return null;
                    }
               }  //using
          }
          #endregion

          #region Customers
          public async Task<Customer> GetCustomerAsync(string id)
          {
               //ApplicationUser appuser = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
               using (ApplicationDbContext appcontext = new ApplicationDbContext())
               {
                    ApplicationUser appuser = appcontext.Users.FirstOrDefault(x => x.Id == id);
                    if(appuser == null)
                    {
                         return null;
                    }
                    else
                    {
                         return new Customer
                         {
                              UserId = appuser.Id,
                              FirstName = appuser.FirstName,
                              LastName = appuser.LastName,
                              Street = appuser.Street,
                              City = appuser.City,
                              State = appuser.State,
                              Zip = appuser.Zip,
                              Email = appuser.Email,
                              Phone = appuser.PhoneNumber,
                              BestMethod = appuser.BestMethod
                         };
                    }
               }
          }

          public async Task<Customer[]> GetCustomersAsync()
          {
               using (ApplicationDbContext appcontext = new ApplicationDbContext())
               {
                    ApplicationUser[] appusers = appcontext.Users.OrderBy(x => x.LastName).ToArray();
                    Customer[] customers = new Customer[appusers.Length];
                    for (int i = 0; i < appusers.Length; i++)
                    {
                         customers[i] = new Customer
                         {
                              UserId = appusers[i].Id,
                              FirstName = appusers[i].FirstName,
                              LastName = appusers[i].LastName,
                              Street = appusers[i].Street,
                              City = appusers[i].City,
                              State = appusers[i].State,
                              Zip = appusers[i].Zip,
                              Email = appusers[i].Email,
                              Phone = appusers[i].PhoneNumber,
                              BestMethod = appusers[i].BestMethod
                         };

                    }
                    return customers;
               }
          }

          public async Task<Customer[]> UpdateCustomerAsync(Customer updatedCustomer, bool returnNewList)
          {
               using (ApplicationDbContext appcontext = new ApplicationDbContext())
               {
                    ApplicationUser existingUser = appcontext.Users.SingleOrDefault(x => x.Id == updatedCustomer.UserId);
                    if (existingUser != null)
                    {
                         existingUser.FirstName = updatedCustomer.FirstName;
                         existingUser.LastName = updatedCustomer.LastName;
                         existingUser.Street = updatedCustomer.Street;
                         existingUser.City = updatedCustomer.City;
                         existingUser.State = updatedCustomer.State;
                         existingUser.Zip = updatedCustomer.Zip;
                         existingUser.Email = updatedCustomer.Email;
                         existingUser.UserName = updatedCustomer.Email;
                         existingUser.PhoneNumber = updatedCustomer.Phone;
                         existingUser.BestMethod = updatedCustomer.BestMethod;
                         appcontext.SaveChanges();
                    }
                    if (returnNewList)
                    {
                         return await GetCustomersAsync();
                    }
                    else
                    {
                         return null;
                    }
               }
          }
          #endregion

          #region InvestorContacts
          public async Task<InvestorContact[]> AddInvestorContactAsync(InvestorContact newContact, bool returnNewList)
          {
               using (DataDbContext context = new DataDbContext())
               {
                    context.InvestorContacts.Add(newContact);
                    context.SaveChanges();
                    if (returnNewList)
                    {
                         return await GetInvestorContactsAsync();
                    }
                    else
                    {
                         return null;
                    }
               }  //using
          }

          public async Task<InvestorContact[]> DeleteInvestorContactAsync(int id, bool returnNewList)
          {
               using (DataDbContext context = new DataDbContext())
               {
                    var existingContact = context.InvestorContacts.SingleOrDefault(x => x.Id == id);
                    if (existingContact != null)
                    {
                         context.InvestorContacts.Remove(existingContact);
                         context.SaveChanges();
                    }
                    if (returnNewList)
                    {
                         return await GetInvestorContactsAsync();
                    }
                    else
                    {
                         return null;
                    }
               }  //using
          }

          public async Task<InvestorContact> GetInvestorContactAsync(int id)
          {
               using (DataDbContext context = new DataDbContext())
               {
                    InvestorContact contact = context.InvestorContacts.SingleOrDefault(x => x.Id == id);
                    return contact;
               }
          }

          public async Task<InvestorContact[]> GetInvestorContactsAsync()
          {
               using (DataDbContext context = new DataDbContext())
               {
                    InvestorContact[] contacts = context.InvestorContacts.OrderBy(x => x.LastName).ToArray();
                    return contacts;
               }
          }

          public async Task<InvestorContact[]> UpdateInvestorContactAsync(InvestorContact updatedContact, bool returnNewList)
          {
               using (DataDbContext context = new DataDbContext())
               {
                    var existingContact = context.InvestorContacts.SingleOrDefault(x => x.Id == updatedContact.Id);
                    if (existingContact != null)
                    {
                         //existingContact = updatedContact;
                         context.Entry(existingContact).CurrentValues.SetValues(updatedContact);
                         context.SaveChanges();
                    }
                    if (returnNewList)
                    {
                         return await GetInvestorContactsAsync();
                    }
                    else
                    {
                         return null;
                    }
               }  //using
          }
          #endregion

          #region MediaContacts
          public async Task<MediaContact[]> AddMediaContactAsync(MediaContact newContact, bool returnNewList)
          {
               using (DataDbContext context = new DataDbContext())
               {
                    context.MediaContacts.Add(newContact);
                    context.SaveChanges();
                    if (returnNewList)
                    {
                         return await GetMediaContactsAsync();
                    }
                    else
                    {
                         return null;
                    }
               }  //using
          }

          public async Task<MediaContact[]> DeleteMediaContactAsync(int id, bool returnNewList)
          {
               using (DataDbContext context = new DataDbContext())
               {
                    var existingContact = context.MediaContacts.SingleOrDefault(x => x.Id == id);
                    if (existingContact != null)
                    {
                         context.MediaContacts.Remove(existingContact);
                         context.SaveChanges();
                    }
                    if (returnNewList)
                    {
                         return await GetMediaContactsAsync();
                    }
                    else
                    {
                         return null;
                    }
               }  //using
          }

          public async Task<MediaContact> GetMediaContactAsync(int id)
          {
               using (DataDbContext context = new DataDbContext())
               {
                    MediaContact contact = context.MediaContacts.SingleOrDefault(x => x.Id == id);
                    return contact;
               }
          }

          public async Task<MediaContact[]> GetMediaContactsAsync()
          {
               using (DataDbContext context = new DataDbContext())
               {
                    MediaContact[] contacts = context.MediaContacts.OrderBy(x => x.LastName).ToArray();
                    return contacts;
               }
          }

          public async Task<MediaContact[]> UpdateMediaContactAsync(MediaContact updatedContact, bool returnNewList)
          {
               using (DataDbContext context = new DataDbContext())
               {
                    var existingContact = context.MediaContacts.SingleOrDefault(x => x.Id == updatedContact.Id);
                    if (existingContact != null)
                    {
                         //existingContact = updatedContact;
                         context.Entry(existingContact).CurrentValues.SetValues(updatedContact);
                         context.SaveChanges();
                    }
                    if (returnNewList)
                    {
                         return await GetMediaContactsAsync();
                    }
                    else
                    {
                         return null;
                    }
               }  //using
          }
          #endregion

          #region Orders
          public async Task<ViewOrder[]> AddOrderAsync(ViewOrder newViewOrder, bool returnNewList)
          {
               using (DataDbContext context = new DataDbContext())
               {
                    Order newOrder = new Order
                    {
                         Id = newViewOrder.Id,
                         OnDate = newViewOrder.OnDate,
                         CustomerId = newViewOrder.CustomerId,
                         ShipFirstName = newViewOrder.ShipFirstName,
                         ShipLastName = newViewOrder.ShipLastName,
                         ShipStreet = newViewOrder.ShipStreet,
                         ShipCity = newViewOrder.ShipCity,
                         ShipState = newViewOrder.ShipState,
                         ShipZip = newViewOrder.ShipZip,
                         Campaign = newViewOrder.Campaign,
                         Subtotal = newViewOrder.Subtotal,
                         Tax = newViewOrder.Tax,
                         ShipHand = newViewOrder.ShipHand,
                         CardLastFour = newViewOrder.CardLastFour,
                         TransactionId = newViewOrder.TransactionId,
                         ResponseCode = newViewOrder.ResponseCode,
                         AuthorizationCode = newViewOrder.AuthorizationCode,
                         Total = newViewOrder.Total,
                         Deferred = newViewOrder.Deferred,
                         Done = newViewOrder.Done
                    };
                    context.Orders.Add(newOrder);
                    context.SaveChanges();
                    //now that we have saved, the database has generated an id that we can access, to create a parent reference for OrderItems
                    int orderid = newOrder.Id;

                    List<OrderItem> dbOrderItems = new List<OrderItem>();
                    foreach (ViewOrderItem item in newViewOrder.Items)
                    {
                         dbOrderItems.Add(new OrderItem
                         {
                              OrderId = orderid,
                              ProductId = item.ProductId,
                              UnitPrice = item.UnitPrice,
                              Quantity = item.Quantity,
                              LineTotal = item.LineTotal
                         });
                    }
                    context.OrderItems.AddRange(dbOrderItems);
                    context.SaveChanges();
                    if (returnNewList)
                    {
                         return await GetOrdersAsync();
                    }
                    else
                    {
                         return null;
                    }
               }  //using
          }

          public async Task<ViewOrder[]> DeleteOrderAsync(int id, bool returnNewList)
          {
               using (DataDbContext context = new DataDbContext())
               {
                    Order existingOrder = context.Orders.SingleOrDefault(x => x.Id == id);
                    if (existingOrder != null)
                    {
                         OrderItem[] existingOrderItems = context.OrderItems.Where(x => x.OrderId == id).ToArray();
                         context.Orders.Remove(existingOrder);
                         if (existingOrderItems != null)
                         {
                              context.OrderItems.RemoveRange(existingOrderItems);
                         }
                         context.SaveChanges();
                    }
                    if (returnNewList)
                    {
                         return await GetOrdersAsync();
                    }
                    else
                    {
                         return null;
                    }
               }  //using
          }

          public async Task<ViewOrder> GetDeferredOrderAsync(string id)
          {
               using (DataDbContext context = new DataDbContext())
               {
                    Order orderData = context.Orders
                         .Where(x => x.CustomerId == id)
                         .OrderByDescending(x => x.OnDate)
                         .FirstOrDefault();

                    if (orderData == null)
                    {
                         return null;
                    }
                    else
                    {
                         using (ApplicationDbContext appcontext = new ApplicationDbContext())
                         {
                              ApplicationUser appuser = appcontext.Users.FirstOrDefault(x => x.Id == id);
                              ViewOrder vieworder = new ViewOrder
                              {
                                   Id = orderData.Id,
                                   OnDate = orderData.OnDate,
                                   CustomerId = orderData.CustomerId,
                                   FirstName = (appuser == null) ? "" : appuser.FirstName,
                                   LastName = (appuser == null) ? "" : appuser.LastName,
                                   Street = (appuser == null) ? "" : appuser.Street,
                                   City = (appuser == null) ? "" : appuser.City,
                                   State = (appuser == null) ? "" : appuser.State,
                                   Zip = (appuser == null) ? "" : appuser.Zip,
                                   Email = (appuser == null) ? "" : appuser.Email,
                                   Phone = (appuser == null) ? "" : appuser.PhoneNumber,
                                   BestMethod = (appuser == null) ? "" : appuser.BestMethod,
                                   ShipFirstName = orderData.ShipFirstName,
                                   ShipLastName = orderData.ShipLastName,
                                   ShipStreet = orderData.ShipStreet,
                                   ShipCity = orderData.ShipCity,
                                   ShipState = orderData.ShipState,
                                   ShipZip = orderData.ShipZip,
                                   Campaign = orderData.Campaign,
                                   Subtotal = orderData.Subtotal,
                                   ShipHand = orderData.ShipHand,
                                   Tax = orderData.Tax,
                                   Total = orderData.Total,
                                   TransactionId = orderData.TransactionId,
                                   ResponseCode = orderData.ResponseCode,
                                   AuthorizationCode = orderData.AuthorizationCode,
                                   CardLastFour = orderData.CardLastFour,
                                   Deferred = orderData.Deferred,
                                   Refunded = orderData.Refunded,
                                   Done = orderData.Done,
                                   Items = new List<ViewOrderItem>()
                              };


                              //add order items
                              int oid = orderData.Id;
                              var orderItemData = context.OrderItems
                                   .Join(context.Products, o => o.ProductId, p => p.Id, (orderItem, product) => new { OrderItem = orderItem, Product = product })
                                   .Where(x => x.OrderItem.OrderId == oid)
                                   .ToArray();

                              if (orderItemData != null)
                              {
                                   for (int i = 0; i < orderItemData.Length; i++)
                                   {
                                        ViewOrderItem vieworderitem = new ViewOrderItem
                                        {
                                             Id = orderItemData[i].OrderItem.Id,
                                             OrderId = oid,
                                             ProductId = orderItemData[i].OrderItem.ProductId,
                                             ProductName = orderItemData[i].Product.Name,
                                             ProductShortDescr = orderItemData[i].Product.ShortDescr,
                                             ProductImagePath = orderItemData[i].Product.ImagePath,
                                             UnitPrice = orderItemData[i].OrderItem.UnitPrice,
                                             Quantity = orderItemData[i].OrderItem.Quantity,
                                             LineTotal = orderItemData[i].OrderItem.LineTotal
                                        };
                                        vieworder.Items.Add(vieworderitem);
                                   }
                              }
                              return vieworder;
                         }  //using ApplicationDbContext
                    }
               }  //using DataDbContext
          }

          public async Task<ViewOrder> GetOrderAsync(int id)
          {
               using (DataDbContext context = new DataDbContext())
               {
                    Order orderData = context.Orders.SingleOrDefault(x => x.Id == id);

                    if (orderData == null)
                    {
                         return null;
                    }
                    else
                    {
                         using (ApplicationDbContext appcontext = new ApplicationDbContext())
                         {
                              ApplicationUser appuser = appcontext.Users.FirstOrDefault(x => x.Id == orderData.CustomerId);
                              ViewOrder vieworder = new ViewOrder
                              {
                                   Id = orderData.Id,
                                   OnDate = orderData.OnDate,
                                   CustomerId = orderData.CustomerId,
                                   FirstName = (appuser == null) ? "" : appuser.FirstName,
                                   LastName = (appuser == null) ? "" : appuser.LastName,
                                   Street = (appuser == null) ? "" : appuser.Street,
                                   City = (appuser == null) ? "" : appuser.City,
                                   State = (appuser == null) ? "" : appuser.State,
                                   Zip = (appuser == null) ? "" : appuser.Zip,
                                   Email = (appuser == null) ? "" : appuser.Email,
                                   Phone = (appuser == null) ? "" : appuser.PhoneNumber,
                                   BestMethod = (appuser == null) ? "" : appuser.BestMethod,
                                   ShipFirstName = orderData.ShipFirstName,
                                   ShipLastName = orderData.ShipLastName,
                                   ShipStreet = orderData.ShipStreet,
                                   ShipCity = orderData.ShipCity,
                                   ShipState = orderData.ShipState,
                                   ShipZip = orderData.ShipZip,
                                   Campaign = orderData.Campaign,
                                   Subtotal = orderData.Subtotal,
                                   ShipHand = orderData.ShipHand,
                                   Tax = orderData.Tax,
                                   Total = orderData.Total,
                                   TransactionId = orderData.TransactionId,
                                   ResponseCode = orderData.ResponseCode,
                                   AuthorizationCode = orderData.AuthorizationCode,
                                   CardLastFour = orderData.CardLastFour,
                                   Deferred = orderData.Deferred,
                                   Refunded = orderData.Refunded,
                                   Done = orderData.Done,
                                   Items = new List<ViewOrderItem>()
                              };

                              //add order items
                              int oid = orderData.Id;
                              var orderItemData = context.OrderItems
                                   .Join(context.Products, o => o.ProductId, p => p.Id, (orderItem, product) => new { OrderItem = orderItem, Product = product })
                                   .Where(x => x.OrderItem.OrderId == oid)
                                   .ToArray();

                              if (orderItemData != null)
                              {
                                   for (int i = 0; i < orderItemData.Length; i++)
                                   {
                                        ViewOrderItem vieworderitem = new ViewOrderItem
                                        {
                                             Id = orderItemData[i].OrderItem.Id,
                                             OrderId = oid,
                                             ProductId = orderItemData[i].OrderItem.ProductId,
                                             ProductName = orderItemData[i].Product.Name,
                                             ProductShortDescr = orderItemData[i].Product.ShortDescr,
                                             ProductImagePath = orderItemData[i].Product.ImagePath,
                                             UnitPrice = orderItemData[i].OrderItem.UnitPrice,
                                             Quantity = orderItemData[i].OrderItem.Quantity,
                                             LineTotal = orderItemData[i].OrderItem.LineTotal
                                        };
                                        vieworder.Items.Add(vieworderitem);
                                   }
                              }
                              return vieworder;
                         }  //using ApplicationDbContext
                    }
               }  //using DataDbContext
          }

          public async Task<ViewOrder[]> GetOrdersAsync()
          {
               using (DataDbContext context = new DataDbContext())
               {
                    Order[] orderData = context.Orders
                         .ToArray();

                    if (orderData == null)
                    {
                         return null;
                    }
                    else
                    {
                         ViewOrder[] vieworders = new ViewOrder[orderData.Length];
                         using (ApplicationDbContext appcontext = new ApplicationDbContext())
                         {
                              for (int i = 0; i < orderData.Length; i++)
                              {
                                   string userId = orderData[i].CustomerId;
                                   ApplicationUser appuser = appcontext.Users.FirstOrDefault(x => x.Id == userId);
                                   vieworders[i] = new ViewOrder
                                   {
                                        Id = orderData[i].Id,
                                        OnDate = orderData[i].OnDate,
                                        CustomerId = orderData[i].CustomerId,
                                        FirstName = (appuser == null) ? "" : appuser.FirstName,
                                        LastName = (appuser == null) ? "" : appuser.LastName,
                                        Street = (appuser == null) ? "" : appuser.Street,
                                        City = (appuser == null) ? "" : appuser.City,
                                        State = (appuser == null) ? "" : appuser.State,
                                        Zip = (appuser == null) ? "" : appuser.Zip,
                                        Email = (appuser == null) ? "" : appuser.Email,
                                        Phone = (appuser == null) ? "" : appuser.PhoneNumber,
                                        BestMethod = (appuser == null) ? "" : appuser.BestMethod,
                                        ShipFirstName = orderData[i].ShipFirstName,
                                        ShipLastName = orderData[i].ShipLastName,
                                        ShipStreet = orderData[i].ShipStreet,
                                        ShipCity = orderData[i].ShipCity,
                                        ShipState = orderData[i].ShipState,
                                        ShipZip = orderData[i].ShipZip,
                                        Campaign = orderData[i].Campaign,
                                        Subtotal = orderData[i].Subtotal,
                                        ShipHand = orderData[i].ShipHand,
                                        Tax = orderData[i].Tax,
                                        Total = orderData[i].Total,
                                        TransactionId = orderData[i].TransactionId,
                                        ResponseCode = orderData[i].ResponseCode,
                                        AuthorizationCode = orderData[i].AuthorizationCode,
                                        CardLastFour = orderData[i].CardLastFour,
                                        Deferred = orderData[i].Deferred,
                                        Refunded = orderData[i].Refunded,
                                        Done = orderData[i].Done,
                                        Items = new List<ViewOrderItem>()
                                   };

                                   //add order items
                                   int oid = orderData[i].Id;
                                   var orderItemData = context.OrderItems
                                        .Join(context.Products, o => o.ProductId, p => p.Id, (orderItem, product) => new { OrderItem = orderItem, Product = product })
                                        .Where(x => x.OrderItem.OrderId == oid)
                                        .ToArray();

                                   if (orderItemData != null)
                                   {
                                        for (int j = 0; j < orderItemData.Length; j++)
                                        {
                                             ViewOrderItem vieworderitem = new ViewOrderItem
                                             {
                                                  Id = orderItemData[j].OrderItem.Id,
                                                  OrderId = oid,
                                                  ProductId = orderItemData[j].OrderItem.ProductId,
                                                  ProductName = orderItemData[j].Product.Name,
                                                  ProductShortDescr = orderItemData[j].Product.ShortDescr,
                                                  ProductImagePath = orderItemData[j].Product.ImagePath,
                                                  UnitPrice = orderItemData[j].OrderItem.UnitPrice,
                                                  Quantity = orderItemData[j].OrderItem.Quantity,
                                                  LineTotal = orderItemData[j].OrderItem.LineTotal
                                             };
                                             vieworders[i].Items.Add(vieworderitem);
                                        }
                                   }
                              }  //order iteration
                              return vieworders;
                         }  //using ApplicationDbContext
                    }
               }  //using DataDbContext
          }

          public async Task<ViewOrder[]> UpdateOrderAsync(ViewOrder updatedViewOrder, bool returnNewList)
          {
               using (DataDbContext context = new DataDbContext())
               {
                    var existingOrder = context.Orders.SingleOrDefault(x => x.Id == updatedViewOrder.Id);
                    if (existingOrder != null)
                    {
                         Order updatedOrder = new Order
                         {
                              Id = updatedViewOrder.Id,
                              OnDate = updatedViewOrder.OnDate,
                              CustomerId = updatedViewOrder.CustomerId,
                              ShipFirstName = updatedViewOrder.ShipFirstName,
                              ShipLastName = updatedViewOrder.ShipLastName,
                              ShipStreet = updatedViewOrder.ShipStreet,
                              ShipCity = updatedViewOrder.ShipCity,
                              ShipState = updatedViewOrder.ShipState,
                              ShipZip = updatedViewOrder.ShipZip,
                              Campaign = updatedViewOrder.Campaign,
                              Subtotal = updatedViewOrder.Subtotal,
                              Tax = updatedViewOrder.Tax,
                              ShipHand = updatedViewOrder.ShipHand,
                              Total = updatedViewOrder.Total,
                              Deferred = updatedViewOrder.Deferred,
                              Done = updatedViewOrder.Done
                         };
                         //existingOrder = updatedOrder;
                         context.Entry(existingOrder).CurrentValues.SetValues(updatedViewOrder);

                         //order items: out with the old
                         OrderItem[] existingOrderItems = context.OrderItems.Where(x => x.OrderId == updatedViewOrder.Id).ToArray();
                         context.OrderItems.RemoveRange(existingOrderItems);

                         //and in with the new
                         List<OrderItem> updatedOrderItems = new List<OrderItem>();
                         foreach (ViewOrderItem item in updatedViewOrder.Items)
                         {
                              updatedOrderItems.Add(new OrderItem
                              {
                                   OrderId = updatedViewOrder.Id,
                                   ProductId = item.ProductId,
                                   UnitPrice = item.UnitPrice,
                                   Quantity = item.Quantity,
                                   LineTotal = item.LineTotal
                              });
                         }
                         context.OrderItems.AddRange(updatedOrderItems);
                         context.SaveChanges();
                    }
                    if (returnNewList)
                    {
                         return await GetOrdersAsync();
                    }
                    else
                    {
                         return null;
                    }
               }  //using
          }
          #endregion

          #region Products
          public async Task<Product[]> AddProductAsync(Product newProduct, bool returnAllProds, bool returnActiveProds)
          {
               using (DataDbContext context = new DataDbContext())
               {
                    context.Products.Add(newProduct);
                    context.SaveChanges();
                    if (returnAllProds)
                    {
                         return await GetProductsAsync(false);
                    }
                    else if (returnActiveProds)
                    {
                         return await GetProductsAsync(true);
                    }
                    else
                    {
                         return null;
                    }
               }  //using
          }
          public async Task<Product[]> DeleteProductAsync(int id, bool returnAllProds, bool returnActiveProds)
          {
               using (DataDbContext context = new DataDbContext())
               {
                    var existingProduct = context.Products.SingleOrDefault(x => x.Id == id);
                    if (existingProduct != null)
                    {
                         context.Products.Remove(existingProduct);
                         context.SaveChanges();
                    }
                    if (returnAllProds)
                    {
                         return await GetProductsAsync(false);
                    }
                    else if (returnActiveProds)
                    {
                         return await GetProductsAsync(true);
                    }
                    else
                    {
                         return null;
                    }
               }  //using
          }
          public async Task<Product> GetProductAsync(int id)
          {
               using (DataDbContext context = new DataDbContext())
               {
                    Product product = context.Products.SingleOrDefault(x => x.Id == id);
                    return product;
               }
          }
          public async Task<Product[]> GetProductsAsync(bool activeOnly)
          {
               using (DataDbContext context = new DataDbContext())
               {
                    Product[] products;
                    if (activeOnly)
                    {
                         products = context.Products.Where(x => x.Active == true).OrderBy(x => x.Name).ToArray();
                    }
                    else
                    {
                         products = context.Products.OrderBy(x => x.Name).ToArray();
                    }
                    return products;
               }
          }
          public async Task<Product[]> UpdateProductAsync(Product updatedProduct, bool returnAllProds, bool returnActiveProds)
          {
               using (DataDbContext context = new DataDbContext())
               {
                    var existingProduct = context.Products.SingleOrDefault(x => x.Id == updatedProduct.Id);
                    if (existingProduct != null)
                    {
                         //existingProduct = updatedProduct;
                         context.Entry(existingProduct).CurrentValues.SetValues(updatedProduct);
                         context.SaveChanges();
                    }
                    if (returnAllProds)
                    {
                         return await GetProductsAsync(false);
                    }
                    else if (returnActiveProds)
                    {
                         return await GetProductsAsync(true);
                    }
                    else
                    {
                         return null;
                    }
               }  //using
          }
          #endregion

          #region PublicContacts
          public async Task<PublicContact[]> AddPublicContactAsync(PublicContact newContact, bool returnNewList)
          {
               using (DataDbContext context = new DataDbContext())
               {
                    context.PublicContacts.Add(newContact);
                    context.SaveChanges();
                    if (returnNewList)
                    {
                         return await GetPublicContactsAsync();
                    }
                    else
                    {
                         return null;
                    }
               }  //using
          }

          public async Task<PublicContact[]> DeletePublicContactAsync(int id, bool returnNewList)
          {
               using (DataDbContext context = new DataDbContext())
               {
                    var existingContact = context.PublicContacts.SingleOrDefault(x => x.Id == id);
                    if (existingContact != null)
                    {
                         context.PublicContacts.Remove(existingContact);
                         context.SaveChanges();
                    }
                    if (returnNewList)
                    {
                         return await GetPublicContactsAsync();
                    }
                    else
                    {
                         return null;
                    }
               }  //using
          }

          public async Task<PublicContact> GetPublicContactAsync(int id)
          {
               using (DataDbContext context = new DataDbContext())
               {
                    PublicContact contact = context.PublicContacts.SingleOrDefault(x => x.Id == id);
                    return contact;
               }
          }

          public async Task<PublicContact[]> GetPublicContactsAsync()
          {
               using (DataDbContext context = new DataDbContext())
               {
                    PublicContact[] contacts = context.PublicContacts.OrderBy(x => x.LastName).ToArray();
                    return contacts;
               }
          }

          public async Task<PublicContact[]> UpdatePublicContactAsync(PublicContact updatedContact, bool returnNewList)
          {
               using (DataDbContext context = new DataDbContext())
               {
                    var existingContact = context.PublicContacts.SingleOrDefault(x => x.Id == updatedContact.Id);
                    if (existingContact != null)
                    {
                         //existingContact = updatedContact;
                         context.Entry(existingContact).CurrentValues.SetValues(updatedContact);
                         context.SaveChanges();
                    }
                    if (returnNewList)
                    {
                         return await GetPublicContactsAsync();
                    }
                    else
                    {
                         return null;
                    }
               }  //using
          }
          #endregion

          #region RetailContacts
          public async Task<RetailContact[]> AddRetailContactAsync(RetailContact newContact, bool returnNewList)
          {
               using (DataDbContext context = new DataDbContext())
               {
                    context.RetailContacts.Add(newContact);
                    context.SaveChanges();
                    if (returnNewList)
                    {
                         return await GetRetailContactsAsync();
                    }
                    else
                    {
                         return null;
                    }
               }  //using
          }

          public async Task<RetailContact[]> DeleteRetailContactAsync(int id, bool returnNewList)
          {
               using (DataDbContext context = new DataDbContext())
               {
                    var existingContact = context.RetailContacts.SingleOrDefault(x => x.Id == id);
                    if (existingContact != null)
                    {
                         context.RetailContacts.Remove(existingContact);
                         context.SaveChanges();
                    }
                    if (returnNewList)
                    {
                         return await GetRetailContactsAsync();
                    }
                    else
                    {
                         return null;
                    }
               }  //using
          }

          public async Task<RetailContact> GetRetailContactAsync(int id)
          {
               using (DataDbContext context = new DataDbContext())
               {
                    RetailContact contact = context.RetailContacts.SingleOrDefault(x => x.Id == id);
                    return contact;
               }
          }

          public async Task<RetailContact[]> GetRetailContactsAsync()
          {
               using (DataDbContext context = new DataDbContext())
               {
                    RetailContact[] contacts = context.RetailContacts.OrderBy(x => x.LastName).ToArray();
                    return contacts;
               }
          }

          public async Task<RetailContact[]> UpdateRetailContactAsync(RetailContact updatedContact, bool returnNewList)
          {
               using (DataDbContext context = new DataDbContext())
               {
                    var existingContact = context.RetailContacts.SingleOrDefault(x => x.Id == updatedContact.Id);
                    if (existingContact != null)
                    {
                         //existingContact = updatedContact;
                         context.Entry(existingContact).CurrentValues.SetValues(updatedContact);
                         context.SaveChanges();
                    }
                    if (returnNewList)
                    {
                         return await GetRetailContactsAsync();
                    }
                    else
                    {
                         return null;
                    }
               }  //using
          }
          #endregion

          #region TaxRates
          public async Task<TaxRate> GetTaxRateAsync(string state)
          {
               using (DataDbContext context = new DataDbContext())
               {
                    TaxRate rate = context.TaxRates.SingleOrDefault(x => x.State == state);
                    if (rate == null)
                    {
                         //failure to return could mean the table is not initialized
                         TaxRate[] rates = context.TaxRates.OrderBy(x => x.State).ToArray();
                         if (rates.Length < 62)
                         {
                              PopulateTaxRates(context);
                              rate = context.TaxRates.SingleOrDefault(x => x.State == state);
                         }
                    }
                    return rate;
               }
          }

          public async Task<TaxRate[]> GetTaxRatesAsync()
          {
               using (DataDbContext context = new DataDbContext())
               {
                    TaxRate[] rates = context.TaxRates.OrderBy(x => x.State).ToArray();
                    if (rates.Length < 62)
                    {
                         PopulateTaxRates(context);
                         rates = context.TaxRates.OrderBy(x => x.State).ToArray();
                    }
                    return rates;
               }
          }

          public async Task<TaxRate[]> UpdateTaxRateAsync(TaxRate updatedRate, bool returnNewList)
          {
               using (DataDbContext context = new DataDbContext())
               {
                    var existingRate = context.TaxRates.SingleOrDefault(x => x.State == updatedRate.State);
                    if (existingRate != null)
                    {
                         //existingRate = updatedRate;
                         context.Entry(existingRate).CurrentValues.SetValues(updatedRate);
                         context.SaveChanges();
                    }
                    if (returnNewList)
                    {
                         return await GetTaxRatesAsync();
                    }
                    else
                    {
                         return null;
                    }
               }  //using
          }

          private void PopulateTaxRates(DataDbContext context)
          {
               context.TaxRates.AddRange(context.DefaultRates);
               context.SaveChanges();
          }
          #endregion

     }  //class
}  //namespace
