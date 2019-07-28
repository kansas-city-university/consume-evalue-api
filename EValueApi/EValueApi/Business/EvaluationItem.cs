using System;

namespace EValueApi.Business
{
    public class EvaluationItem
    {

        public int SiteId { get; set; }
        public string SiteName { get; set; }
        public int ActivityId { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public int TimeFrameId { get; set; }
        public string TimeFrameLabel { get; set; }
        public string Choice { get; set; }
        public string EssayText { get; set; }
        public int EvaluatorUserId { get; set; }
        public string EvaluatorExternalId { get; set; }
        public string EvaluatorExternalIdLabel { get; set; }
        public string NumericAnswer { get; set; }
        public bool IsConfidential { get; set; }
        public bool IsMandatory { get; set; }
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public string QuestionTopic { get; set; }
        public int QuestionTypeId { get; set; }
        public string QuestionTypeDesc { get; set; }
        public string RowId { get; set; }
        public int SortOrder { get; set; }
        public int? SubjectUserId { get; set; }
        public string SubjectExternalId { get; set; }
        public string SubjectExternalIdLabel { get; set; }
        public string SubjectLegacyExternalId { get; set; }
        public string EvaluatorLegacyExternalId { get; set; }
        public int Eimnum { get; set; }
        public int EvaluationRequestId { get; set; }
        public int StatusId { get; set; }
        public int EvaluationFormTypeId { get; set;  }
        public DateTime? CompletedDate { get; set; }
        public DateTime? LastEvaluatorUpdateDate { get; set; }

    }
}
