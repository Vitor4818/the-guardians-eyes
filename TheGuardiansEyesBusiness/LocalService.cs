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
        public LocalModel? ObterPorId(int id)
        {
            return _context.Locais
                .Include(l => l.Desastres)
                .FirstOrDefault(l => l.Id == id);
        }

        // OBTER POR COORDENADAS (latitude e longitude exatas)
        public LocalModel? ObterPorCoordenadas(double latitude, double longitude)
        {
            return _context.Locais
                .FirstOrDefault(l => l.Latitude == latitude && l.Longitude == longitude);
        }

        // CADASTRAR UM NOVO LOCAL
        public LocalModel CadastrarLocal(LocalModel local)
        {
            _context.Locais.Add(local);
            _context.SaveChanges();
            return local;
        }

        // ATUALIZAR UM LOCAL EXISTENTE
        public bool AtualizarLocal(LocalModel local)
        {
            var existente = _context.Locais.Find(local.Id);
            if (existente == null) return false;

            existente.Latitude = local.Latitude;
            existente.Longitude = local.Longitude;
            existente.Cep = local.Cep;
            existente.Endereco = local.Endereco;
            existente.Municipio = local.Municipio;
            existente.Numero = local.Numero;

            _context.Locais.Update(existente);
            _context.SaveChanges();
            return true;
        }

        // REMOVER LOCAL
        public bool RemoverLocal(int id)
        {
            var local = _context.Locais.Find(id);
            if (local == null) return false;

            _context.Locais.Remove(local);
            _context.SaveChanges();
            return true;
        }
    }
}
