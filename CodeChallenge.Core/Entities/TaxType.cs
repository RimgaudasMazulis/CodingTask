using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CodeChallenge.Core.Entities
{
    public class TaxType
    {
        [Key]
        public int Id { get; set; }
        public string TaxName { get; set; }

        public virtual ICollection<Tax> Tax { get; set; }

    }
}
