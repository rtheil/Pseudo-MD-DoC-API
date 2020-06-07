using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pseudo_MD_DoC_API.Applications
{
    public class ApplicationStatus
    {
        public enum Type
        {
            New,
            BackgroundCheckPending,
            TestPending,
            FailedTest,
            Offer,
            Hired
        }
    }
}
