namespace EValueApi.Business
{
    public class RequirementStatusOption
    {
        public static class Constants
        {
            public const int Met = 1;
            public const int Not_Met = 2;
            public const int Pending = 3;
            public const int NA = 4;
            public const int To_Be_Entered = 5;
            public const int Exempt = 6;
            public const int To_Be_Verified = 7;
        }

        public int StatusId { get; set; }
        public string StatusLabel { get; set; }
    }
}
