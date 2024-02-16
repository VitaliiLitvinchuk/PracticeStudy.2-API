using Core.ViewModels.Machine;
using FluentValidation;

namespace Core.Validators.Machine
{
    public class CarYearValidator : AbstractValidator<CarYearViewModel>
    {
        public CarYearValidator()
        {
            RuleFor(x => x.YearOfManufacture)
                .NotEmpty().WithMessage("Введіть рік");
        }
    }
}
