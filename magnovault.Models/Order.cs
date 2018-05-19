using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace magnovault.Models
{
     //this version of Order corresponds to the database entity
     public class Order
     {
          public int Id { get; set; }
          [DataType(DataType.Date)]
          [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
          public DateTime OnDate { get; set; }

          public string CustomerId { get; set; }

          [Required]
          public string ShipFirstName { get; set; }
          public string ShipLastName { get; set; }
          [Required]
          public string ShipStreet { get; set; }
          [Required]
          public string ShipCity { get; set; }
          [StringLength(2)]
          [Required]
          public string ShipState { get; set; }
          [DataType(DataType.PostalCode)]
          [Required]
          public string ShipZip { get; set; }

          public int? Campaign { get; set; }

          [DataType(DataType.Currency)]
          [DisplayFormat(DataFormatString = "{0:c}", ApplyFormatInEditMode = true)]
          public decimal Subtotal { get; set; }
          [DataType(DataType.Currency)]
          [DisplayFormat(DataFormatString = "{0:c}", ApplyFormatInEditMode = true)]
          public decimal ShipHand { get; set; }
          [DataType(DataType.Currency)]
          [DisplayFormat(DataFormatString = "{0:c}", ApplyFormatInEditMode = true)]
          public decimal Tax { get; set; }
          [DataType(DataType.Currency)]
          [DisplayFormat(DataFormatString = "{0:c}", ApplyFormatInEditMode = true)]
          public decimal Adjustment { get; set; }
          [DataType(DataType.Currency)]
          [DisplayFormat(DataFormatString = "{0:c}", ApplyFormatInEditMode = true)]
          public decimal Total { get; set; }

          public string TransactionId { get; set; }
          public string ResponseCode { get; set; }
          public string AuthorizationCode { get; set; }
          public string CardLastFour { get; set; }

          public bool Deferred { get; set; }
          public bool Refunded { get; set; }
          public bool Done { get; set; }
     }
}