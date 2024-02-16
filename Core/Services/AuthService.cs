using AutoMapper;
using Core.Entities.Identity;
using Core.Helpers;
using Core.Interfaces.Services;
using Core.ViewModels.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Drawing.Imaging;
using System.Text.Json;

namespace Core.Services
{
    /// <summary>
    /// Сервіс для автентифікація користувача
    /// </summary>
    public class AuthService(UserManager<User> userManager,
        ITokenService tokenService,
        ILogger<TokenService> logger,
        IMapper mapper) : IAuthService
    {
        private readonly UserManager<User> _userManager = userManager;
        private readonly ITokenService _tokenService = tokenService;
        private readonly IMapper _mapper = mapper;
        private readonly ILogger<TokenService> _logger = logger;

        /// <summary>
        /// {Interface} Описано в інтерфейсі IAuthService
        /// </summary>
        public async Task<string> LoginAsync(LoginViewModel model)
        {
            try
            {
                User user = await _userManager.FindByEmailAsync(model.Email);

                if (user != null)
                    if (await _userManager.CheckPasswordAsync(user, model.Password))
                        return await _tokenService.CreateToken(user);

                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// {Interface} Описано в інтерфейсі IAuthService
        /// </summary>
        public async Task<string> RegisterAsync(RegisterViewModel model)
        {
            try
            {
                var user = _mapper.Map<User>(model);
                
                var img = model.Photo.FromBase64StringToImage();

                string randomFilename = Path.GetRandomFileName() + ".jpeg";
                var dir = Path.Combine(Directory.GetCurrentDirectory(), "uploads", randomFilename);
                user.Photo = randomFilename;

                img.Save(dir, ImageFormat.Jpeg);

                var result = await _userManager.CreateAsync(user, model.Password);

                if (!result.Succeeded)
                    return null;

                await _userManager.AddToRoleAsync(user, ENV.Roles.User);
                return await _tokenService.CreateToken(user);
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
