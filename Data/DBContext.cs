using Microsoft.EntityFrameworkCore;
using UserAuthentication.Models;

namespace UserAuthentication.Data
{
    public class DBContext: DbContext
    {
        public DBContext(DbContextOptions<DBContext> options): base (options)
        {
            
        }

        public DbSet<User> userDB { get; set; }
    }
}
