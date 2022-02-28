using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiVehiculos.Entidades;

namespace WebApiVehiculos.Controllers
{
    [ApiController]
    [Route("api/vehiculos")]
    public class VehiculosController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public VehiculosController(ApplicationDbContext context)
        {
            this.dbContext = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Vehiculo>>> Get()
        {
            return await dbContext.Vehiculos.Include(x => x.Tipos).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Post(Vehiculo vehiculo)
        {
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
