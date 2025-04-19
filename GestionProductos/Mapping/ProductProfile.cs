using AutoMapper;
using GestionProductos.DTOs.ProductDetails;
using GestionProductos.DTOs.Products;
using GestionProductos.Models;

namespace GestionProductos.Mapping
{
    public class ProductProfile: Profile
    {
        public ProductProfile() {

            // Model -> DTO
            CreateMap<Product, ProductDto>();
            CreateMap<ProductDetail, ProductDetailDto>();

            // DTO -> Model
            CreateMap<CreateProductDto, Product>();
            CreateMap<UpdateProductDto, Product>();
            CreateMap<CreateProductDetailDto, ProductDetail>();
            CreateMap<UpdateProductDetailDto, ProductDetail>();
        }
    }
}
