using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pseudo_MD_DoC_API.Entities
{
    public class Configuration
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string propertyName { get; set; }

        [Required]
        public string propertyValue { get; set; }

    }
}
