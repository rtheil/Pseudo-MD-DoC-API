using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pseudo_MD_DoC_API.Applications
{
    public class ApplicationStatus
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Status { get; set; }

        /*public enum Type
        {
            New,
            BackgroundCheckPending,
            TestPending,
            FailedTest,
            Offer,
            Hired
        }*/
    }
}
