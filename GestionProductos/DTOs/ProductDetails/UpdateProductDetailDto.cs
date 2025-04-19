using System.ComponentModel.DataAnnotations;

namespace GestionProductos.DTOs.ProductDetails
{
    public class UpdateProductDetailDto
    {
        [Required]
        [StringLength(400)]
        public string Description { get; set; }
        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }
    }
}
