using Pseudo_MD_DoC_API.Applications;
using Pseudo_MD_DoC_API.Models.Users;
using Pseudo_MD_DoC_API.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pseudo_MD_DoC_API.Models.Applications
{
    public class ApplicationOutputModel
    {
        public int Id { get; set; }
        public UserOutputModel User { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleInitial { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string HomePhone { get; set; }
        public string CellPhone { get; set; }
        public string EMailAddress { get; set; } //TODO: REMOVE BECAUSE USER ACCOUNT HAS THIS
        public string SocialSecurityNumber { get; set; }
        public bool IsUsCitizen { get; set; }
        public bool HasFelony { get; set; }
        public bool WillDrugTest { get; set; }
        public ICollection<Education> Education { get; set; }
        public ICollection<Employment> Employment { get; set; }
        public ICollection<Reference> References { get; set; }
        public ApplicationStatus.Type ApplicationStatus { get; set; }
        public int? TestScore { get; set; }
        public DateTime? DateReceived { get; set; }
        public DateTime? DateProcessed { get; set; }

    }
}
