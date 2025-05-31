using TheGuardiansEyesData;
using TheGuardiansEyesModel;
using Microsoft.EntityFrameworkCore;

namespace TheGuardiansEyesBusiness
{
    public class ImpactoClassificacaoService
    {
        private readonly AppDbContext _context;

        public ImpactoClassificacaoService(AppDbContext context)
        {
            _context = context;
        }

        // LISTAR TODOS
        public List<ImpactoClassificacaoModel> ListarClassificacoes()
        {
            return _context.ImpactosClassificacao
                .ToList();
        }

        // OBTER POR ID
        public ImpactoClassificacaoModel? ObterPorId(int id)
        {
            return _context.ImpactosClassificacao
                .FirstOrDefault(i => i.Id == id);
        }

    }
}
