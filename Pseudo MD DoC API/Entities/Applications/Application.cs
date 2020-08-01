using Pseudo_MD_DoC_API.Users;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Pseudo_MD_DoC_API.Applications
{
    //[Table("Applications")]
    public class Application
    {
        public Application()
        {
            Education = new Collection<Education>();
            Employment = new Collection<Employment>();
            References = new Collection<Reference>();
        }

        //APPLICANT INFORMATION
        [Key]
        public int Id { get; set; }

        [Required]
        public User User { get; set; }

        [Required]
        [StringLength(25)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(25)]
        public string LastName { get; set; }

        [Required]
        [StringLength(1)]
        public string MiddleInitial { get; set; }

        [Required]
        [StringLength(100)]
        public string Address { get; set; }

        [Required]
        [StringLength(30)]
        public string City { get; set; }

        [Required]
        [StringLength(2)]
        public string State { get; set; }

        [Required]
        [StringLength(5)]
        public string ZipCode { get; set; }

        [Required]
        [StringLength(10)]
        public string HomePhone { get; set; }

        [StringLength(10)]
        public string CellPhone { get; set; }

        [Required]
        [StringLength(255)]
        public string EMailAddress { get; set; } //TODO: REMOVE BECAUSE USER ACCOUNT HAS THIS

        [Required]
        [StringLength(9)]
        public string SocialSecurityNumber { get; set; }

        [Required]
        public bool IsUsCitizen { get; set; }

        [Required]
        public bool HasFelony { get; set; }

        [Required]
        public bool WillDrugTest { get; set; }

        //ONE-TO-MANY ITEMS
        public ICollection<Education> Education { get; set; }

        public ICollection<Employment> Employment { get; set; }

        public ICollection<Reference> References { get; set; }

        //POST-APPLICATION INTERNAL INFORMATION
        [Required]
        //public ICollection<ApplicationStatus> ApplicationStatus { get; set; }
        public ApplicationStatus.Type ApplicationStatus { get; set; }

        public int? TestScore { get; set; }

        //PROCESS DATES
        public DateTime? DateReceived { get; set; }
        public DateTime? DateProcessed { get; set; }

    }
}
