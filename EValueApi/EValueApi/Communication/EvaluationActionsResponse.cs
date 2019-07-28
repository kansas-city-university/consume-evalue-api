using System.Collections.Generic;
using EValueApi.Business;

namespace EValueApi.Communication
{
    public class EvaluationActionsResponse
    {

        public List<EvaluationAction> EvaluationActions { get; set; }
        public bool Status { get; set; }

    }
}
