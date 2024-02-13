using System.Linq.Expressions;

namespace MagicVilla_API.Repositorio.IRepositorio
{


    //interfaz generico recibimos cualquier tipo de identidad
    //Todos los maodelos que vamos agregando pueden utilizar repositorio
    public interface IRepositorio<T> where T : class
    {

        //Todos los maodelos que vayamos  agregando pueden utilizar el repositorio


        //Metodos para nuestras clases
        Task Crear(T ebtidad);

        Task<List<T>> ObtenerTodos(Expression<Func<T, bool>> ?filtro = null);

        Task<List<T>> Obtener(Expression<Func<T, bool>> filtro = null, bool tracked = true);

        Task Remove(T entidades);


        Task  Grabar();


    }
}
