﻿using System.ComponentModel.DataAnnotations;

namespace MagicVilla_API.Modelos.Dto
{
    public class VillaCreateDto
    {



        [Required]
        [MaxLength(40)]
        public string Nombre { get; set; }
        public string Detalle { get; set; }
        public double Tarifa { get; set; }
        public int Ocupantes { get; set; }
        public int MetrosCuadrados { get; set; }
  
        public string ImageUrl { get; set; }
        public string Amenida { get; set; }
    }
}


//DTO(data transfer object)