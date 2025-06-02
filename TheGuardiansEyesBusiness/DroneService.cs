using TheGuardiansEyesModel;
using TheGuardiansEyesData;
using Microsoft.EntityFrameworkCore;

namespace TheGuardiansEyesBusiness
{
    public class DroneService
    {
        private readonly AppDbContext _context;

        public DroneService(AppDbContext context)
        {
            _context = context;
        }

        // LISTAR TODOS
        public List<DroneModel> ListarDrones()
        {
            return _context.Drones
                .Include(d => d.ImagensCapturadas)
                .ToList();
        }

        // OBTER POR ID
        public DroneModel ObterPorId(int id)
        {
            var drone = _context.Drones
                .Include(d => d.ImagensCapturadas)
                .FirstOrDefault(d => d.Id == id);

            if (drone == null)
                throw new KeyNotFoundException("Drone não encontrado.");

            return drone;
        }

        // CADASTRAR
        public DroneModel CadastrarDrone(DroneModel drone)
        {
            try
            {
                _context.Drones.Add(drone);
                _context.SaveChanges();
                return drone;
            }
            catch (DbUpdateException)
            {
                throw new InvalidOperationException("Erro ao cadastrar o drone. Verifique se os dados estão corretos.");
            }
        }

        // ATUALIZAR
        public bool AtualizarDrone(DroneModel drone)
        {
            var existente = _context.Drones
                .Include(d => d.ImagensCapturadas)
                .FirstOrDefault(d => d.Id == drone.Id);

            if (existente == null)
                throw new KeyNotFoundException("Drone não encontrado para atualização.");

            existente.Fabricante = drone.Fabricante;
            existente.Modelo = drone.Modelo;
            existente.TempoVoo = drone.TempoVoo;
            existente.DistanciaMaxima = drone.DistanciaMaxima;
            existente.VelocidadeMaxima = drone.VelocidadeMaxima;
            existente.Camera = drone.Camera;
            existente.Peso = drone.Peso;

            try
            {
                _context.Drones.Update(existente);
                _context.SaveChanges();
                return true;
            }
            catch (DbUpdateException)
            {
                throw new InvalidOperationException("Erro ao atualizar o drone.");
            }
        }

        // REMOVER
        public bool RemoverDrone(int id)
        {
            var drone = _context.Drones.Find(id);

            if (drone == null)
                throw new KeyNotFoundException("Drone não encontrado para remoção.");

            try
            {
                _context.Drones.Remove(drone);
                _context.SaveChanges();
                return true;
            }
            catch (DbUpdateException)
            {
                throw new InvalidOperationException("Erro ao remover o drone. Verifique se há dependências relacionadas.");
            }
        }
    }
}
