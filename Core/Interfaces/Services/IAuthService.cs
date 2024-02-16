using Core.ViewModels.Auth;

namespace Core.Interfaces.Services
{
    /// <summary>
    /// Інтерфейс для роботи з AuthService в builder.Service
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Метод авторизації користувача та отримання jwt
        /// </summary>
        /// <param name="model">Email, Password</param>
        /// <returns>Повертає jwt, працює асинхронно</returns>
        Task<string> LoginAsync(LoginViewModel model);
        /// <summary>
        /// Метод реєстрації користувача та отримання jwt
        /// </summary>
        /// <param name="model">Email, FirstName, SecondName, Photo(base64), Phone, Password, ConfirmPassword</param>
        /// <returns>Повертає jwt, працює асинхронно</returns>
        Task<string> RegisterAsync(RegisterViewModel model);
    }
}
