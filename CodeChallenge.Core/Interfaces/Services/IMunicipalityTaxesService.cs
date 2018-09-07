using CodeChallenge.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeChallenge.Core.Interfaces.Services
{
    public interface IMunicipalityTaxesService
    {
        Task ImportDataFromCsv();
        Task AddMunicipalityTax(TaxesModel municipalityTaxModel);
        Task<TaxesModel> GetMunicipalityTaxAsync(string municipalityName, DateTime date);
        Task<IEnumerable<MunicipalityModel>> GetMunicipalitiesWithTaxesAsync();
        Task<IEnumerable<TaxTypesModel>> GetTaxTypesAsync();
        Task UpdateMunicipalityTax(TaxesModel taxesModel);
    }
}
