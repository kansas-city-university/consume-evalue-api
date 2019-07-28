using System.Collections.Generic;
using EValueApi.Business;

namespace EValueApi.Communication
{
    public class GroupResponse
    {
        public List<Group> Groups { get; set; }
        public bool Status { get; set; }
    }
}
