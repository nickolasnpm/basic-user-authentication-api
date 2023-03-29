namespace UserAuthentication.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string role { get; set; }
        public string email { get; set; }
        public string password { get; set; }
    }
}
