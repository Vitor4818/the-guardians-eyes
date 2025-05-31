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
        public DroneModel? ObterPorId(int id)
        {
            return _context.Drones
                .Include(d => d.ImagensCapturadas)
                .FirstOrDefault(d => d.Id == id);
        }

        // CADASTRAR
        public DroneModel CadastrarDrone(DroneModel drone)
        {
            _context.Drones.Add(drone);
            _context.SaveChanges();
            return drone;
        }

        // ATUALIZAR
        public bool AtualizarDrone(DroneModel drone)
        {
            var existente = _context.Drones
                .Include(d => d.ImagensCapturadas)
                .FirstOrDefault(d => d.Id == drone.Id);

            if (existente == null) return false;

            existente.Fabricante = drone.Fabricante;
            existente.Modelo = drone.Modelo;
            existente.TempoVoo = drone.TempoVoo;
            existente.DistanciaMaxima = drone.DistanciaMaxima;
            existente.VelocidadeMaxima = drone.VelocidadeMaxima;
            existente.Camera = drone.Camera;
            existente.Peso = drone.Peso;

            _context.Drones.Update(existente);
            _context.SaveChanges();
            return true;
        }

        // REMOVER
        public bool RemoverDrone(int id)
        {
            var drone = _context.Drones.Find(id);
            if (drone == null) return false;

            _context.Drones.Remove(drone);
            _context.SaveChanges();
            return true;
        }
    }
}
