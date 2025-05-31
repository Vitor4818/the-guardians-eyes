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
        public DbSet<ImpactoModel> Impactos { get; set; }
        public DbSet<SensoresModel> Sensores { get; set; }
        public DbSet<SubGrupoDesastreModel> SubGrupoDesastre { get; set; }
        public DbSet<TerrenoGeograficoModel> TerrenosGeograficos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ImpactoClassificacaoModel>().HasData(
                new ImpactoClassificacaoModel { Id = 1, Nivel = 1, DescNivel = "Leve" },
                new ImpactoClassificacaoModel { Id = 2, Nivel = 2, DescNivel = "Moderado" },
                new ImpactoClassificacaoModel { Id = 3, Nivel = 3, DescNivel = "Grave" }
            );

            modelBuilder.Entity<TerrenoGeograficoModel>().HasData(
               new TerrenoGeograficoModel { Id = 1, NomeTerreno = "Montanha" },
               new TerrenoGeograficoModel { Id = 2, NomeTerreno = "Planície" },
               new TerrenoGeograficoModel { Id = 3, NomeTerreno = "Floresta" },
               new TerrenoGeograficoModel { Id = 4, NomeTerreno = "Área Urbana" },
               new TerrenoGeograficoModel { Id = 5, NomeTerreno = "Deserto" },
               new TerrenoGeograficoModel { Id = 6, NomeTerreno = "Pantanal" },
               new TerrenoGeograficoModel { Id = 7, NomeTerreno = "Litoral" }
           );


            modelBuilder.Entity<ImpactoModel>()
        .HasOne(i => i.ImpactoClassificacao)
        .WithMany() // Sem coleção inversa por enquanto
        .HasForeignKey(i => i.ImpactoClassificacaoId);

            modelBuilder.Entity<ImagensCapturadasModel>()
                .HasOne(i => i.Local)
                .WithMany(l => l.ImagensCapturadas) // você pode criar essa propriedade no LocalModel
                .HasForeignKey(i => i.IdLocal);

            modelBuilder.Entity<ImagensCapturadasModel>()
                .HasOne(i => i.ImpactoClassificacao)
                .WithMany()
                .HasForeignKey(i => i.IdImpactoClassificacao);

            modelBuilder.Entity<ImagensCapturadasModel>()
                .HasOne(i => i.Drone)
                .WithMany()
                .HasForeignKey(i => i.IdDrone);

            modelBuilder.Entity<DesastreModel>()
    .HasOne(d => d.Local)
    .WithMany(l => l.Desastres)
    .HasForeignKey(d => d.IdLocal)
    .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DesastreModel>()
                .HasOne(d => d.ImpactoClassificacao)
                .WithMany(ic => ic.Desastres)
                .HasForeignKey(d => d.Impacto)
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

            modelBuilder.Entity<ImagensCapturadasModel>()
                .HasOne(imagem => imagem.   Desastre)
                .WithMany(desastre => desastre.ImagensCapturadas)
                .HasForeignKey(imagem => imagem.IdDesastre)
                .OnDelete(DeleteBehavior.Cascade);  // ou Restrict, conforme sua regra de negócio
    
        }

        
    
    
    }
    

    
}
