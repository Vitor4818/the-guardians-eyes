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
        public SensoresModel ObterPorId(int id)
        {
            var sensor = _context.Sensores.FirstOrDefault(s => s.Id == id);
            if (sensor == null)
                throw new KeyNotFoundException("Sensor não encontrado.");
            
            return sensor;
        }

        // CADASTRAR
        public SensoresModel CadastrarSensor(SensoresModel sensor)
        {
            try
            {
                _context.Sensores.Add(sensor);
                _context.SaveChanges();
                return sensor;
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException("Erro ao cadastrar sensor. Verifique os dados fornecidos ou conflitos de integridade.", ex);
            }
        }

        // ATUALIZAR
        public SensoresModel AtualizarSensor(SensoresModel sensor)
        {
            var existente = _context.Sensores.Find(sensor.Id);
            if (existente == null)
                throw new KeyNotFoundException("Sensor para atualização não encontrado.");

            existente.Chip = sensor.Chip;
            existente.Modelo = sensor.Modelo;
            existente.Interface = sensor.Interface;
            existente.Utilidade = sensor.Utilidade;
            existente.Fabricante = sensor.Fabricante;
            existente.Estado = sensor.Estado;
            existente.Saida = sensor.Saida;
            existente.TipoSaida = sensor.TipoSaida;
            existente.MediaTensaoRegistrada = sensor.MediaTensaoRegistrada;

            try
            {
                _context.Sensores.Update(existente);
                _context.SaveChanges();
                return existente;
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException("Erro ao atualizar sensor. Verifique os dados fornecidos.", ex);
            }
        }

        // REMOVER
        public void RemoverSensor(int id)
        {
            var sensor = _context.Sensores.Find(id);
            if (sensor == null)
                throw new KeyNotFoundException("Sensor para exclusão não encontrado.");

            try
            {
                _context.Sensores.Remove(sensor);
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException("Erro ao excluir sensor. Verifique se há vínculos com outros dados.", ex);
            }
        }
    }
}
