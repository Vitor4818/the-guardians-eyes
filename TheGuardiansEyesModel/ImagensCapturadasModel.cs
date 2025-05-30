namespace TheGuardiansEyesModel;

public class ImagensCapturadasModel
{

    public required int Id { get; set; }
    public required string Hospedagem { get; set; }
    public required string Tamanho { get; set; }
    //FK PARA DRONE
    public required int IdDrone { get; set; }
    
    // Navegação para Drone
    public DroneModel? Drone { get; set; }
    
    
}
