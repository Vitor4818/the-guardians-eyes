using System.Linq;
using TheGuardiansEyesModel;
using TheGuardiansEyesData;
namespace TheGuardiansEyesData.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public UsuarioModel? BuscarPorEmailESenha(string email, string senha)
        {
            return _context.Usuarios
                           .FirstOrDefault(u => u.Email == email && u.Senha == senha);
        }

        // outros métodos...
    }
}