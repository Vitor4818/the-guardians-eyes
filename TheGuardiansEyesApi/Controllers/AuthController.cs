using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Annotations;
using TheGuardiansEyesBusiness;
using TheGuardiansEyesModel;
using Microsoft.AspNetCore.Http;

namespace TheGuardiansEyesApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Realiza o login do usuário.
        /// </summary>
        /// <param name="loginDto">Credenciais de login (e-mail e senha).</param>
        /// <returns>Token de autenticação e dados do usuário.</returns>
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(Summary = "Realiza login", Description = "Autentica o usuário com e-mail e senha, retornando um token JWT e os dados do usuário.")]
        public IActionResult Login([FromBody] LoginDTO loginDto)
        {
            var (usuario, token) = _authService.Login(loginDto.Email, loginDto.Senha);

            if (usuario == null)
                return Unauthorized(new { mensagem = "E-mail ou senha inválidos." });

            return Ok(new
            {
                mensagem = "Login realizado com sucesso",
                token,
                usuario.Id,
                usuario.Nome,
                usuario.Email
            });
        }

        /// <summary>
        /// Retorna o perfil do usuário autenticado.
        /// </summary>
        /// <returns>Nome do usuário autenticado.</returns>
        [Authorize]
        [HttpGet("perfil")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Retorna o perfil do usuário", Description = "Retorna uma mensagem de boas-vindas com o nome do usuário autenticado.")]
        public IActionResult Perfil()
        {
            var userName = User.Identity?.Name;
            return Ok(new { mensagem = $"Bem-vindo, {userName}" });
        }
    }
}