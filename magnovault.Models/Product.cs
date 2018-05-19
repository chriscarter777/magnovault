using System.ComponentModel.DataAnnotations;

namespace magnovault.Models
{
     public class Product
     {
          public int Id { get; set; }
          public bool Active { get; set; }
          public string Name { get; set; }
          public string ShortDescr { get; set; }
          [DataType(DataType.MultilineText)]
          public string LongDescr { get; set; }
          [DataType(DataType.ImageUrl)]
          public string ImagePath { get; set; }

          [DataType(DataType.Currency)]
          [DisplayFormat(DataFormatString = "{0:c}", ApplyFormatInEditMode = true)]
          public decimal Price { get; set; }
     }
}
