using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace mfiles.Entities
{
    public class core_bid
    {
        [Key]
        public int id { get; set; }
        [Required]
        public int applicant_id { get; set; }
        [ForeignKey("category_id")]
        public core_category core_Category { get; set; }
        public int category_id { get; set; }
    }
}
