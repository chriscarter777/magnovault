using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using magnovault.Models;

namespace magnovault.Data
{
     public interface IMagnovaultDbRepository
     {
          Task<BusinessRules> GetBusinessRulesAsync();
          Task<BusinessRules> UpdateBusinessRulesAsync(BusinessRules businessrules, bool returnNewRules);

          Task<Customer> GetCustomerAsync(string user);
          Task<Customer[]> GetCustomersAsync();
          Task<Customer[]> UpdateCustomerAsync(Customer customer, bool returnNewList);

          Task<InvestorContact[]> AddInvestorContactAsync(InvestorContact contact, bool returnNewList);
          Task<InvestorContact[]> DeleteInvestorContactAsync(int id, bool returnNewList);
          Task<InvestorContact> GetInvestorContactAsync(int id);
          Task<InvestorContact[]> GetInvestorContactsAsync();
          Task<InvestorContact[]> UpdateInvestorContactAsync(InvestorContact contact, bool returnNewList);

          Task<MediaContact[]> AddMediaContactAsync(MediaContact contact, bool returnNewList);
          Task<MediaContact[]> DeleteMediaContactAsync(int id, bool returnNewList);
          Task<MediaContact> GetMediaContactAsync(int id);
          Task<MediaContact[]> GetMediaContactsAsync();
          Task<MediaContact[]> UpdateMediaContactAsync(MediaContact contact, bool returnNewList);

          Task<ViewOrder[]> AddOrderAsync(ViewOrder order, bool returnNewList);
          Task<ViewOrder[]> DeleteOrderAsync(int id, bool returnNewList);
          Task<ViewOrder> GetDeferredOrderAsync(string userId);
          Task<ViewOrder> GetOrderAsync(int id);
          Task<ViewOrder[]> GetOrdersAsync();
          Task<ViewOrder[]> UpdateOrderAsync(ViewOrder order, bool returnNewList);

          Task<Product[]> AddProductAsync(Product product, bool returnAllProds, bool returnActiveProds);
          Task<Product[]> DeleteProductAsync(int id, bool returnAllProds, bool returnActiveProds);
          Task<Product> GetProductAsync(int id);
          Task<Product[]> GetProductsAsync(bool activeOnly);
          Task<Product[]> UpdateProductAsync(Product product, bool returnAllProds, bool returnActiveProds);

          Task<PublicContact[]> AddPublicContactAsync(PublicContact contact, bool returnNewList);
          Task<PublicContact[]> DeletePublicContactAsync(int id, bool returnNewList);
          Task<PublicContact> GetPublicContactAsync(int id);
          Task<PublicContact[]> GetPublicContactsAsync();
          Task<PublicContact[]> UpdatePublicContactAsync(PublicContact contact, bool returnNewList);

          Task<RetailContact[]> AddRetailContactAsync(RetailContact contact, bool returnNewList);
          Task<RetailContact[]> DeleteRetailContactAsync(int id, bool returnNewList);
          Task<RetailContact> GetRetailContactAsync(int id);
          Task<RetailContact[]> GetRetailContactsAsync();
          Task<RetailContact[]> UpdateRetailContactAsync(RetailContact contact, bool returnNewList);

          Task<TaxRate> GetTaxRateAsync(string state);
          Task<TaxRate[]> GetTaxRatesAsync();
          Task<TaxRate[]> UpdateTaxRateAsync(TaxRate rate, bool returnNewList);
     }
}