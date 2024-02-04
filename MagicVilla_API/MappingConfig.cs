using AutoMapper;
using MagicVilla_API.Modelos;
using MagicVilla_API.Modelos.Dto;

namespace MagicVilla_API
{

    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            //de una manera
            CreateMap<Villa, VillaDto>();
            CreateMap<VillaDto, Villa>();

            //de otra manera
            CreateMap<Villa, VillaCreateDto>().ReverseMap();
            CreateMap<Villa, VillaUpdateDto>().ReverseMap();

        }
    }
}
