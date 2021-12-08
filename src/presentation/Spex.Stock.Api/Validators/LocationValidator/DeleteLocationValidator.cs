using Application.CQRS.Commands.LocationCommand;
using FluentValidation;

namespace Spex.Stock.Api.Validators.LocationValidator
{
    public class DeleteLocationValidator : AbstractValidator<DeleteLocationByIdCommand>
    {
        public DeleteLocationValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage($"{nameof(DeleteLocationByIdCommand.Id)} should be greater than zero.");
        }
    }
}
