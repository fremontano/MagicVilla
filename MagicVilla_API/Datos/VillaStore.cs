using MagicVilla_API.Modelos.Dto;

namespace MagicVilla_API.Datos
{
    public static class VillaStore
    {
        public static List<VillaDto> villaList = new List<VillaDto>
        {

        new VillaDto{Id=1, Nombre="Vista a la Playa",Ocupantes=2,MetrosCuadrados=50},
        new VillaDto{Id=2, Nombre="Vista a la Piscina"},
        new VillaDto{Id=3, Nombre="Vista a la Moderna"},
        new VillaDto{Id=4, Nombre="Vista a la Ciudad "},
        };
    }
}
