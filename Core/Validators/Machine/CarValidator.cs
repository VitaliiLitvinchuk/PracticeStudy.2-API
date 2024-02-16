using Core.ViewModels.Machine;
using FluentValidation;

namespace Core.Validators.Machine
{
    public class CarValidator : AbstractValidator<CarViewModel>
    {
        public CarValidator()
        {
            RuleFor(x => x).NotEmpty().WithMessage("Лінь обробляти все");
        }
    }
}
