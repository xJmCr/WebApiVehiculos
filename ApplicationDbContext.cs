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

        public DbSet<Tipo> Tipos { get; set; }

        //public DbSet<AlumnoClase> AlumnoClase { get; set; }
    }
}
