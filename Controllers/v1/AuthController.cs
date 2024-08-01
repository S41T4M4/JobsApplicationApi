using JobApplication.Domain.Models;
using JobApplication.Services;
using JobApplication.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace JobApplication.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IJobRepository _jobRepository;
        private readonly TokenService _tokenService;

        public AuthController(IJobRepository jobRepository, TokenService tokenService)
        {
            _jobRepository = jobRepository;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public IActionResult Auth([FromBody] LoginViewModel loginViewModel)
        {
            if (loginViewModel == null || string.IsNullOrEmpty(loginViewModel.Email) || string.IsNullOrEmpty(loginViewModel.Senha))
            {
                return BadRequest("Credenciais inválidas.");
            }

            var user = _jobRepository.GetUsuariosByEmail(loginViewModel.Email);
            if (user == null || user.senha != loginViewModel.Senha) 
            {
                return Unauthorized("Email ou senha inválidos.");
            }

            var token = _tokenService.GenerateToken(user);

            return Ok(token);
        }
    }
}
