using TheGuardiansEyesModel;
using TheGuardiansEyesData;
using Microsoft.EntityFrameworkCore;

namespace TheGuardiansEyesBusiness
{
    public class GrupoDesastreService
    {
        private readonly AppDbContext _context;

        public GrupoDesastreService(AppDbContext context)
        {
            _context = context;
        }

        // LISTAR TODOS
        public List<GrupoDesastreModel> ListarGrupos()
        {
            return _context.GruposDesastre
                .Include(g => g.Subgrupos)  // Inclui os subgrupos relacionados
                .Include(g => g.Desastres)  // Inclui os desastres relacionados
                .ToList();
        }

        // OBTER POR ID
        public GrupoDesastreModel? ObterPorId(int id)
        {
            return _context.GruposDesastre
                .Include(g => g.Subgrupos)
                .Include(g => g.Desastres)
                .FirstOrDefault(g => g.Id == id);
        }

        // CADASTRAR
        public GrupoDesastreModel CadastrarGrupo(GrupoDesastreModel grupo)
        {
            _context.GruposDesastre.Add(grupo);
            _context.SaveChanges();
            return grupo;
        }

        // ATUALIZAR
        public bool AtualizarGrupo(GrupoDesastreModel grupo)
        {
            var existente = _context.GruposDesastre.Find(grupo.Id);
            if (existente == null) return false;

            existente.NomeGrupo = grupo.NomeGrupo;

            _context.GruposDesastre.Update(existente);
            _context.SaveChanges();
            return true;
        }

        // REMOVER
        public bool RemoverGrupo(int id)
        {
            var grupo = _context.GruposDesastre.Find(id);
            if (grupo == null) return false;

            _context.GruposDesastre.Remove(grupo);
            _context.SaveChanges();
            return true;
        }
    }
}
