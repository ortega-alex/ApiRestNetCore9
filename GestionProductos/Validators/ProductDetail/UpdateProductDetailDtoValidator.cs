using FluentValidation;
using GestionProductos.DTOs.ProductDetails;

namespace GestionProductos.Validators.ProductDetail
{
    public class UpdateProductDetailDtoValidator:AbstractValidator<UpdateProductDetailDto>
    {
        public UpdateProductDetailDtoValidator() {
              RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Description is required.")
                .MaximumLength(400);

            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Price must be greater than or equal to 0.");
        }
    }
}
