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
            try
            {
                return _context.ImpactosClassificacao.ToList();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Erro ao listar classificações de impacto.", ex);
            }
        }

        // OBTER POR ID
        public ImpactoClassificacaoModel ObterPorId(int id)
        {
            try
            {
                var classificacao = _context.ImpactosClassificacao
                    .FirstOrDefault(i => i.Id == id);

                if (classificacao == null)
                    throw new KeyNotFoundException("Classificação de impacto não encontrada.");

                return classificacao;
            }
            catch (Exception ex) when (!(ex is KeyNotFoundException))
            {
                throw new InvalidOperationException("Erro ao buscar a classificação de impacto.", ex);
            }
        }
    }
}
