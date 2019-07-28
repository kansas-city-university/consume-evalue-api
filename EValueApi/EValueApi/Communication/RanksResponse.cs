using System.Collections.Generic;
using EValueApi.Business;

namespace EValueApi.Communication
{
    public class RanksResponse
    {
        public List<Rank> Ranks { get; set; }
        public bool Status { get; set; }
    }
}
