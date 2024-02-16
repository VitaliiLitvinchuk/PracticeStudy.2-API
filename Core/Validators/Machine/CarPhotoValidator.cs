using Core.ViewModels.Machine;
using FluentValidation;

namespace Core.Validators.Machine
{
    public class CarPhotoValidator : AbstractValidator<CarPhotoViewModel>
    {
        public CarPhotoValidator()
        {
            RuleFor(x => x.CarId)
                .NotEmpty().WithMessage("Вкажіть якій машині");

            RuleFor(x => x.Photo)
                .NotEmpty().WithMessage("Вкажіть фото");
        }
    }
}
