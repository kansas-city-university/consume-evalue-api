using System.Collections.Generic;
using EValueApi.Business;

namespace EValueApi.Communication
{
    public class SitesResponse
    {
        public List<Site> Sites { get; set; }
        public bool Status { get; set; }
    }
}
