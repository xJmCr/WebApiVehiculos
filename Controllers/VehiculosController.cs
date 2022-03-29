using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiVehiculos.Entidades;
using WebApiVehiculos.Filtros;
using WebApiVehiculos.Services;

namespace WebApiVehiculos.Controllers
{
    [ApiController]
    [Route("api/vehiculos")]
    //[Authorize]
    public class VehiculosController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IService service;
        private readonly ServiceTransient serviceTransient;
        private readonly ServiceScoped serviceScoped;
        private readonly ServiceSingleton serviceSingleton;
        private readonly ILogger<VehiculosController> logger;

        public VehiculosController(ApplicationDbContext context, IService service,
            ServiceTransient serviceTransient, ServiceScoped serviceScoped,
            ServiceSingleton serviceSingleton, ILogger<VehiculosController> logger)
        {
            this.dbContext = context;
            this.service = service;
            this.serviceTransient = serviceTransient;
            this.serviceScoped = serviceScoped;
            this.serviceSingleton = serviceSingleton;
            this.logger = logger;
        }

        [HttpGet("GUID")]
        //[ResponseCache(Duration = 10)]
        [ServiceFilter(typeof(FiltroDeAccion))]
        public ActionResult ObtenerGuid()
        {
            return Ok(new
            {
                AlumnosControllerTransient = serviceTransient.guid,
                ServiceA_Transient = service.GetTransient(),
                AlumnosControllerScoped = serviceScoped.guid,
                ServiceA_Scoped = service.GetScoped(),
                AlumnosControllerSingleton = serviceSingleton.guid,
                ServiceA_Singleton = service.GetSingleton()
            });
        }

        [HttpGet]
        [HttpGet("listado")]
        [HttpGet("/listado")]
        //[ResponseCache(Duration = 10)]
        //[Authorize]
        [ServiceFilter(typeof(FiltroDeAccion))]
        public async Task<ActionResult<List<Vehiculo>>> Get()
        {
            //* Niveles de logs
            // Critical
            // Error
            // Warning
            // Information
            // Debug
            // Trace
            // *//
            throw new NotImplementedException();
            logger.LogInformation("Se obtiene el listado de alumnos");
            logger.LogWarning("Mensaje de prueba warning");
            service.EjecutarJob();
            return await dbContext.Vehiculos.Include(x => x.Tipos).ToListAsync();
        }

        [HttpGet("primero")]
        public async Task<ActionResult<Vehiculo>> PrimerVehiculo([FromHeader] int valor, [FromQuery] string vehiculo, [FromQuery] int vehiculoId)
        {
            return await dbContext.Vehiculos.FirstOrDefaultAsync();
        }

        [HttpGet("primero2")]
        public ActionResult<Vehiculo> PrimerVehiculo()
        {
            return new Vehiculo() { Nombre = "Ferrari" };
        }

        [HttpGet("{id:int}/{param=BMW}")]
        public async Task<ActionResult<Vehiculo>> Get(int id, string param)
        {
            var vehiculo = await dbContext.Vehiculos.FirstOrDefaultAsync(x => x.Id == id);

            if(vehiculo == null)
            {
                return NotFound();
            }

            return vehiculo;
        }

        [HttpGet("{nombre}")]
        public async Task<ActionResult<Vehiculo>> Get([FromRoute] string nombre)
        {
            var vehiculo = await dbContext.Vehiculos.FirstOrDefaultAsync(x => x.Nombre.Contains(nombre));

            if(vehiculo == null)
            {
                return NotFound();
            }

            return vehiculo;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Vehiculo vehiculo)
        {
            //Ejemplo para validar desde el controlador con la BD con ayuda del dbContext

            var existeVehiculoMismoNombre = await dbContext.Vehiculos.AnyAsync(x => x.Nombre == vehiculo.Nombre);

            if (existeVehiculoMismoNombre)
            {
                return BadRequest("Ya existe un autor con el nombre");
            }

            dbContext.Add(vehiculo);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(Vehiculo vehiculo, int id)
        {
            if(vehiculo.Id != id)
            {
                return BadRequest("El ID del vehiculo no coincide con el establecido en la URL.");
            }

            dbContext.Update(vehiculo);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await dbContext.Vehiculos.AnyAsync(x => x.Id == id);
            if(!exist)
            {
                return NotFound();
            }

            dbContext.Remove(new Vehiculo()
            {
                Id = id
            });
            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
