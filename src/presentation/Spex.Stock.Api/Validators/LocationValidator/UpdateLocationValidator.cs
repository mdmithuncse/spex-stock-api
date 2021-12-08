using Application.CQRS.Commands.LocationCommand;
using FluentValidation;

namespace Spex.Stock.Api.Validators.LocationValidator
{
    public class UpdateLocationValidator : AbstractValidator<UpdateLocationCommand>
    {
        public UpdateLocationValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage($"{nameof(UpdateLocationCommand.Id)} should be greater than zero.");
            RuleFor(x => x.Location).MaximumLength(300);
        }
    }
}
