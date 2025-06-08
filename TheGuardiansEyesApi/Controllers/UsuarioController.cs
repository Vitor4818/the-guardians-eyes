using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TheGuardiansEyesModel;
using TheGuardiansEyesBusiness;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Authorization;

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

        /// <summary>
        /// Lista todos os usuários cadastrados.
        /// </summary>
        /// <returns>Lista de usuários.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [SwaggerOperation(Summary = "Lista todos os usuários", Description = "Retorna todos os usuários cadastrados no sistema.")]
        public IActionResult Get()
        {
            var usuarios = usuarioService.ListarUsuarios();
            return usuarios.Count == 0 ? NoContent() : Ok(usuarios);
        }

        /// <summary>
        /// Obtém um usuário pelo ID.
        /// </summary>
        /// <param name="id">ID do usuário.</param>
        /// <returns>Dados do usuário.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Busca um usuário por ID", Description = "Retorna os dados de um usuário específico pelo seu ID.")]
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

        /// <summary>
        /// Obtém um usuário pelo CPF.
        /// </summary>
        /// <param name="cpf">CPF do usuário.</param>
        /// <returns>Dados do usuário.</returns>
        [HttpGet("cpf")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Busca um usuário por CPF", Description = "Retorna os dados de um usuário específico através do CPF.")]
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

        /// <summary>
        /// Cadastra um novo usuário.
        /// </summary>
        /// <param name="usuario">Dados do usuário a ser cadastrado.</param>
        /// <returns>Usuário criado.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Cadastra um novo usuário", Description = "Cadastra um novo usuário com os dados fornecidos.")]
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

        /// <summary>
        /// Atualiza os dados de um usuário existente.
        /// </summary>
        /// <param name="id">ID do usuário a ser atualizado.</param>
        /// <param name="usuario">Dados atualizados do usuário.</param>
        /// <returns>Resposta sem conteúdo.</returns>
       [HttpPut("{id}")]
[ProducesResponseType(StatusCodes.Status204NoContent)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
[SwaggerOperation(Summary = "Atualiza um usuário", Description = "Atualiza os dados de um usuário existente.")]
public IActionResult Put(int id, [FromBody] UsuarioModel usuario)
{
    if (usuario == null)
        return BadRequest("Dados inválidos.");

    try
    {
        usuarioService.AtualizarUsuario(id, usuario);
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


        /// <summary>
        /// Remove um usuário do sistema.
        /// </summary>
        /// <param name="id">ID do usuário a ser removido.</param>
        /// <returns>Resposta sem conteúdo.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Remove um usuário", Description = "Remove um usuário do sistema com base no ID informado.")]
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
