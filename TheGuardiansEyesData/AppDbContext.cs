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
        public DbSet<DesastreModel> Desastres { get; set; }
        public DbSet<DroneModel> Drones { get; set; }
        public DbSet<GrupoDesastreModel> GruposDesastre { get; set; }
        public DbSet<ImagensCapturadasModel> ImagensCapturadas { get; set; }
        public DbSet<ImpactoClassificacaoModel> ImpactosClassificacao { get; set; }
        public DbSet<LocalModel> Locais { get; set; }
        public DbSet<SensoresModel> Sensores { get; set; }
        public DbSet<SubGrupoDesastreModel> SubGruposDesastre { get; set; }
        public DbSet<TerrenoGeograficoModel> TerrenosGeograficos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        modelBuilder.Entity<ImpactoClassificacaoModel>().HasData(
            new ImpactoClassificacaoModel { Id = 1, Nivel = 1, DescNivel = "Leve" },
            new ImpactoClassificacaoModel { Id = 2, Nivel = 2, DescNivel = "Moderado" },
            new ImpactoClassificacaoModel { Id = 3, Nivel = 3, DescNivel = "Grave" }
        );

            modelBuilder.Entity<ImagensCapturadasModel>()
            .HasOne(i => i.Drone)
            .WithMany(d => d.ImagensCapturadas)
            .HasForeignKey(i => i.IdDrone)
            .OnDelete(DeleteBehavior.Cascade);

                modelBuilder.Entity<DesastreModel>()
        .HasOne(d => d.Local)
        .WithMany(l => l.Desastres)
        .HasForeignKey(d => d.IdLocal)
        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DesastreModel>()
                .HasOne(d => d.ImpactoClassificacao)
                .WithMany(ic => ic.Desastres)
                .HasForeignKey(d => d.IdImpactoClassificacao)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DesastreModel>()
                .HasOne(d => d.GrupoDesastre)
                .WithMany(g => g.Desastres)
                .HasForeignKey(d => d.IdGrupoDesastre)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DesastreModel>()
                .HasOne(d => d.Usuario)
                .WithMany(u => u.Desastres)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.Restrict);
    

        }

        
    
    
    }
    

    
}
