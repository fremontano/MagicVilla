using System.ComponentModel.DataAnnotations;

namespace MagicVilla_API.Modelos.Dto
{
    public class VillaUpdateDto
    {


        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(40)]
        public string Nombre { get; set; }
        public string Detalle { get; set; }

        [Required]
        public double Tarifa { get; set; }
        [Required]
        public int Ocupantes { get; set; }

        public int MetrosCuadrados { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        public string Amenida { get; set; }
    }
}


//DTO(data transfer object)