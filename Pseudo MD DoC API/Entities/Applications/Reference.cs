using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pseudo_MD_DoC_API.Applications
{
    public class Reference
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Phone]
        [Required]
        public string PhoneNumber 
        {
            get;
            set;
        }

        [Required]
        [StringLength(30)]
        public string Relation { get; set; }
    }
}
