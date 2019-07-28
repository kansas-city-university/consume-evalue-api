using System.Collections.Generic;
using EValueApi.Business;

namespace EValueApi.Communication
{
    public class RequirementTypeOptionsResponse
    {
        public List<RequirementTypeOption> RequirementTypeOptions { get; set; }
        public bool Status { get; set; }
    }
}
