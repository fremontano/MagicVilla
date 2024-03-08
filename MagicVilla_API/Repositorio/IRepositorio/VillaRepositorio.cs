using MagicVilla_API.Datos;
using MagicVilla_API.Modelos;

namespace MagicVilla_API.Repositorio.IRepositorio
{
    public class VillaRepositorio : Repositorio<Villa>, IVillaRepositorio
    {


        //inyectar variablre al constructor
        private readonly ApplicationDbContext _dbContext;

        public VillaRepositorio(ApplicationDbContext dbContext) :base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Villa> Actualizar(Villa entity)
        {
            entity.FechaDeActualizacion = DateTime.Now;
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
    }
}
