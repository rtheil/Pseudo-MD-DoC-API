using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pseudo_MD_DoC_API.Models
{
    public class TokenModel
    {
        [Required]
        public string Token { get; set; }
    }
}
