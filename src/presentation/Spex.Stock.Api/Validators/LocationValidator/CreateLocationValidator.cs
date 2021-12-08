using Application.CQRS.Commands.LocationCommand;
using FluentValidation;

namespace Spex.Stock.Api.Validators.LocationValidator
{
    public class CreateLocationValidator : AbstractValidator<CreateLocationCommand>
    {
        public CreateLocationValidator()
        {
            RuleFor(x => x.Location).MaximumLength(300);
        }
    }
}
