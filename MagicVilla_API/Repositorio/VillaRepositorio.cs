using MagicVilla_API.Datos;
using MagicVilla_API.Modelos;
using MagicVilla_API.Repositorio.IRepositorio;

namespace MagicVilla_API.Repositorio
{
    public class VillaRepositorio :Repositorio<Villa>, IVillaRepositorio
    {



        private readonly ApplicationDbContext _dbContext;


        public VillaRepositorio(ApplicationDbContext dbContext) :base(dbContext) 
        {
            _dbContext = dbContext;
            
        }


        //tiene su propio metodo a actualizar, apesar que hereda de los otros metodos en  REPOSITORIO
        //Agregar este servicio a programa.cs

        public async Task<Villa> Actualizar(Villa entity)
        {
            entity.FechaDeActualizacion = DateTime.Now;
            _dbContext.Villas.Update(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }
    }
}
