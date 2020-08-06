using Pseudo_MD_DoC_API.Applications;
using Pseudo_MD_DoC_API.Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pseudo_MD_DoC_API.Models.Applications
{
    public class ApplicationSaveModel
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string MiddleInitial { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string ZipCode { get; set; }
        [Required]
        public string HomePhone { get; set; }
        public string CellPhone { get; set; }
        [Required]
        public string SocialSecurityNumber { get; set; }
        [Required]
        public bool IsUsCitizen { get; set; }
        [Required]
        public bool HasFelony { get; set; }
        [Required]
        public bool WillDrugTest { get; set; }
        [Required]
        public ICollection<Education> Education { get; set; }
        [Required]
        public ICollection<Employment> Employment { get; set; }
        [Required]
        public ICollection<Reference> References { get; set; }
        [Required]
        public ApplicationStatus.Type ApplicationStatus { get; set; }
        public int? TestScore { get; set; }
        public DateTime? DateReceived { get; set; }
        public DateTime? DateProcessed { get; set; }
    }
}
