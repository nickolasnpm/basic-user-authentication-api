using System.ComponentModel.DataAnnotations.Schema;

namespace UserAuthentication.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string emailAddress { get; set; }
        public string password { get; set; }
        [NotMapped]
        public List<string> roles { get; set; }
        public List<UserRole> UserRole { get; set; }
    }
}
