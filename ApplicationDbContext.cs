using Microsoft.EntityFrameworkCore;
using WebApiVehiculos.Entidades;

namespace WebApiVehiculos
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Vehiculo> Vehiculos { get; set; }
    }
}
