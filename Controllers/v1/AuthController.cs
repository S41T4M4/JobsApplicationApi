using Microsoft.Extensions.Logging;
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
        private readonly ILogger<AuthController> _logger;

        public AuthController(IJobRepository jobRepository, TokenService tokenService, ILogger<AuthController> logger)
        {
            _jobRepository = jobRepository;
            _tokenService = tokenService;
            _logger = logger;
        }

        [HttpPost("login")]
        public IActionResult Auth([FromBody] LoginViewModel loginViewModel)
        {
            if (loginViewModel == null || string.IsNullOrEmpty(loginViewModel.Email) || string.IsNullOrEmpty(loginViewModel.Senha))
            {
                _logger.LogWarning("Credenciais inválidas fornecidas.");
                return BadRequest("Credenciais inválidas.");
            }

            var user = _jobRepository.GetUsuariosByEmail(loginViewModel.Email);

            if (user == null || user.senha != loginViewModel.Senha)
            {
                _logger.LogWarning("Tentativa de login falhou para o email: {Email}", loginViewModel.Email);
                return Unauthorized("Email ou senha inválidos.");
            }

            // Determine o perfil do usuário
            bool isRecrutador = user.Empresa != null;

            // Se o usuário for um recrutador, verifique o CNPJ
            if (isRecrutador)
            {
                if (string.IsNullOrEmpty(loginViewModel.Cnpj) || user.Empresa.cnpj != loginViewModel.Cnpj)
                {
                    _logger.LogWarning("CNPJ inválido fornecido para o recrutador: {Email}", loginViewModel.Email);
                    return Unauthorized("CNPJ inválido.");
                }
            }

            // Gera um token para o usuário autenticado
            var token = _tokenService.GenerateToken(user);
            _logger.LogInformation("Usuário autenticado com sucesso: {Email}, Perfil: {Perfil}", user.email, isRecrutador ? "Recrutador" : "Candidato");

            // Retorna o tipo de perfil junto com o token
            return Ok(new { Message = token, perfil = isRecrutador ? "Recrutador" : "Candidato", user.id, user.nome, Cnpj = user.Empresa?.cnpj, user.empresa_id });
        }
    }
}
