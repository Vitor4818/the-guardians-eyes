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
                .Include(g => g.Subgrupos)
                .Include(g => g.Desastres)
                .ToList();
        }

        // OBTER POR ID
        public GrupoDesastreModel ObterPorId(int id)
        {
            var grupo = _context.GruposDesastre
                .Include(g => g.Subgrupos)
                .Include(g => g.Desastres)
                .FirstOrDefault(g => g.Id == id);

            if (grupo == null)
                throw new KeyNotFoundException("Grupo de desastre não encontrado.");

            return grupo;
        }

        // CADASTRAR
        public GrupoDesastreModel CadastrarGrupo(GrupoDesastreModel grupo)
        {
            try
            {
                _context.GruposDesastre.Add(grupo);
                _context.SaveChanges();
                return grupo;
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException("Erro ao cadastrar grupo de desastre. Verifique os dados informados.", ex);
            }
        }

        // ATUALIZAR
        public bool AtualizarGrupo(GrupoDesastreModel grupo)
        {
            var existente = _context.GruposDesastre.Find(grupo.Id);
            if (existente == null)
                throw new KeyNotFoundException("Grupo de desastre não encontrado para atualização.");

            try
            {
                existente.NomeGrupo = grupo.NomeGrupo;
                _context.GruposDesastre.Update(existente);
                _context.SaveChanges();
                return true;
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException("Erro ao atualizar grupo de desastre. Verifique os dados informados.", ex);
            }
        }

        // REMOVER
        public bool RemoverGrupo(int id)
        {
            var grupo = _context.GruposDesastre.Find(id);
            if (grupo == null)
                throw new KeyNotFoundException("Grupo de desastre não encontrado para remoção.");

            try
            {
                _context.GruposDesastre.Remove(grupo);
                _context.SaveChanges();
                return true;
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException("Erro ao remover grupo de desastre. Pode haver entidades relacionadas.", ex);
            }
        }
    }
}
