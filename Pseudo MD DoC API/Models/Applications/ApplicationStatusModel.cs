using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pseudo_MD_DoC_API.Models.Applications
{
    public class ApplicationStatusModel
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string statusAction { get; set; }
        public int? TestScore { get; set; }

    }
}
