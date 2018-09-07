using System.Collections.Generic;
using System.IO;
using CodeChallenge.Core.Models;
using CsvHelper;
using System.Linq;

namespace CodeChallenge.Services.Helpers
{
    public static class CSVHelper
    {
        public static IEnumerable<CsvImportModel> ReadCsvFile(string filePath)
        {
            List<CsvImportModel> result = new List<CsvImportModel>();
            using (StreamReader sr = File.OpenText(filePath))
            {
                CsvReader csv = new CsvReader(sr);
                result = csv.GetRecords<CsvImportModel>().ToList();
            }
            return result;         
        }

        public static void WriteCsvFile(IEnumerable<CsvImportModel> csvModels, string filePath)
        {
            using (StreamWriter sw = new StreamWriter(filePath))
            {
                var csv = new CsvWriter(sw);
                csv.WriteHeader(typeof(CsvImportModel));
                csv.WriteRecords(csvModels);
            }
        }        
    }
}
