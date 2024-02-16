using Core.ViewModels.Machine;
using FluentValidation;

namespace Core.Validators.Machine
{
    public class CarBrandValidator : AbstractValidator<CarBrandViewModel>
    {
        public CarBrandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(x => "Вкажіть назву бренду");
        }
    }
}
