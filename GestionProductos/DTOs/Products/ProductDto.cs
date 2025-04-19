using GestionProductos.DTOs.ProductDetails;
using GestionProductos.DTOs.User;

namespace GestionProductos.DTOs.Products
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public UserDto User { get; set; } = new UserDto();

        public List<ProductDetailDto> ProductDetails { get; set; }
    }
}
