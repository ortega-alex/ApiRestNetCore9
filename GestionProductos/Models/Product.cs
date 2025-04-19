using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionProductos.Models
{
    public class Product: BaseEntity
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal Price { get; set; }

        public int? UserId { get; set; }
        [ForeignKey("UserId")]
        public User? User { get; set; }

        public ICollection<ProductDetail> ProductDetails { get; set; } = new List<ProductDetail>();
    }
}
