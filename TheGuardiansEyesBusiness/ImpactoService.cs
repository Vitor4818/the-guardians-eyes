using TheGuardiansEyesModel;
using TheGuardiansEyesData;
using Microsoft.EntityFrameworkCore;

namespace TheGuardiansEyesBusiness
{
    public class ImpactoService
    {
        private readonly AppDbContext _context;

        public ImpactoService(AppDbContext context)
        {
            _context = context;
        }

        // LISTAR TODOS
        public List<ImpactoModel> ListarImpactos()
        {
            return _context.Impactos
                .Include(i => i.ImpactoClassificacao)
                .ToList();
        }

        // OBTER POR ID
        public ImpactoModel? ObterPorId(int id)
        {
            return _context.Impactos
                .Include(i => i.ImpactoClassificacao)
                .FirstOrDefault(i => i.Id == id);
        }

        // CADASTRAR
        public ImpactoModel CadastrarImpacto(ImpactoModel impacto)
        {
            _context.Impactos.Add(impacto);
            _context.SaveChanges();
            return impacto;
        }

        // ATUALIZAR
        public bool AtualizarImpacto(ImpactoModel impacto)
        {
            var existente = _context.Impactos.Find(impacto.Id);
            if (existente == null) return false;

            existente.ImpactoClassificacaoId = impacto.ImpactoClassificacaoId;

            _context.Impactos.Update(existente);
            _context.SaveChanges();
            return true;
        }

        // REMOVER
        public bool RemoverImpacto(int id)
        {
            var impacto = _context.Impactos.Find(id);
            if (impacto == null) return false;

            _context.Impactos.Remove(impacto);
            _context.SaveChanges();
            return true;
        }
    }
}
