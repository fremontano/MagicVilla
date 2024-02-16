using MagicVilla_API.Modelos;
using MagicVilla_API.Repository.IRepositorio;

namespace MagicVilla_API.Repositorio.IRepositorio
{
    public interface IVillaRepositorio : IRepositorio<Villa>
    {


        //entidad a trabajar la entidad villa
        Task<Villa> Actualizar(Villa entity);


    }
}
