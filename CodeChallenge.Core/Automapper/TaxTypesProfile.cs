using AutoMapper;
using CodeChallenge.Core.Entities;
using CodeChallenge.Core.Models;

namespace CodeChallenge.Core.Automapper
{
    public class TaxTypesProfile : Profile
    {
        public TaxTypesProfile()
        {
            CreateMap<TaxType, TaxTypesModel>();
            CreateMap<TaxTypesModel, TaxType>();
        }
    }
}
