using System.Collections.Generic;
using EValueApi.Business;

namespace EValueApi.Communication
{
    public class PeopleGroupsResponse
    {
        public List<PeopleGroup> PeopleGroups { get; set; }
        public bool Status { get; set; }
    }
}
