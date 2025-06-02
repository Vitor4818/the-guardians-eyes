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
                .Include(u => u.Desastres)
                .ToList();
        }

        // OBTER POR ID
        public UsuarioModel ObterPorId(int id)
        {
            var usuario = _context.Usuarios
                .Include(u => u.Desastres)
                .FirstOrDefault(u => u.Id == id);

            if (usuario == null)
                throw new KeyNotFoundException("Usuário não encontrado.");

            return usuario;
        }

        // OBTER POR CPF
        public UsuarioModel ObterPorCpf(string cpf)
        {
            var usuario = _context.Usuarios
                .Include(u => u.Desastres)
                .FirstOrDefault(u => u.Cpf == cpf);

            if (usuario == null)
                throw new KeyNotFoundException("Usuário com CPF não encontrado.");

            return usuario;
        }

        // CADASTRAR
        public UsuarioModel CadastrarUsuario(UsuarioModel usuario)
        {
            try
            {
                _context.Usuarios.Add(usuario);
                _context.SaveChanges();
                return usuario;
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException("Erro ao cadastrar usuário. Verifique se o CPF já existe ou se há restrições de integridade.", ex);
            }
        }

        // ATUALIZAR
        public UsuarioModel AtualizarUsuario(UsuarioModel usuario)
        {
            var existente = _context.Usuarios.Find(usuario.Id);
            if (existente == null)
                throw new KeyNotFoundException("Usuário para atualização não encontrado.");

            existente.Nome = usuario.Nome;
            existente.Sobrenome = usuario.Sobrenome;
            existente.Cpf = usuario.Cpf;
            existente.Cargo = usuario.Cargo;
            existente.Funcao = usuario.Funcao;
            existente.Email = usuario.Email;
            existente.Senha = usuario.Senha;

            try
            {
                _context.Usuarios.Update(existente);
                _context.SaveChanges();
                return existente;
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException("Erro ao atualizar o usuário. Verifique os dados fornecidos.", ex);
            }
        }

        // REMOVER
        public void RemoverUsuario(int id)
        {
            var usuario = _context.Usuarios.Find(id);
            if (usuario == null)
                throw new KeyNotFoundException("Usuário para exclusão não encontrado.");

            try
            {
                _context.Usuarios.Remove(usuario);
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException("Erro ao excluir o usuário. Verifique se ele está vinculado a outros dados.", ex);
            }
        }
    }
}
