using FluentValidation;
using static WebApi.src.Domain.Contract.ProductsContracts;

namespace WebApi.src.Domain.Validations
{
    public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
    {
        public CreateProductRequestValidator() 
        {
            RuleFor(x => x.Name).NotEmpty().WithName("Name is required");
            RuleFor(x => x.Price).NotEmpty().WithName("Price is required");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
        }

    }
}
