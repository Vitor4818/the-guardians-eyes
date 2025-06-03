using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using TheGuardiansEyesModel;
using TheGuardiansEyesBusiness;
using Swashbuckle.AspNetCore.Annotations;

namespace TheGuardiansEyesApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GrupoDesastreController : ControllerBase
    {
        private readonly GrupoDesastreService _grupoService;

        public GrupoDesastreController(GrupoDesastreService grupoService)
        {
            _grupoService = grupoService;
        }

        /// <summary>
        /// Lista todos os grupos de desastre cadastrados.
        /// </summary>
        /// <returns>Lista de grupos de desastre.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [SwaggerOperation(Summary = "Lista todos os grupos de desastre", Description = "Retorna todos os grupos de desastre cadastrados no sistema.")]
        public IActionResult Get()
        {
            var grupos = _grupoService.ListarGrupos();
            return grupos.Count == 0 ? NoContent() : Ok(grupos);
        }

        /// <summary>
        /// Obtém um grupo de desastre pelo ID.
        /// </summary>
        /// <param name="id">ID do grupo de desastre.</param>
        /// <returns>Dados do grupo de desastre.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(Summary = "Busca um grupo de desastre por ID", Description = "Retorna os dados de um grupo de desastre específico pelo seu ID.")]
        public IActionResult Get(int id)
        {
            try
            {
                var grupo = _grupoService.ObterPorId(id);
                return Ok(grupo);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Cadastra um novo grupo de desastre.
        /// </summary>
        /// <param name="grupo">Dados do grupo de desastre a ser cadastrado.</param>
        /// <returns>Grupo de desastre criado.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(Summary = "Cadastra um novo grupo de desastre", Description = "Cadastra um novo grupo de desastre com os dados fornecidos.")]
        public IActionResult Post([FromBody] GrupoDesastreModel grupo)
        {
            if (string.IsNullOrWhiteSpace(grupo.NomeGrupo))
                return BadRequest("O nome do grupo é obrigatório.");

            try
            {
                var criado = _grupoService.CadastrarGrupo(grupo);
                return CreatedAtAction(nameof(Get), new { id = criado.Id }, criado);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Atualiza os dados de um grupo de desastre existente.
        /// </summary>
        /// <param name="id">ID do grupo de desastre a ser atualizado.</param>
        /// <param name="grupo">Dados atualizados do grupo de desastre.</param>
        /// <returns>Resposta sem conteúdo.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(Summary = "Atualiza um grupo de desastre", Description = "Atualiza os dados de um grupo de desastre existente.")]
        public IActionResult Put(int id, [FromBody] GrupoDesastreModel grupo)
        {
            if (grupo == null || grupo.Id != id)
                return BadRequest("Dados inconsistentes.");

            try
            {
                var atualizado = _grupoService.AtualizarGrupo(grupo);
                return atualizado ? NoContent() : NotFound();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Remove um grupo de desastre do sistema.
        /// </summary>
        /// <param name="id">ID do grupo de desastre a ser removido.</param>
        /// <returns>Resposta sem conteúdo.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(Summary = "Remove um grupo de desastre", Description = "Remove um grupo de desastre do sistema com base no ID informado.")]
        public IActionResult Delete(int id)
        {
            try
            {
                var removido = _grupoService.RemoverGrupo(id);
                return removido ? NoContent() : NotFound();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
