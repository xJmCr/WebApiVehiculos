using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiVehiculos.Entidades
{
    public class Vehiculo
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [StringLength(maximumLength:5, ErrorMessage = "El campo {0} solo puede tener hasta 5 caracteres.")]
        public string Nombre { get; set; }

        [Range(2000, 2022, ErrorMessage = "El campo Año no se encuentra dentro del rango.")]
        [NotMapped]
        public int Año { get; set; }

        [CreditCard]
        [NotMapped]
        public string Tarjeta { get; set; }

        [Url]
        [NotMapped]
        public string Url { get; set; }
        public List<Tipo> Tipos { get; set; }
    }
}
