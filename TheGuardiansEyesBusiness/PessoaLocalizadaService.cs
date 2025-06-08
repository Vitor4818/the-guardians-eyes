using TheGuardiansEyesModel;
using TheGuardiansEyesData;
using Microsoft.EntityFrameworkCore;

namespace TheGuardiansEyesBusiness
{
    public class PessoaLocalizadaService
    {
        private readonly AppDbContext _context;

        public PessoaLocalizadaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<PessoaLocalizadaModel>> ListarPessoasLocalizadasAsync()
        {
            return await _context.PessoasLocalizadas
                .Include(p => p.Local)
                .Include(p => p.ImpactoClassificacao)
                .ToListAsync();
        }
        
        //Filtrar pelas pessoas identificadas nas ultimas 48h 
        public async Task<List<PessoaLocalizadaModel>> ListarPessoasUltimas48HorasAsync()
        {
            var dataLimite = DateTime.Now.AddHours(-48);

            return await _context.PessoasLocalizadas
                .Where(p => p.DataHora >= dataLimite)
                .Include(p => p.Local)
                .Include(p => p.ImpactoClassificacao)
                .ToListAsync();
        }
    }
}
