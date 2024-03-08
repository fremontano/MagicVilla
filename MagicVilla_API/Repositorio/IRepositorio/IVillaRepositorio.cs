using System.Threading.Tasks;
using MagicVilla_API.Modelos;
using MagicVilla_API.Repository.IRepositorio;

namespace MagicVilla_API.Repositorio.IRepositorio
{
    public interface IVillaRepositorio : IRepositorio<Villa>
    {
        Task<Villa> Actualizar(Villa entity);
    }
}
