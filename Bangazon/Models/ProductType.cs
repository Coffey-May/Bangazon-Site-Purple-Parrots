using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bangazon.Models
{
  public class ProductType
  {
    [Key]
    public int ProductTypeId { get; set; }

   
    public string Label { get; set; }

    [NotMapped]
    public int Quantity { get; set; }

   public virtual ICollection<Product> Products { get; set; }
  }
}
