using System.Collections.Generic;
using EValueApi.Business;

namespace EValueApi.Communication
{
    public class TimeFramesResponse
    {
        public List<TimeFrame> TimeFrames { get; set; }
        public bool Status { get; set; }
    }
}
