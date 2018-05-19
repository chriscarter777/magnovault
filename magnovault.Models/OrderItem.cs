using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace magnovault.Models
{
     public class OrderItem
     {
          public int Id { get; set; }
          public int OrderId { get; set; }
          public int ProductId { get; set; }

          [DataType(DataType.Currency)]
          [DisplayFormat(DataFormatString = "{0:c}", ApplyFormatInEditMode = true)]
          public decimal UnitPrice { get; set; }

          public int Quantity { get; set; }

          [DataType(DataType.Currency)]
          [DisplayFormat(DataFormatString = "{0:c}", ApplyFormatInEditMode = true)]
          public decimal LineTotal { get; set; }
     }
}
