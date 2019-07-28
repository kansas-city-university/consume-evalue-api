using System.Collections.Generic;
using EValueApi.Business;

namespace EValueApi.Communication
{
    public class RequirementStatusOptionsResponse
    {
        public List<RequirementStatusOption> RequirementStatusOptions { get; set; }
        public bool Status { get; set; }
    }
}
