using Microsoft.EntityFrameworkCore;
using TheGuardiansEyesModel; 

namespace TheGuardiansEyesData
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Adicione aqui seus DbSets
        public DbSet<UsuarioModel> Usuarios { get; set; }
    }
}
