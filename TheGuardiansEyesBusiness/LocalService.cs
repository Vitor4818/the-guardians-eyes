using TheGuardiansEyesModel;
using TheGuardiansEyesData;
using Microsoft.EntityFrameworkCore;

namespace TheGuardiansEyesBusiness
{
    public class LocalService
    {
        private readonly AppDbContext _context;

        public LocalService(AppDbContext context)
        {
            _context = context;
        }

        // LISTAR TODOS OS LOCAIS
        public List<LocalModel> ListarLocais()
        {
            return _context.Locais
                .Include(l => l.Desastres)
                .ToList();
        }

        // OBTER POR ID
        public LocalModel ObterPorId(int id)
        {
            var local = _context.Locais
                .Include(l => l.Desastres)
                .FirstOrDefault(l => l.Id == id);

            if (local == null)
                throw new KeyNotFoundException("Local não encontrado.");

            return local;
        }

        // OBTER POR COORDENADAS
        public LocalModel ObterPorCoordenadas(double latitude, double longitude)
        {
            var local = _context.Locais
                .FirstOrDefault(l => l.Latitude == latitude && l.Longitude == longitude);

            if (local == null)
                throw new KeyNotFoundException("Local com essas coordenadas não encontrado.");

            return local;
        }

        // CADASTRAR UM NOVO LOCAL
        public LocalModel CadastrarLocal(LocalModel local)
        {
            try
            {
                _context.Locais.Add(local);
                _context.SaveChanges();
                return local;
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException("Erro ao cadastrar local. Verifique os dados fornecidos.", ex);
            }
        }

        // ATUALIZAR UM LOCAL EXISTENTE
        public LocalModel AtualizarLocal(LocalModel local)
        {
            var existente = _context.Locais.Find(local.Id);
            if (existente == null)
                throw new KeyNotFoundException("Local para atualização não encontrado.");

            existente.Latitude = local.Latitude;
            existente.Longitude = local.Longitude;
            existente.Cep = local.Cep;
            existente.Endereco = local.Endereco;
            existente.Municipio = local.Municipio;
            existente.Numero = local.Numero;

            try
            {
                _context.Locais.Update(existente);
                _context.SaveChanges();
                return existente;
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException("Erro ao atualizar local. Verifique os dados fornecidos.", ex);
            }
        }

        // REMOVER LOCAL
        public void RemoverLocal(int id)
        {
            var local = _context.Locais.Find(id);
            if (local == null)
                throw new KeyNotFoundException("Local para exclusão não encontrado.");

            try
            {
                _context.Locais.Remove(local);
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException("Erro ao remover local. Verifique se há vínculos com outros dados.", ex);
            }
        }
    }
}
