using TheGuardiansEyesModel;

namespace TheGuardiansEyesData.Repositories
{
    public interface IUserRepository
    {
            UsuarioModel? BuscarPorEmailESenha(string email, string senha);

    }
}