using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TheGuardiansEyesModel;
using TheGuardiansEyesBusiness;

namespace UsuariosApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService usuarioService;
        private readonly ILogger<UsuarioController> _logger;

        public UsuarioController(UsuarioService usuarioService, ILogger<UsuarioController> logger)
        {
            this.usuarioService = usuarioService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var usuarios = usuarioService.ListarUsuarios();
            return usuarios.Count == 0 ? NoContent() : Ok(usuarios);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var usuario = usuarioService.ObterPorId(id);
                return Ok(usuario);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Usuário não encontrado.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar usuário por ID.");
                return StatusCode(500, "Ocorreu um erro interno ao buscar o usuário.");
            }
        }

        [HttpGet("cpf")]
        public IActionResult GetPorCpf(string cpf)
        {
            try
            {
                var usuario = usuarioService.ObterPorCpf(cpf);
                return Ok(usuario);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Usuário com o CPF informado não foi encontrado.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar usuário por CPF.");
                return StatusCode(500, "Ocorreu um erro interno ao buscar o usuário.");
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] UsuarioModel usuario)
        {
            if (string.IsNullOrWhiteSpace(usuario.Nome) ||
                string.IsNullOrWhiteSpace(usuario.Sobrenome) ||
                string.IsNullOrWhiteSpace(usuario.Cpf) ||
                string.IsNullOrWhiteSpace(usuario.Cargo) ||
                string.IsNullOrWhiteSpace(usuario.Funcao) ||
                string.IsNullOrWhiteSpace(usuario.Email) ||
                string.IsNullOrWhiteSpace(usuario.Senha))
            {
                return BadRequest("Todos os campos são obrigatórios.");
            }

            try
            {
                var criada = usuarioService.CadastrarUsuario(usuario);
                return CreatedAtAction(nameof(Get), new { id = criada.Id }, criada);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Erro ao cadastrar usuário.");
                return BadRequest("Não foi possível cadastrar o usuário. Verifique se o CPF já está cadastrado ou se os dados estão corretos.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao cadastrar usuário.");
                return StatusCode(500, "Erro inesperado. Tente novamente mais tarde.");
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UsuarioModel usuario)
        {
            if (usuario == null || usuario.Id != id)
                return BadRequest("Dados inconsistentes.");

            try
            {
                usuarioService.AtualizarUsuario(usuario);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Usuário não encontrado para atualização.");
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Erro ao atualizar usuário.");
                return BadRequest("Erro ao atualizar o usuário. Verifique os dados fornecidos.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao atualizar usuário.");
                return StatusCode(500, "Erro inesperado. Tente novamente mais tarde.");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                usuarioService.RemoverUsuario(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Usuário não encontrado para exclusão.");
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Erro ao excluir usuário.");
                return BadRequest("Erro ao excluir o usuário. Verifique se ele está vinculado a outros dados.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao excluir usuário.");
                return StatusCode(500, "Erro inesperado. Tente novamente mais tarde.");
            }
        }
    }
}
