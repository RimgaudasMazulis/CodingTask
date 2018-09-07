using CodeChallenge.Core.Entities;
using CodeChallenge.Core.Interfaces.Repositories;
using CodeChallenge.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeChallenge.Data.Repositories
{
    public class MunicipalityTaxesRepository : IMunicipalityTaxesRepository
    {
        private CodeChallengeContext _context;

        public MunicipalityTaxesRepository(CodeChallengeContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Used to retrieve all the tax types
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<TaxType>> GetTaxTypesAsync()
        {
            return await _context.TaxTypes
                .ToListAsync();
        }

        /// <summary>
        /// Used to retrieve all the municipalities
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Municipality>> GetMunicipalitiesAsync()
        {
            return await _context.Municipalities
                .Include("Taxes")
                .ToListAsync();
        }

        /// <summary>
        /// Return specific municipality by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<Municipality> GetMunicipalityByIdAsync(int municipalityId)
        {
            return await _context.Municipalities
                .Include("Taxes")
                .FirstAsync(o => o.Id == municipalityId);
        }

        /// <summary>
        /// Used to retrieve particular municipality with taxes
        /// </summary>
        /// <param name="municipalityName"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public async Task<Municipality> GetMunicipalityTaxesAsync(string municipalityName, DateTime date)
        {
            return await _context.Municipalities
                .Where(o => o.Name == municipalityName)
                .Include("Taxes")
                .FirstAsync();
        }

        /// <summary>
        /// Used for inserting records one at the time
        /// </summary>
        /// <param name="municipality"></param>
        public async Task AddMunicipalityTaxAsync(Tax municipalityTax)
        {
            await _context.Taxes
                .AddAsync(municipalityTax);
        }

        /// <summary>
        /// Used for import when importing multiple records
        /// </summary>
        /// <param name="municiplities"></param>
        public async Task AddRangeOfMunicipalityTaxesAsync(List<Tax> municipalityTaxes)
        {
            await _context.Taxes
                .AddRangeAsync(municipalityTaxes);
        }

        /// <summary>
        /// Used for updating particular record
        /// </summary>
        /// <param name="tax"></param>
        public void UpdateMunicipalityTax(Tax tax)
        {
             _context.Entry(tax).State = EntityState.Modified;
        }

        /// <summary>
        /// Used for updating particular record
        /// </summary>
        /// <param name="municipality"></param>
        public void UpdateMunicipality(Municipality municipality)
        {
            _context.Entry(municipality).State = EntityState.Modified;
        }

        /// <summary>
        /// Used to save changes in the the database
        /// </summary>
        /// <returns></returns>
        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
