using System;

namespace EValueApi.Business
{
    public class TimeFrame
    {

        public int TimeFrameId { get; set; }
        public string TimeFrameLabel { get; set; }
        public DateTime TimeFrameBegin { get; set; }
        public DateTime TimeFrameEnd { get; set; }

    }
}
