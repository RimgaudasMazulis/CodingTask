using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeChallenge.Core.Entities
{
    public class Tax
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        public string MunicipalityName { get; set; }
        public double TaxAmount { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }

        [ForeignKey("TaxType")]
        public int TaxTypeId { get; set; }
        public virtual TaxType TaxType { get; set; }

        [ForeignKey("Municipality")]
        public int MunicipalityId { get; set; }
        public virtual Municipality Municipality { get; set; }
    }
}
