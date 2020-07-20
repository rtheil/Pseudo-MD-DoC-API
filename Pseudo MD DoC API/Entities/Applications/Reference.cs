using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pseudo_MD_DoC_API.Applications
{
    public class Reference
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Phone]
        [Required]
        public string PhoneNumber 
        {
            /*
            get 
            {
                string outPhone = String.Format("{0:###-###-####}", PhoneNumber);
                return outPhone; 
            }
            set { PhoneNumber = value; } 
            */
            get;
            set;
        }

        public string Relation { get; set; }
    }
}
