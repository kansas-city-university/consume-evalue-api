using System;

namespace EValueApi.Business
{
    public class Schedule
    {
        public int ScheduleId { get; set; }
        public int UserId { get; set; }
        public int SiteId { get; set; }
        public int ActivityId { get; set; }
        public int? TeamId { get; set; }
        public int TimeFrameId { get; set; }
        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int EvaluationAction { get; set; }

    }
}
