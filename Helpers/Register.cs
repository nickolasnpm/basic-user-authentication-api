namespace UserAuthentication.Helpers
{
    public class Register
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public List<RoleList> roles { get; set; }
        public string emailAddress { get; set; }
        public string password { get; set; }
    }
}
