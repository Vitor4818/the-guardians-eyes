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
public UsuarioModel CadastrarUsuario(UsuarioModel usuario)
{
    // Evita gerar SQL com True/False no Oracle
    var usuarioExistente = _context.Usuarios
        .FirstOrDefault(u => u.Cpf == usuario.Cpf || u.Email == usuario.Email);

    if (usuarioExistente != null)
        throw new InvalidOperationException("Já existe um usuário com o mesmo CPF ou e-mail.");

    try
    {
        _context.Usuarios.Add(usuario);
        _context.SaveChanges();
        return usuario;
    }
    catch (DbUpdateException ex)
    {
        throw new InvalidOperationException("Erro ao cadastrar usuário. Verifique os dados.", ex);
    }
}
// ATUALIZAR (com atualização parcial)
public UsuarioModel AtualizarUsuario(int id, UsuarioModel usuario)
{
    var existente = _context.Usuarios.Find(id);
    if (existente == null)
        throw new KeyNotFoundException("Usuário para atualização não encontrado.");

    // Atualiza apenas os campos não nulos/vazios
    if (!string.IsNullOrWhiteSpace(usuario.Nome))
        existente.Nome = usuario.Nome;

    if (!string.IsNullOrWhiteSpace(usuario.Sobrenome))
        existente.Sobrenome = usuario.Sobrenome;

    if (!string.IsNullOrWhiteSpace(usuario.Cpf))
        existente.Cpf = usuario.Cpf;

    if (!string.IsNullOrWhiteSpace(usuario.Cargo))
        existente.Cargo = usuario.Cargo;

    if (!string.IsNullOrWhiteSpace(usuario.Funcao))
        existente.Funcao = usuario.Funcao;

    if (!string.IsNullOrWhiteSpace(usuario.Email))
        existente.Email = usuario.Email;

    if (!string.IsNullOrWhiteSpace(usuario.Senha))
        existente.Senha = usuario.Senha;

    try
    {
        _context.Update(existente);
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
