using TheGuardiansEyesModel;
using TheGuardiansEyesData;
using Microsoft.EntityFrameworkCore;

namespace TheGuardiansEyesBusiness
{
    public class UsuarioService
    {
        private readonly AppDbContext _context;

        public UsuarioService(AppDbContext context)
        {
            _context = context;
        }

        // LISTAR TODOS
        public List<UsuarioModel> ListarUsuarios()
        {
            return _context.Usuarios
                .Include(u => u.Desastres) // Inclui os desastres relacionados
                .ToList();
        }

        // OBTER POR ID
        public UsuarioModel? ObterPorId(int id)
        {
            return _context.Usuarios
                .Include(u => u.Desastres)
                .FirstOrDefault(u => u.Id == id);
        }

        // OBTER POR CPF
        public UsuarioModel? ObterPorCpf(string cpf)
        {
            return _context.Usuarios
                .Include(u => u.Desastres)
                .FirstOrDefault(u => u.Cpf == cpf);
        }

        // CADASTRAR
        public UsuarioModel CadastrarUsuario(UsuarioModel usuario)
        {
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
            return usuario;
        }

        // ATUALIZAR
        public bool AtualizarUsuario(UsuarioModel usuario)
        {
            var existente = _context.Usuarios.Find(usuario.Id);
            if (existente == null) return false;

            existente.Nome = usuario.Nome;
            existente.Sobrenome = usuario.Sobrenome;
            existente.Cpf = usuario.Cpf;
            existente.Cargo = usuario.Cargo;
            existente.Funcao = usuario.Funcao;
            existente.Email = usuario.Email;
            existente.Senha = usuario.Senha;

            _context.Usuarios.Update(existente);
            _context.SaveChanges();
            return true;
        }

        // REMOVER
        public bool RemoverUsuario(int id)
        {
            var usuario = _context.Usuarios.Find(id);
            if (usuario == null) return false;

            _context.Usuarios.Remove(usuario);
            _context.SaveChanges();
            return true;
        }
    }
}
