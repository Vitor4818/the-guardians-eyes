using TheGuardiansEyesModel;
using TheGuardiansEyesData;
using Microsoft.EntityFrameworkCore;

namespace TheGuardiansEyesBusiness
{
    public class TerrenoGeograficoService
    {
        private readonly AppDbContext _context;

        public TerrenoGeograficoService(AppDbContext context)
        {
            _context = context;
        }

        // LISTAR TODOS
        public List<TerrenoGeograficoModel> ListarTerrenos()
        {
            return _context.TerrenosGeograficos
                .Include(t => t.Desastres) // Inclui os desastres relacionados (se houver)
                .ToList();
        }

        // OBTER POR ID
        public TerrenoGeograficoModel? ObterPorId(int id)
        {
            return _context.TerrenosGeograficos
                .Include(t => t.Desastres)
                .FirstOrDefault(t => t.Id == id);
        }

    }
}
