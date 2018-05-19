using magnovault.Models;
using System.Linq;

namespace magnovault.Utilities
{
     //the OrderCalculator class provides a consistent 'single-source-of-truth' calculation of order prices for all clients
     public static class OrderCalculator
     {
          public static AjaxOrder CalculateOrder(AjaxOrder order, BusinessRules busRules, TaxRate[] taxRates)
          {
               for (int j = 0; j < order.Items.Length; j++)
               {
                    order.Items[j] = CalculateOrderItem(order.Items[j], order.Campaign, busRules);
               }
               order.Subtotal = SubtotalOrder(order, busRules);
               order.ShipHand = ShipHandOrder(order, busRules);
               order.Tax = TaxOrder(order, busRules, taxRates);
               order.Adjustment = AdjustOrder(order, busRules);
               order.Total = TotalOrder(order, busRules);
               return order;
          }  //CalculateOrder

          private static AjaxOrderItem CalculateOrderItem(AjaxOrderItem item, int? camapaign, BusinessRules rules)
          {
               //this method allows special line item pricing to be applied based on AjaxOrder.campaign or Business Rules (no implementation currently).
               item.LineTotal = item.UnitPrice * item.Quantity;
               return item;
          }  //CalculateOrderItem

          private static decimal SubtotalOrder(AjaxOrder order, BusinessRules rules)
          {
               decimal subtotal = 0;
               foreach (AjaxOrderItem item in order.Items)
               {
                    subtotal += item.LineTotal;
               }
               return subtotal;

          }  //SubtotalOrder

          private static decimal ShipHandOrder(AjaxOrder order, BusinessRules rules)
          {
               int totalquantity = 0;
               foreach (AjaxOrderItem item in order.Items)
               {
                    totalquantity += item.Quantity;
               }

               if (rules.ShipHandPerItem)
               {
                    return rules.ShipHandAmount * totalquantity;
               }
               else
               {
                    return (totalquantity > 0) ? rules.ShipHandAmount : 0;
               }
          }  //ShipHandOrder

          private static decimal TaxOrder(AjaxOrder order, BusinessRules rules, TaxRate[] taxRates)
          {
               TaxRate applicableTaxRate = taxRates.SingleOrDefault(x => x.State == order.ShipState);
               if (applicableTaxRate.AppliesToShipHand)
               {
                    return (order.Subtotal + order.ShipHand) * applicableTaxRate.Rate;
               }
               else
               {
                    return order.Subtotal * applicableTaxRate.Rate;
               }
          }  //TaxOrder

          private static decimal AdjustOrder(AjaxOrder order, BusinessRules rules)
          {
               //this method allows special order pricing to be applied based on order.campaign or Business Rules (no implementation currently).
               decimal adjustment = 0;
               return adjustment;
          }  //TaxOrder

          private static decimal TotalOrder(AjaxOrder order, BusinessRules rules)
          {
               return order.Subtotal + order.Tax + order.ShipHand + order.Adjustment;

          }  //TotalOrder
     }
}