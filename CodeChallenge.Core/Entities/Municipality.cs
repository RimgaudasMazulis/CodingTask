using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CodeChallenge.Core.Entities
{
    public class Municipality
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Tax> Taxes { get; set; }

    }
}
