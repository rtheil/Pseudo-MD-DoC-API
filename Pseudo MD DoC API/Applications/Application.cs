using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pseudo_MD_DoC_API.Applications
{
    public class Application
    {
        //APPLICANT INFORMATION
        public int Id { get; set; }

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
        public PhoneAttribute HomePhone { get; set; }

        public PhoneAttribute CellPhone { get; set; }

        [Required]
        public string EMailAddress { get; set; }

        [Required]
        public string SocialSecurityNumber { get; set; }

        [Required]
        public bool IsUsCitizen { get; set; }

        [Required]
        public bool HasFelony { get; set; }

        [Required]
        public bool WillDrugTest { get; set; }

        //ONE-TO-MANY ITEMS
        [Required]
        public Education[] Education { get; set; }

        [Required]
        public Employment[] EmploymentHistory { get; set; }

        [Required]
        public Reference[] References { get; set; }

        //POST-APPLICATION INTERNAL INFORMATION
        [Required]
        public AppStatus.AppStatusType ApplicationStatus { get; set; }
        
        public int TestScore { get; set; }

    }
}
