using Microsoft.AspNetCore.Mvc;
using TheGuardiansEyesModel;
using TheGuardiansEyesBusiness;

namespace UsuariosApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService usuarioService;

        public UsuarioController(UsuarioService usuarioService)
        {
            this.usuarioService = usuarioService;
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
            var usuario = usuarioService.ObterPorId(id);
            return usuario == null ? NotFound() : Ok(usuario);
        }

        [HttpGet("nome")]
        public IActionResult GetPorCpf(string cpf)
        {
            var usuario = usuarioService.ObterPorCpf(cpf);
            return usuario == null ? NotFound() : Ok(usuario);
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

            var criada = usuarioService.CadastrarUsuario(usuario);
            return CreatedAtAction(nameof(Get), new { id = criada.Id }, criada); // Corrigido aqui
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UsuarioModel usuario)
        {
            if (usuario == null || usuario.Id != id) // Corrigido aqui
                return BadRequest("Dados inconsistentes.");

            return usuarioService.AtualizarUsuario(usuario) ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return usuarioService.RemoverUsuario(id) ? NoContent() : NotFound();
        }
    }
}
