using CodeChallenge.Core.Models;
using CodeChallenge.Core.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CodeChallenge.Core.Interfaces.Services;
using CodeChallenge.Services.Helpers;
using CodeChallenge.Core.Entities;
using System;
using System.IO;

namespace CodeChallenge.Services.Services
{
    public class MunicipalityTaxesService : IMunicipalityTaxesService
    {
        private readonly IMunicipalityTaxesRepository _repository;

        public MunicipalityTaxesService(IMunicipalityTaxesRepository repository)
        {
            _repository = repository;
        }

        public async Task ImportDataFromCsv()
        {
            List<Tax> municipalityTaxes = new List<Tax>();
            var filePath = Path.Combine(Environment.CurrentDirectory, "ImportData/test.csv");
            var records = CSVHelper.ReadCsvFile(filePath).ToList();
            
            foreach (var record in records)
            {
                if (!await IsMunicipalityTaxValid(Mapper.Map<TaxesModel>(record)))
                    continue;
                
                var municipalityTax = Mapper.Map<Tax>(record);
                municipalityTaxes.Add(municipalityTax);
            }

            await _repository.AddRangeOfMunicipalityTaxesAsync(municipalityTaxes);
            await _repository.Save();
        }

        public async Task AddMunicipalityTax(TaxesModel municipalityTaxModel)
        {
            if (!await IsMunicipalityTaxValid(Mapper.Map<TaxesModel>(municipalityTaxModel)))
                return;

            var tax = Mapper.Map<Tax>(municipalityTaxModel);

            await _repository.AddMunicipalityTaxAsync(tax);
            await _repository.Save();
        }

        public async Task<IEnumerable<MunicipalityModel>> GetMunicipalitiesWithTaxesAsync()
        {
            var municipalitiesWithTaxes = await _repository.GetMunicipalitiesAsync();
            return Mapper.Map<IEnumerable<MunicipalityModel>>(municipalitiesWithTaxes);
        }

        public async Task<TaxesModel> GetMunicipalityTaxAsync(string municipalityName, DateTime date)
        {
            var municipality = await _repository.GetMunicipalityTaxesAsync(municipalityName, date);
            if(municipality == null)
            {
                // Logs that municipality is missing in DB, can't insert this value
                return null;
            }
            if(municipality.Taxes == null)
            {
                // Logs that municipality is missing its Taxes
                return null;
            }

            foreach (var tax in municipality.Taxes.OrderByDescending(o => o.TaxTypeId))
            {
                if(tax.BeginDate <= date && date <= tax.EndDate)
                {
                    TaxesModel taxesModel = new TaxesModel()
                    {
                        MunicipalityName = municipality.Name,
                        Description = tax.Description,
                        TaxAmount = tax.TaxAmount,
                        TaxTypeId = tax.TaxTypeId,
                        BeginDate = tax.BeginDate,
                        EndDate = tax.EndDate
                    };
                    return taxesModel;
                }
            }
            // Logs that no period was found that mathes the date
            return null;
        }

        public async Task<IEnumerable<TaxTypesModel>> GetTaxTypesAsync()
        {
            var taxTypes = await _repository.GetTaxTypesAsync();
            return Mapper.Map<IEnumerable<TaxTypesModel>>(taxTypes);
        }

        public async Task UpdateMunicipalityTax(TaxesModel taxesModel)
        {
            if (!await IsMunicipalityTaxValid(Mapper.Map<TaxesModel>(taxesModel)))
                return;

            var taxEntity = Mapper.Map<Tax>(taxesModel);
            _repository.UpdateMunicipalityTax(taxEntity);
            await _repository.Save();
        }

        private async Task<bool> IsMunicipalityTaxValid(TaxesModel record)
        {
            var taxTypes = await _repository.GetTaxTypesAsync();
            var municipality = _repository.GetMunicipalityByIdAsync(record.MunicipalityId);
            if (municipality == null || municipality.GetType() != typeof(Municipality))
            {
                // Logs that municipality is missing in DB, can't insert this value
                return false;
            }

            if (taxTypes.First(o => o.Id == record.TaxTypeId) == null)
            {
                // Logs that taxTypeId is wrong
                return false;
            }

            return true;
        }

        //private string GetTaxTypeEnumName(int value)
        //{
        //    TaxTypesEnum taxType = (TaxTypesEnum)value;
        //    return taxType.ToString();
        //}  
    }
}
