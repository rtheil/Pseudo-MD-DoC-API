using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pseudo_MD_DoC_API.Users
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string EmailAddress { get; set; }

        //TODO: Store password somehow
        //protected string StoredPassword { get; set; }

        [Required]
        public string HashedPassword { get; set; }

        [Required]
        public string PasswordSalt { get; set; }

        [Required]
        public bool Administrator { get; set; }
    }
}
