using System.Collections.Generic;
using EValueApi.Business;

namespace EValueApi.Communication
{
    public class RolesResponse
    {
        public List<Role> Roles { get; set; }
        public bool Status { get; set; }
    }
}
