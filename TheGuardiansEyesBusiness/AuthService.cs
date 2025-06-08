using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TheGuardiansEyesModel;
using TheGuardiansEyesData;
using TheGuardiansEyesData.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace TheGuardiansEyesBusiness
{
    public class AuthService
{
    private readonly IUserRepository _userRepository;
    private readonly string _secret;

    public AuthService(IUserRepository userRepository, IConfiguration configuration)
    {
        _userRepository = userRepository;
            _secret = configuration["JwtSettings:SecretKey"]
            ?? throw new ArgumentException("JWT Secret Key não configurada.");
    }

public (UsuarioModel? usuario, string? token) Login(string email, string senha)
{
    var user = _userRepository.BuscarPorEmailESenha(email, senha);
    if (user == null) return (null, null);

    var token = GerarToken(user);
    return (user, token);
}

        public string GerarToken(UsuarioModel usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secret);
if (usuario.Nome == null || usuario.Email == null || usuario.Id == null)
    throw new ArgumentException("Nome, Email e Id do usuário não podem ser nulos.");

    var nome = usuario.Nome;
    var email = usuario.Email;
    var id = usuario.Id.Value;

            var tokenDescriptor = new SecurityTokenDescriptor
            
            {
                Subject = new ClaimsIdentity(new[]
                {
            new Claim(ClaimTypes.NameIdentifier, id.ToString()),
            new Claim(ClaimTypes.Name, nome),
            new Claim(ClaimTypes.Email, email),
            new Claim(ClaimTypes.Role, "Usuario")
            }),
                Expires = DateTime.UtcNow.AddHours(3),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}