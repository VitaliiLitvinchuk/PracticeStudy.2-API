using Core.Entities.Identity;

namespace Core.Interfaces.Services
{
    /// <summary>
    /// Інтерфейс для роботи з TokenService в builder.Service
    /// </summary>
    public interface ITokenService
    {
        /// <summary>
        /// Генерує токен
        /// </summary>
        /// <param name="user">Користувач (з бази даних)</param>
        /// <returns>jwt</returns>
        Task<string> CreateToken(User user);
        /// <summary>
        /// Перевіряє jwt
        /// </summary>
        /// <param name="token">jwt</param>
        /// <returns>true - правильний, false - неправильний</returns>
        Task<bool> ValidateToken(string token);
    }
}
