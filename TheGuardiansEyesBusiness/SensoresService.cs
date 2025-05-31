using TheGuardiansEyesModel;
using TheGuardiansEyesData;
using Microsoft.EntityFrameworkCore;

namespace TheGuardiansEyesBusiness
{
    public class SensoresService
    {
        private readonly AppDbContext _context;

        public SensoresService(AppDbContext context)
        {
            _context = context;
        }

        // LISTAR TODOS
        public List<SensoresModel> ListarSensores()
        {
            return _context.Sensores.ToList();
        }

        // OBTER POR ID
        public SensoresModel? ObterPorId(int id)
        {
            return _context.Sensores.FirstOrDefault(s => s.Id == id);
        }

        // CADASTRAR
        public SensoresModel CadastrarSensor(SensoresModel sensor)
        {
            _context.Sensores.Add(sensor);
            _context.SaveChanges();
            return sensor;
        }

        // ATUALIZAR
        public bool AtualizarSensor(SensoresModel sensor)
        {
            var existente = _context.Sensores.Find(sensor.Id);
            if (existente == null) return false;

            existente.Chip = sensor.Chip;
            existente.Modelo = sensor.Modelo;
            existente.Interface = sensor.Interface;
            existente.Utilidade = sensor.Utilidade;
            existente.Fabricante = sensor.Fabricante;
            existente.Estado = sensor.Estado;
            existente.Saida = sensor.Saida;
            existente.TipoSaida = sensor.TipoSaida;
            existente.MediaTensaoRegistrada = sensor.MediaTensaoRegistrada;

            _context.Sensores.Update(existente);
            _context.SaveChanges();
            return true;
        }

        // REMOVER
        public bool RemoverSensor(int id)
        {
            var sensor = _context.Sensores.Find(id);
            if (sensor == null) return false;

            _context.Sensores.Remove(sensor);
            _context.SaveChanges();
            return true;
        }
    }
}
