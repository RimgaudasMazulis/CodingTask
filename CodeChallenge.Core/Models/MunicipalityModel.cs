using System.Collections.Generic;

namespace CodeChallenge.Core.Models
{
    public class MunicipalityModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<TaxesModel> Taxes { get; set; }
    }
}
