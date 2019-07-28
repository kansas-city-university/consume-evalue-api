using System.Collections.Generic;
using EValueApi.Business;

namespace EValueApi.Communication
{
    public class EvaluationItemsResponse
    {
        public List<EvaluationItem> EvaluationItems { get; set; }
        public bool Status { get; set; }
    }
}
