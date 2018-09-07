using CodeChallenge.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeChallenge.Core.Interfaces.Repositories
{
    public interface IMunicipalityTaxesRepository
    {
        Task<IEnumerable<Municipality>> GetMunicipalitiesAsync();
        Task<Municipality> GetMunicipalityTaxesAsync(string municipalityName, DateTime date);
        Task<IEnumerable<TaxType>> GetTaxTypesAsync();
        Task<Municipality> GetMunicipalityByIdAsync(int municipalityId);

        Task AddMunicipalityTaxAsync(Tax municipalityTax);
        Task AddRangeOfMunicipalityTaxesAsync(List<Tax> municipalityTaxes);

        void UpdateMunicipalityTax(Tax municipality);
        void UpdateMunicipality(Municipality municipality);

        Task Save();
    }
}
