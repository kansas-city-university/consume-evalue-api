using System.Collections.Generic;
using EValueApi.Business;

namespace EValueApi.Communication
{
    public class ActivitiesResponse
    {
        public List<Activity> Activities { get; set; }
        public bool Status { get; set; }
    }
}
