using System;

namespace EValueApi.Business
{
    public class User
    {
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Ssn { get; set; }
        public int RankId { get; set; }
        public string Password { get; set; }
        public string Login { get; set; }
        public string LastName { get; set; }
        public string Initial { get; set; }
        public string Gender { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public DateTime? Birthdate { get; set; }

    }
}
