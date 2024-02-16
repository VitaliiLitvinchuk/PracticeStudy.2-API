using Core.ViewModels.Auth;
using FluentValidation;

namespace Core.Validators
{
    /// <summary>
    /// Клас для перевірки властивостей моделі
    /// </summary>
    public class LoginValidator : AbstractValidator<LoginViewModel>
    {
        /// <summary>
        /// Перевірка властивостей моделі
        /// </summary>
        public LoginValidator()
        {
            RuleFor(x => x.Email)
               .NotEmpty().WithMessage("Поле пошта є обов'язковим!")
               .EmailAddress().WithMessage("Пошта є не коректною!");

            RuleFor(x => x.Password)
                .NotEmpty().WithName("Password").WithMessage("Поле пароль є обов'язковим!");
        }
    }
}
