using System;
using System.ComponentModel;
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
          [DisplayName("Date")]
          public DateTime OnDate { get; set; }

          public string CustomerId { get; set; }

          [Required]
          [DisplayName("First Name")]
          public string ShipFirstName { get; set; }
          [DisplayName("Last Name")]
          public string ShipLastName { get; set; }
          [Required]
          [DisplayName("Street")]
          public string ShipStreet { get; set; }
          [Required]
          [DisplayName("City")]
          public string ShipCity { get; set; }
          [StringLength(2)]
          [Required]
          [DisplayName("State")]
          public string ShipState { get; set; }
          [DataType(DataType.PostalCode)]
          [Required]
          [DisplayName("Zip")]
          public string ShipZip { get; set; }

          public int? Campaign { get; set; }

          [DataType(DataType.Currency)]
          [DisplayFormat(DataFormatString = "{0:c}", ApplyFormatInEditMode = true)]
          public decimal Subtotal { get; set; }
          [DataType(DataType.Currency)]
          [DisplayFormat(DataFormatString = "{0:c}", ApplyFormatInEditMode = true)]
          [DisplayName("S & H")]
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