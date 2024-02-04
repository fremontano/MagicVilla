using MagicVilla_API.Modelos;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_API.Datos
{
    public class ApplicationDbContext :DbContext
    {


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options) 
        {
            
        }


        //este modelo se va a crear como una tabla en la base de datos
        public DbSet<Villa> Villas { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Villa>().HasData(
                new Villa()
                {
                    Id = 1,
                    Nombre = "Villa nancy",
                    Detalle = "Detalle de la villa..",
                    ImageUrl = "",
                    Ocupantes = 5,
                    MetrosCuadrados = 150,
                    Tarifa = 200,
                    Amenida = "",
                    FechaDeCreacion = DateTime.Now,
                    FechaDeActualizacion = DateTime.Now,

                
                },

                new Villa()
                {
                    Id = 2,
                    Nombre = "Premiun a la Piscina",
                    Detalle = "Detalle de la villa..",
                    ImageUrl = "",
                    Ocupantes = 2,
                    MetrosCuadrados = 40,
                    Tarifa = 270,
                    Amenida = "",
                    FechaDeCreacion = DateTime.Now,
                    FechaDeActualizacion = DateTime.Now,


                }

             );
        }
    }
}
