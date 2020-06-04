using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pseudo_MD_DoC_API.Applications
{
    public class AppStatus
    {
        public enum AppStatusType
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
