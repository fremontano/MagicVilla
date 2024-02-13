using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MagicVilla_API.Modelos
{
    public class Villa
    {

        //la propiedad de id tiene la clve primaria key para indicar que es la primary key de la taba de datos
        //y el id se incrementara auotomaticamente
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Detalle { get; set; }
        public double Tarifa { get; set; }
        public int Ocupantes { get; set; }
        public int MetrosCuadrados  { get; set; }
        public string ImageUrl { get; set; }
        public string Amenida  { get; set; }

        // Propiedades de fecha
        public DateTime FechaDeCreacion { get; set; }
        public DateTime FechaDeActualizacion { get; set; }




    }
}


