using Microsoft.EntityFrameworkCore;
using UserAuthentication.Models;

namespace UserAuthentication.Data
{
    public class DBContext: DbContext
    {
        public DBContext(DbContextOptions<DBContext> options): base (options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRole>()
                .HasOne(x => x.User)
                .WithMany(y => y.UserRole)
                .HasForeignKey(z => z.UserID);

            modelBuilder.Entity<UserRole>()
                .HasOne(x => x.Role)
                .WithMany(y => y.UserRole)
                .HasForeignKey(z => z.RoleID);
        }

        public DbSet<User> UserTable { get; set; }
        public DbSet<Role> RoleTable { get; set; }
        public DbSet<UserRole> UserRoleTable { get; set; }

    }
}
