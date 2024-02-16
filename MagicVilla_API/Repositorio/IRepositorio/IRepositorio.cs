using System.Linq.Expressions;

namespace MagicVilla_API.Repository.IRepositorio
{



    //Repositorio generico
    public interface IRepositorio<T> where T : class
    {

        //Metodos a inplementar en una clase
        Task Crear(T entity);

        Task<List<T>> ObtenerTodos(Expression<Func<T, bool>>? filter = null);

        Task<T> Obtener(Expression<Func<T, bool>>? filter = null,  bool tracked = true);


        Task Remover(T entity);

        Task Grabar();
        

    }
}
