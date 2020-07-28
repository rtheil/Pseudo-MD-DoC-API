using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Pseudo_MD_DoC_API.Models.Users
{
    public class ForgotModel
    {
        [Required]
        public string EmailAddress { get; set; }

        [Required]
        public string FromEmail { get; set; }

        [Required]
        public string FromName { get; set; }

        [Required]
        public string subject { get; set; }

        /// <summary>
        /// The suffix url to send to the user with the reset token appended
        /// </summary>
        [Required]
        public string ForgotUrl { get; set; }

        [Required]
        public string EmailContent { get; set; }
    }
}
