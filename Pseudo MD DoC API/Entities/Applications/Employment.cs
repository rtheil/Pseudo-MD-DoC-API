using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pseudo_MD_DoC_API.Applications
{
    public class Employment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string EmployerName { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [Phone]
        [Required]
        public string Phone { get; set; }

        [Required]
        public string Position { get; set; }

        [Required]
        public bool CanContact { get; set; }
    }
}
