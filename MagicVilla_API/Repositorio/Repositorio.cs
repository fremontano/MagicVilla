using MagicVilla_API.Datos;
using MagicVilla_API.Repositorio.IRepositorio;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MagicVilla_API.Repositorio
{
    public class Repositorio<T> : IRepositorio<T> where T : class
    {



        //inyectar nuestro dbContext y no lo hacemos desde el controlador
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet; //variable interna


        //Constructor
        public Repositorio(ApplicationDbContext db)
        {
            _db = db; //inicializamos
            this.dbSet = _db.Set<T>(); //combertimos en una entidad
        }

        //Agregar un nuevo registro cualcuier entidad u objecto(villa)
        public async Task Crear(T entidad)
        {
            await dbSet.AddAsync(entidad);
            await Grabar();
        }


        //metodo grabar
        public async Task Grabar()
        {
            await _db.SaveChangesAsync();
        }


        //recibe un filtro y retorna un solo registro
        public async Task<List<T>> Obtener(Expression<Func<T, bool>> filtro = null, bool tracked = true)
        {
            //variable query
            IQueryable<T> query = dbSet;
            if (!tracked)
            {
                query = query.AsNoTracking();//si el parametro es falso
            }
            if (filtro != null)
            {
                query = query.Where(filtro); //expresion linq con where
            }

            // Utiliza Take(1) para limitar a un solo resultado y ToListAsync para convertirlo en lista
            return await query.Take(1).ToListAsync();
        }



        //metodo para obtener toda mi lista
        public async Task<List<T>> ObtenerTodos(Expression<Func<T, bool>>? filtro = null)
        {
            IQueryable<T> query = dbSet; // Especificamos el tipo genérico T
            if (filtro != null)
            {
                query = query.Where(filtro);
            }

            //retorna una lista
            return await query.ToListAsync();
        }


        //metodo de eliminar
        public async Task Remover(T entidades)
        {
            dbSet.Remove(entidades);
            await Grabar();
        }
    }
}
