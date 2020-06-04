using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pseudo_MD_DoC_API.Applications
{
    public class Reference
    {
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        [Phone]
        public PhoneAttribute PhoneNumber { get; set; }
        
        public string Relation { get; set; }
    }
}
