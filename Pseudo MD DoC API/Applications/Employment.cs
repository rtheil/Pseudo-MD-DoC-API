﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pseudo_MD_DoC_API.Applications
{
    public class Employment
    {
        public int Id { get; set; }

        [Required]
        public string EmployerName { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }
        
        [Phone]
        public PhoneAttribute phone { get; set; }

        [Required]
        public string Position { get; set; }

        [Required]
        public bool CanContact { get; set; }
    }
}