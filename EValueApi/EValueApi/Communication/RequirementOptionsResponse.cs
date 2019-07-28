using System.Collections.Generic;
using EValueApi.Business;

namespace EValueApi.Communication
{
    public class RequirementOptionsResponse
    {
        public List<RequirementOption> RequirementOptions { get; set; }
        public bool Status { get; set; }
    }
}
