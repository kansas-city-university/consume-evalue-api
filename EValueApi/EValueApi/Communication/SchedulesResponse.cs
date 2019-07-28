using System.Collections.Generic;
using EValueApi.Business;

namespace EValueApi.Communication
{
    public class SchedulesResponse
    {
        public List<Schedule> Schedules { get; set; }
        public bool Status { get; set; }
    }
}
