using AutoMapper;
using CodeChallenge.Core.Entities;
using CodeChallenge.Core.Models;

namespace CodeChallenge.Core.Automapper
{
    public class MunicipalityProfile : Profile
    {
        public MunicipalityProfile()
        {
            CreateMap<Municipality, MunicipalityModel>();
            CreateMap<MunicipalityModel, Municipality>();
        }
    }
}
