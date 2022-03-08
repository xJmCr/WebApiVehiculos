using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiVehiculos.Entidades;

namespace WebApiVehiculos.Controllers
{
    [ApiController]
    [Route("api/tipos")]
    public class TiposController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public TiposController (ApplicationDbContext context)
        {
            this.dbContext = context;
        }

        [HttpGet]
        [HttpGet("/listadoTipos")]
        public async Task<ActionResult<List<Tipo>>> GetAll()
        {
            return await dbContext.Tipos.ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Tipo>> GetById(int id)
        {
            return await dbContext.Tipos.FirstOrDefaultAsync(x => x.Id == id);
        }

        [HttpPost]
        public async Task<ActionResult> Post(Tipo tipo)
        {
            var existeVehiculo = await dbContext.Vehiculos.AnyAsync(x => x.Id == tipo.VehiculoId);

            if (!existeVehiculo)
            {
                return BadRequest($"No existe el vehiculo con el ID: {tipo.VehiculoId}");
            }

            dbContext.Add(tipo);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(Tipo tipo, int id)
        {
            var exist = await dbContext.Tipos.AnyAsync(x => x.Id == id);

            if (!exist)
            {
                return NotFound("El tipo especificado no existe.");
            }

            if(tipo.Id != id)
            {
                return BadRequest("El ID del tipo no coincide con el establecido en la URL.");
            }

            dbContext.Update(tipo);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await dbContext.Tipos.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                return NotFound("El recurso no fue encontrado.");
            }

            dbContext.Remove(new Tipo { Id = id });
            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
