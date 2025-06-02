using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TheGuardiansEyesModel;
using TheGuardiansEyesBusiness;
using System;
using System.Collections.Generic;

namespace TheGuardiansEyesApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImpactoController : ControllerBase
    {
        private readonly ImpactoService impactoService;
        private readonly ILogger<ImpactoController> _logger;

        public ImpactoController(ImpactoService impactoService, ILogger<ImpactoController> logger)
        {
            this.impactoService = impactoService;
            _logger = logger;
        }

        // GET: api/impacto
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var impactos = impactoService.ListarImpactos();
                return impactos.Count == 0 ? NoContent() : Ok(impactos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao listar impactos.");
                return StatusCode(500, "Erro interno ao buscar impactos.");
            }
        }

[HttpGet("{id}")]
public IActionResult GetById(int id)
{
    try
    {
        var impacto = impactoService.ObterPorId(id);
        if (impacto == null)
            return NotFound("Impacto não encontrado.");
        return Ok(impacto);
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, $"Erro ao buscar impacto com ID {id}.");
        return StatusCode(500, "Erro interno ao buscar o impacto.");
    }
}

        // POST: api/impacto
[HttpPost]
public IActionResult Post([FromBody] ImpactoModel impacto)
{
    try
    {
        if (impacto == null || impacto.ImpactoClassificacaoId <= 0)
            return BadRequest("Dados inválidos.");

        var criado = impactoService.CadastrarImpacto(impacto);
        return CreatedAtAction(nameof(Get), new { id = criado.Id }, criado);
    }
    catch (InvalidOperationException ex)
    {
        _logger.LogWarning(ex, "Erro de validação ao cadastrar impacto.");
        return BadRequest(ex.Message); // ou UnprocessableEntity(ex.Message)
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "Erro inesperado ao cadastrar impacto.");
        return StatusCode(500, "Erro interno ao cadastrar impacto.");
    }
}

// PUT: api/impacto/{id}
[HttpPut("{id}")]
public IActionResult Put(int id, [FromBody] ImpactoModel impacto)
{
    try
    {
        if (impacto == null || impacto.Id != id)
            return BadRequest("Dados inconsistentes.");

        impactoService.AtualizarImpacto(impacto);
        return NoContent(); // se não lançar exceção, deu certo
    }
    catch (KeyNotFoundException ex)
    {
        _logger.LogWarning(ex, "Impacto para atualização não encontrado.");
        return NotFound(ex.Message);
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, $"Erro ao atualizar impacto com ID {id}.");
        return StatusCode(500, "Erro interno ao atualizar impacto.");
    }
}

// DELETE: api/impacto/{id}
[HttpDelete("{id}")]
public IActionResult Delete(int id)
{
    try
    {
        impactoService.RemoverImpacto(id);
        return NoContent();
    }
    catch (KeyNotFoundException ex)
    {
        _logger.LogWarning(ex, "Tentativa de exclusão de impacto inexistente.");
        return NotFound(ex.Message);
    }
    catch (InvalidOperationException ex)
    {
        _logger.LogWarning(ex, "Falha de regra de negócio ao excluir impacto.");
        return BadRequest(ex.Message); // ou UnprocessableEntity(ex.Message)
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, $"Erro inesperado ao excluir impacto com ID {id}.");
        return StatusCode(500, "Erro interno ao remover impacto.");
    }
}
    }
}
