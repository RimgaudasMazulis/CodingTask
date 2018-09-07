using CodeChallenge.Core.Entities;
using CodeChallenge.Core.Models;
using AutoMapper;

namespace CodeChallenge.Core.Automapper
{
    public class TaxProfile : Profile
    {
        public TaxProfile()
        {
            CreateMap<Tax, CsvImportModel>();
            CreateMap<CsvImportModel, Tax>();

            CreateMap<Tax, TaxesModel>();
            CreateMap<TaxesModel, Tax>();

            CreateMap<CsvImportModel, TaxesModel>();
            CreateMap<TaxesModel, CsvImportModel>();
        }
    }
}
