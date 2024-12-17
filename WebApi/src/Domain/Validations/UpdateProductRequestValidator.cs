using FluentValidation;
using static WebApi.src.Domain.Contract.ProductsContracts;

namespace WebApi.src.Domain.Validations
{
    public class UpdateProductRequestValidator : AbstractValidator<UpdateProductRequest>
    {

        public UpdateProductRequestValidator()
        { 
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
        }

    }
}
