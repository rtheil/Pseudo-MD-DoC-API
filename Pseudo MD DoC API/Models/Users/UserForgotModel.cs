using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Pseudo_MD_DoC_API.Models.Users
{
    public class UserForgotModel
    {
        [Required]
        public string EmailAddress { get; set; }
    }
}
