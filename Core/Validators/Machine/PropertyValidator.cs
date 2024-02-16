using Core.ViewModels.Machine;
using FluentValidation;

namespace Core.Validators
{
    /// <summary>
    /// Клас для перевірки властивостей моделі
    /// </summary>
    public class PropertyValidator : AbstractValidator<PropertyViewModel>
    {
        /// <summary>
        /// Перевірка властивостей моделі
        /// </summary>
        public PropertyValidator()
        {
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Вкажіть опис");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Вкажіть назву");
        }
    }
}
