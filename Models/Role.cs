using Newtonsoft.Json;

namespace UserAuthentication.Models
{
    public class Role
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public List<UserRole> UserRole { get; set; }
    }
}
