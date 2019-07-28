using System.Collections.Generic;
using EValueApi.Business;

namespace EValueApi.Communication
{
    public class PersonalRecordsResponse
    {
        public List<PersonalRecord> PeronalRecords { get; set; }
        public bool Status { get; set; }
    }
}
