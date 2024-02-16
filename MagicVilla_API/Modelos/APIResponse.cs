using System.Net;

namespace MagicVilla_API.Modelos


    //para qeu todos nuestros enpoint retornen una respuesta estandar, en representacion a lo que queremos retornar
{
    public class APIResponse
    {

        //propiedades
        public HttpStatusCode statusCode { get; set; } //codigo de estado que retorne mi enpoint
        public bool IsExistoso { get; set; } = true;


        //una lista string donde vamos a almacenar todos los errores que se nos presentan
        public List<string> ErrorMessages { get; set; }


        //Propieda de tipo objeto, para almacenar cualquier tipo de lista,objeto cualquier tipo 
        public object Resultado { get; set; }


    }
}
