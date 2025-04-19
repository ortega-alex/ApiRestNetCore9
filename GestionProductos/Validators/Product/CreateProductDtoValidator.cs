using FluentValidation;
using GestionProductos.DTOs.Products;
using GestionProductos.Validators.ProductDetail;

namespace GestionProductos.Validators.Product
{
    public class CreateProductDtoValidator: AbstractValidator<CreateProductDto>
    {
        public CreateProductDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is required.")
                .Length(3, 100)
                .WithMessage("The name must have a minimum three characters and maximum of one hundred");

            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Price must be greater than or equal to 0.");

            RuleForEach(x => x.ProductDetails)
                .SetValidator(new CreateProductDetailDtoValidator());
        }
    }
}
