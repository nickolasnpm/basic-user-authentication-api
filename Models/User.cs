using System.ComponentModel.DataAnnotations.Schema;

namespace UserAuthentication.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public int age { get; set; }
        public string thisDay { get; set; }
        public string emailAddress { get; set; }
        public byte[] passwordHash { get; set; }
        public byte[] passwordSalt { get; set; }

        [NotMapped]
        public List<string> roles { get; set; }
        public List<UserRole> UserRole { get; set; }
    }
}
