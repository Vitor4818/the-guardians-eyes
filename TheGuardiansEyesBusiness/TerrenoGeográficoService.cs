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
                .Include(t => t.Desastres) 
                .ToList();
        }

        // OBTER POR ID
        public TerrenoGeograficoModel ObterPorId(int id)
        {
            var terreno = _context.TerrenosGeograficos
                .Include(t => t.Desastres)
                .FirstOrDefault(t => t.Id == id);

            if (terreno == null)
                throw new KeyNotFoundException("Terreno geográfico não encontrado.");

            return terreno;
        }

    }
}
