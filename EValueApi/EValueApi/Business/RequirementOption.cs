namespace EValueApi.Business
{
    public class RequirementOption
    {
        public static class Constants
        {
            public const int Ongoing = 1;
            public const int One_time = 2;
            public const int Not_required = 3;
        }
        public int RequirementId { get; set; }
        public string RequirementLabel { get; set; }
    }
}
