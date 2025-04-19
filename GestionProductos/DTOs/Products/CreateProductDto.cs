using GestionProductos.DTOs.ProductDetails;
using System.ComponentModel.DataAnnotations;

namespace GestionProductos.DTOs.Products
{
    public class CreateProductDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        public List<CreateProductDetailDto> ProductDetails { get; set; }
    }
}
