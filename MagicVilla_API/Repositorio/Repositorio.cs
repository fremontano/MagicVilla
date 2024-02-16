using MagicVilla_API.Datos;
using MagicVilla_API.Repository.IRepositorio;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MagicVilla_API.Repositorio
{
    public class Repositorio<T> : IRepositorio<T> where T : class
    {


        //inyectar el DBContext 
        private readonly ApplicationDbContext _dbContext;
        //Conversion para la entidad
        internal DbSet<T> dbSet;



        //CONSTRUCTOR PARA INICIALIZAR 
        public Repositorio(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            this.dbSet = _dbContext.Set<T>();
        }





        public async Task Crear(T entity)
        {
            await  dbSet.AddAsync(entity);
            await Grabar();
        }




        public async Task Grabar()
        {
            await _dbContext.SaveChangesAsync();    
        }




        public async Task<T> Obtener(Expression<Func<T, bool>>? filter = null, bool tracked = true)
        {
           IQueryable<T> query = dbSet; //conversion a la entidad
            if(!tracked)
            {
                query = query.AsNoTracking();    
            }

            //trabajar con el filtro
            if(filter != null)
            {
                query = query.Where(filter);
            }
            //retornar el valor
            return await query.FirstOrDefaultAsync();
        }




        public async Task<List<T>> ObtenerTodos(Expression<Func<T, bool>>? filter = null)
        {
            IQueryable<T> query = dbSet; //conversion a la entidad
            //trabajar con el filtro
            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.ToListAsync();
        }



        public async Task Remover(T entity)
        {
            dbSet.Remove(entity);
           await Grabar();
        }






    }
}
