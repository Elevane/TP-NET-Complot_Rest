using Microsoft.EntityFrameworkCore;
using TP_Complot_Rest.Persistence.Entities;

namespace TP_Complot_Rest.Persistence
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>(user =>
            {
                user.ToTable("users");
                user.HasIndex(u => u.Id);
            });

            base.OnModelCreating(builder);
        }
    }
}