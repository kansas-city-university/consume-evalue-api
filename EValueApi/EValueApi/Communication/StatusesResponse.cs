using System.Collections.Generic;
using EValueApi.Business;

namespace EValueApi.Communication
{
    public class StatusesResponse
    {
        public List<Status> Statuses { get; set; }
        public bool Status { get; set; }
    }
}
