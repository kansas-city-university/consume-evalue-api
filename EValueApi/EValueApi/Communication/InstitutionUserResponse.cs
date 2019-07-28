using System.Collections.Generic;
using EValueApi.Business;

namespace EValueApi.Communication
{
    public class InstitutionUserResponse
    {
        public List<InstitutionUser> IntitutionUsers { get; set; }
        public bool Status { get; set; }
    }
}
