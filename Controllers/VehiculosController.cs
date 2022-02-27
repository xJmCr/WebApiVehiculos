using Microsoft.AspNetCore.Mvc;
using WebApiVehiculos.Entidades;

namespace WebApiVehiculos.Controllers
{
    [ApiController]
    [Route("api/vehiculos")]
    public class VehiculosController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<Vehiculo>> Get()
        {
            return new List<Vehiculo>()
            {
                new Vehiculo() { Id = 1, Nombre = "BMW"},
                new Vehiculo() { Id = 2, Nombre = "Mercedes-Benz"}
            };
        }
    }
}
