using FluentValidation;
using GestionProductos.DTOs.Products;

namespace GestionProductos.Validators.Product
{
    public class UpdateProductDtoValidator : AbstractValidator<UpdateProductDto>
    {
        public UpdateProductDtoValidator()
        {
            RuleFor(x => x.Name)
                 .NotEmpty()
                 .WithMessage("Name is required.")
                 .Length(3, 100)
                 .WithMessage("The name must have a minimum three characters and maximum of one hundred");

            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Price must be greater than or equal to 0.");
        }
    }
}
