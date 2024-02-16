using Core.Interfaces.Services;
using Core.ViewModels.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace PracticeStudy._2.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class AuthController(IAuthService authService, ITokenService tokenService, ILogger<AuthController> logger) : ControllerBase
    {
        private readonly IAuthService _authService = authService;
        private readonly ITokenService _tokenService = tokenService;
        private readonly ILogger<AuthController> _logger = logger;

        /// <summary>
        /// Реєстрація [Unauthorize]
        /// </summary>
        /// <param name="model">Пошта, ім'я, пароль, повторний пароль</param>
        /// <returns>Jwt</returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            var token = await _authService.RegisterAsync(model);
            if (string.IsNullOrEmpty(token))
                return BadRequest(new { errors = new { authError = "Реєстрація неуспішна" } });

            return Ok(new { token = token });
        }

        /// <summary>
        /// Вхід [Unauthorize]
        /// </summary>
        /// <param name="model">Пошта, пароль</param>
        /// <returns>Jwt</returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            string token = await _authService.LoginAsync(model);

            if (string.IsNullOrEmpty(token))
                return BadRequest(new { errors = new { authError = "Авторизація неуспішна" } });

            return Ok(new { token = token });
        }

        /// <summary>
        /// Вихід [Authorize]
        /// </summary>
        /// <returns>Jwt</returns>
        [HttpPost]
        [Route("logout")]
        public IActionResult Logout()
        {
            return Ok(new { token = "" });
        }

        /// <summary>
        /// Перевіряє jwt [Unauthorize]
        /// </summary>
        /// <returns>Jwt</returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("validate-jwt")]
        public async Task<IActionResult> ValidateJWT([FromBody] string token)
        {
            return Ok(new { status = await _tokenService.ValidateToken(token) });
        }
    }
}
