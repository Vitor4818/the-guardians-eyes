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
            try
            {
                _context.Impactos.Add(impacto);
                _context.SaveChanges();
                return impacto;
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException("Erro ao cadastrar impacto. Verifique se os dados estão corretos e se há restrições de integridade.", ex);
            }
        }

        // ATUALIZAR
        public ImpactoModel AtualizarImpacto(ImpactoModel impacto)
        {
            var existente = _context.Impactos.Find(impacto.Id);
            if (existente == null)
                throw new KeyNotFoundException("Impacto para atualização não encontrado.");

            existente.ImpactoClassificacaoId = impacto.ImpactoClassificacaoId;

            try
            {
                _context.Impactos.Update(existente);
                _context.SaveChanges();
                return existente;
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException("Erro ao atualizar o impacto. Verifique os dados fornecidos.", ex);
            }
        }

        // REMOVER
        public void RemoverImpacto(int id)
        {
            var impacto = _context.Impactos.Find(id);
            if (impacto == null)
                throw new KeyNotFoundException("Impacto para exclusão não encontrado.");

            try
            {
                _context.Impactos.Remove(impacto);
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException("Erro ao excluir o impacto. Verifique se ele está vinculado a outros dados.", ex);
            }
        }
    }
}
