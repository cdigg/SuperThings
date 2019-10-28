using AutoMapper;
using SuperThings.Data.Models;
using SuperThings.Logic.Models;
using System;
using System.Linq;

namespace SuperThings.Logic.Shared
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Registrant
            CreateMap<RegistrantDto, Registrant>()
                .ForMember(d => d.FavoriteIntegers, s => s.MapFrom(src => src.FavoriteIntegers.Select(x => new RegistrantInteger { IntegerValue = x })));
            CreateMap<Registrant, RegistrantDto>()
                .ForMember(d => d.FavoriteIntegers, s => s.MapFrom(src => src.FavoriteIntegers.Select(x => x.IntegerValue).ToArray()));

            //stats
            CreateMap<FavoriteInteger, FavoriteIntegerDto>().ReverseMap();
        }
    }
}
