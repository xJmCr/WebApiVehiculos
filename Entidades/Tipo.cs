using WebApiVehiculos.Validaciones;

namespace WebApiVehiculos.Entidades
{
    public class Tipo
    {
        public int Id { get; set; }

        public string Nombre {  get; set; }

        public string Color { get; set; }

        public int VehiculoId { get; set; }

        public Vehiculo Vehiculo { get; set; }
    }
}
