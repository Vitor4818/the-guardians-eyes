using TheGuardiansEyesModel;
using TheGuardiansEyesData;
using Microsoft.EntityFrameworkCore;

namespace TheGuardiansEyesBusiness
{
    public class SubGrupoDesastreService
    {
        private readonly AppDbContext _context;

        public SubGrupoDesastreService(AppDbContext context)
        {
            _context = context;
        }

        // LISTAR TODOS
        public List<SubGrupoDesastreModel> ListarSubGrupos()
        {
            return _context.SubGrupoDesastre
                .Include(s => s.GrupoDesastre)
                .ToList();
        }

        // OBTER POR ID
        public SubGrupoDesastreModel? ObterPorId(int id)
        {
            return _context.SubGrupoDesastre
                .Include(s => s.GrupoDesastre)
                .FirstOrDefault(s => s.Id == id);
        }

        // CADASTRAR
        public SubGrupoDesastreModel CadastrarSubGrupo(SubGrupoDesastreModel subgrupo)
        {
            _context.SubGrupoDesastre.Add(subgrupo);
            _context.SaveChanges();
            return subgrupo;
        }

        // ATUALIZAR
        public bool AtualizarSubGrupo(SubGrupoDesastreModel subgrupo)
        {
            var existente = _context.SubGrupoDesastre.Find(subgrupo.Id);
            if (existente == null) return false;

            existente.Subgrupo = subgrupo.Subgrupo;
            existente.Tipo = subgrupo.Tipo;
            existente.SubTipo = subgrupo.SubTipo;
            existente.GrupoDesastreId = subgrupo.GrupoDesastreId;

            _context.SubGrupoDesastre.Update(existente);
            _context.SaveChanges();
            return true;
        }

        // REMOVER
        public bool RemoverSubGrupo(int id)
        {
            var subgrupo = _context.SubGrupoDesastre.Find(id);
            if (subgrupo == null) return false;

            _context.SubGrupoDesastre.Remove(subgrupo);
            _context.SaveChanges();
            return true;
        }
    }
}
