using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace mfiles.Entities
{
    public class core_category
    {
        [Key]
        public int id { get; set; }
        [Required]
        [MaxLength(200)]
        public string name { get; set; }
        public ICollection<core_bid> core_Bids { get; set; } = new List<core_bid>();
    }
}
