using Core.ViewModels.Machine;
using FluentValidation;

namespace Core.Validators.Machine
{
    public class CharacteristicValidator : AbstractValidator<CharacteristicViewModel>
    {
        public CharacteristicValidator()
        {
            RuleFor(x => x)
                .NotEmpty().WithMessage("Лінь обробляти все");
        }
    }
}
