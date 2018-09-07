using System;

namespace CodeChallenge.Core.Models
{
    public class CsvImportModel
    {
        public string MunicipalityName { get; set; }
        public int MunicipalityId { get; set; }
        public string Description { get; set; }
        public int TaxTypeId { get; set; }
        public double TaxAmount { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
