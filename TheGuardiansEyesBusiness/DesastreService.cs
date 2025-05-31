using TheGuardiansEyesModel;
using TheGuardiansEyesData;
using Microsoft.EntityFrameworkCore;

namespace TheGuardiansEyesBusiness
{
    public class DesastreService
    {
        private readonly AppDbContext _context;

        public DesastreService(AppDbContext context)
        {
            _context = context;
        }

        // LISTAR TODOS
        public List<DesastreModel> ListarDesastres()
        {
            return _context.Desastres
                .Include(d => d.Local)
                .Include(d => d.ImpactoClassificacao)
                        .ThenInclude(i => i.ImpactoClassificacao) // Agora sim pega o ImpactoClassificacaoModel
                .Include(d => d.GrupoDesastre)
                .Include(d => d.Usuario)
                .ToList();
        }

        // OBTER POR ID
        public DesastreModel? ObterPorId(int id)
        {
            return _context.Desastres
                .Include(d => d.Local)
                .Include(i => i.ImpactoClassificacao)
                .Include(d => d.GrupoDesastre)
                .Include(d => d.Usuario)
                .FirstOrDefault(d => d.Id == id);
        }

        // CADASTRAR
        public DesastreModel CadastrarDesastre(DesastreModel desastre)
        {
            _context.Desastres.Add(desastre);
            _context.SaveChanges();
            return desastre;
        }

        // ATUALIZAR
        public bool AtualizarDesastre(DesastreModel desastre)
        {
            var existente = _context.Desastres.Find(desastre.Id);
            if (existente == null) return false;

            existente.IdLocal = desastre.IdLocal;
            existente.Impacto = desastre.Impacto;
            existente.IdGrupoDesastre = desastre.IdGrupoDesastre;
            existente.IdUsuario = desastre.IdUsuario;
            existente.Cobrade = desastre.Cobrade;
            existente.DataOcorrencia = desastre.DataOcorrencia;

            _context.Desastres.Update(existente);
            _context.SaveChanges();
            return true;
        }

        // REMOVER
        public bool RemoverDesastre(int id)
        {
            var desastre = _context.Desastres.Find(id);
            if (desastre == null) return false;

            _context.Desastres.Remove(desastre);
            _context.SaveChanges();
            return true;
        }
    }
}
