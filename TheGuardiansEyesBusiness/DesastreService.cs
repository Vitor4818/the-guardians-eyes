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

        //LISTAR TODOS
        public List<DesastreModel> ListarDesastres()
        {
            return _context.Desastres
                .Include(d => d.Local)
                .Include(d => d.ImpactoClassificacao)
                 .Include(d => d.ImpactoClassificacao) 
                .Include(d => d.GrupoDesastre)
                .Include(d => d.Usuario)
                .ToList();
        }

        //OBTER POR ID
        public DesastreModel ObterPorId(int id)
        {
            var desastre = _context.Desastres
                .Include(d => d.Local)
                .Include(d => d.ImpactoClassificacao)
                .Include(d => d.GrupoDesastre)
                .Include(d => d.Usuario)
                .FirstOrDefault(d => d.Id == id);

            if (desastre == null)
                throw new KeyNotFoundException("Desastre não encontrado.");

            return desastre!;
        }

        //CADASTRAR
        public DesastreModel CadastrarDesastre(DesastreModel desastre)
        {
            try
            {
                _context.Desastres.Add(desastre);
                _context.SaveChanges();
                return desastre;
            }
            catch (DbUpdateException)
            {
                throw new InvalidOperationException("Erro ao cadastrar o desastre. Verifique se os dados estão corretos.");
            }
        }

        //ATUALIZAR
        public bool AtualizarDesastre(DesastreModel desastre)
        {
            var existente = _context.Desastres.Find(desastre.Id);
            if (existente == null)
                throw new KeyNotFoundException("Desastre não encontrado para atualização.");

            existente.IdLocal = desastre.IdLocal;
            existente.IdImpactoClassificacao = desastre.IdImpactoClassificacao;
            existente.IdGrupoDesastre = desastre.IdGrupoDesastre;
            existente.IdUsuario = desastre.IdUsuario;
            existente.Cobrade = desastre.Cobrade;
            existente.DataOcorrencia = desastre.DataOcorrencia;

            try
            {
                _context.Desastres.Update(existente);
                _context.SaveChanges();
                return true;
            }
            catch (DbUpdateException)
            {
                throw new InvalidOperationException("Erro ao atualizar o desastre.");
            }
        }

        //REMOVER
        public bool RemoverDesastre(int id)
        {
            var desastre = _context.Desastres.Find(id);
            if (desastre == null)
                throw new KeyNotFoundException("Desastre não encontrado para remoção.");

            try
            {
                _context.Desastres.Remove(desastre);
                _context.SaveChanges();
                return true;
            }
            catch (DbUpdateException)
            {
                throw new InvalidOperationException("Erro ao remover o desastre. Verifique se há dependências relacionadas.");
            }
        }

        // OBTER MAIS PRÓXIMO
        public DesastreModel? ObterDesastreMaisProximo(double latitude, double longitude, double raioKm = 10.0)
        {
            var desastres = _context.Desastres
                .Include(d => d.Local)
                .ToList();

            foreach (var desastre in desastres)
            {
                if (desastre.Local != null)
                {
                    double distancia = CalcularDistanciaEmKm(
                        latitude, longitude,
                        desastre.Local.Latitude,
                        desastre.Local.Longitude
                    );

                    if (distancia <= raioKm)
                        return desastre;
                }
            }

            return null;
        }

        private double CalcularDistanciaEmKm(double lat1, double lon1, double lat2, double lon2)
        {
            const double raioTerraKm = 6371;

            double dLat = GrausParaRadianos(lat2 - lat1);
            double dLon = GrausParaRadianos(lon2 - lon1);

            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                       Math.Cos(GrausParaRadianos(lat1)) * Math.Cos(GrausParaRadianos(lat2)) *
                       Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return raioTerraKm * c;
        }

        private double GrausParaRadianos(double graus)
        {
            return graus * (Math.PI / 180);
        }
    }
}
