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
        public SubGrupoDesastreModel ObterPorId(int id)
        {
            var subgrupo = _context.SubGrupoDesastre
                .Include(s => s.GrupoDesastre)
                .FirstOrDefault(s => s.Id == id);

            if (subgrupo == null)
                throw new KeyNotFoundException("Subgrupo de desastre não encontrado.");

            return subgrupo;
        }

        // CADASTRAR
        public SubGrupoDesastreModel CadastrarSubGrupo(SubGrupoDesastreModel subgrupo)
        {
            try
            {
                _context.SubGrupoDesastre.Add(subgrupo);
                _context.SaveChanges();
                return subgrupo;
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException("Erro ao cadastrar subgrupo. Verifique se há conflitos de integridade ou dados inválidos.", ex);
            }
        }

        // ATUALIZAR
        public SubGrupoDesastreModel AtualizarSubGrupo(SubGrupoDesastreModel subgrupo)
        {
            var existente = _context.SubGrupoDesastre.Find(subgrupo.Id);
            if (existente == null)
                throw new KeyNotFoundException("Subgrupo de desastre para atualização não encontrado.");

            existente.Subgrupo = subgrupo.Subgrupo;
            existente.Tipo = subgrupo.Tipo;
            existente.SubTipo = subgrupo.SubTipo;
            existente.GrupoDesastreId = subgrupo.GrupoDesastreId;

            try
            {
                _context.SubGrupoDesastre.Update(existente);
                _context.SaveChanges();
                return existente;
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException("Erro ao atualizar subgrupo. Verifique os dados fornecidos.", ex);
            }
        }

        // REMOVER
        public void RemoverSubGrupo(int id)
        {
            var subgrupo = _context.SubGrupoDesastre.Find(id);
            if (subgrupo == null)
                throw new KeyNotFoundException("Subgrupo de desastre para exclusão não encontrado.");

            try
            {
                _context.SubGrupoDesastre.Remove(subgrupo);
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException("Erro ao excluir subgrupo. Verifique se ele está vinculado a outros dados.", ex);
            }
        }
    }
}
