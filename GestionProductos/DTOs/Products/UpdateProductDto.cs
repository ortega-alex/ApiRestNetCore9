using GestionProductos.DTOs.ProductDetails;
using System.ComponentModel.DataAnnotations;

namespace GestionProductos.DTOs.Products
{
    public class UpdateProductDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        public List<UpdateProductDetailDto> ProductDetails { get; set; }
    }
}
