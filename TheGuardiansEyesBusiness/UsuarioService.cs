using TheGuardiansEyesModel;
using TheGuardiansEyesData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;


namespace TheGuardiansEyesBusiness
{
    public class UsuarioService
    {
        private readonly AppDbContext _context;
        private readonly IMemoryCache _cache;
        private readonly TimeSpan _cacheDuration = TimeSpan.FromMinutes(5);

        public UsuarioService(AppDbContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }

        // LISTAR TODOS COM CACHE
        public List<UsuarioModel> ListarUsuarios()
        {
            const string cacheKey = "ListaUsuarios";

            if (!_cache.TryGetValue(cacheKey, out List<UsuarioModel> usuarios))
            {
                usuarios = _context.Usuarios
                    .Include(u => u.Desastres)
                    .ToList();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(_cacheDuration);

                _cache.Set(cacheKey, usuarios, cacheOptions);
            }

            return usuarios;
        }

        //Retorna Usuario por ID
        public UsuarioModel ObterPorId(int id)
        {
            string cacheKey = $"Usuario_Id_{id}";

            if (_cache.TryGetValue(cacheKey, out UsuarioModel usuario))
                return usuario;

            usuario = _context.Usuarios
                .Include(u => u.Desastres)
                .FirstOrDefault(u => u.Id == id);

            if (usuario == null)
                throw new KeyNotFoundException("Usuário não encontrado.");

            _cache.Set(cacheKey, usuario, TimeSpan.FromMinutes(5));
            return usuario;
        }

        // OBTER POR CPF
        public UsuarioModel ObterPorCpf(string cpf)
        {
            string cacheKey = $"Usuario_Cpf_{cpf}";

            if (_cache.TryGetValue(cacheKey, out UsuarioModel usuario))
                return usuario;

            usuario = _context.Usuarios
                .Include(u => u.Desastres)
                .FirstOrDefault(u => u.Cpf == cpf);

            if (usuario == null)
                throw new KeyNotFoundException("Usuário com CPF não encontrado.");

            _cache.Set(cacheKey, usuario, TimeSpan.FromMinutes(5));
            return usuario;
        }

        //Cadastrara Usuario
        public UsuarioModel CadastrarUsuario(UsuarioModel usuario)
        {
            var usuarioExistente = _context.Usuarios
                .FirstOrDefault(u => u.Cpf == usuario.Cpf || u.Email == usuario.Email);

            if (usuarioExistente != null)
                throw new InvalidOperationException("Já existe um usuário com o mesmo CPF ou e-mail.");

            try
            {
                _context.Usuarios.Add(usuario);
                _context.SaveChanges();

                // Invalidar lista de usuários
                _cache.Remove("ListaUsuarios");

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
            if (!string.IsNullOrWhiteSpace(usuario.Nome)) existente.Nome = usuario.Nome;
            if (!string.IsNullOrWhiteSpace(usuario.Sobrenome)) existente.Sobrenome = usuario.Sobrenome;
            if (!string.IsNullOrWhiteSpace(usuario.Cpf)) existente.Cpf = usuario.Cpf;
            if (!string.IsNullOrWhiteSpace(usuario.Cargo)) existente.Cargo = usuario.Cargo;
            if (!string.IsNullOrWhiteSpace(usuario.Funcao)) existente.Funcao = usuario.Funcao;
            if (!string.IsNullOrWhiteSpace(usuario.Email)) existente.Email = usuario.Email;
            if (!string.IsNullOrWhiteSpace(usuario.Senha)) existente.Senha = usuario.Senha;

            try
            {
                _context.Update(existente);
                _context.SaveChanges();

                // Invalida os caches relacionados
                _cache.Remove("ListaUsuarios");
                _cache.Remove($"Usuario_Id_{id}");
                _cache.Remove($"Usuario_Cpf_{existente.Cpf}");

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

                // Invalida os caches relacionados
                _cache.Remove("ListaUsuarios");
                _cache.Remove($"Usuario_Id_{id}");
                _cache.Remove($"Usuario_Cpf_{usuario.Cpf}");
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException("Erro ao excluir o usuário. Verifique se ele está vinculado a outros dados.", ex);
            }
        }
    }
}
