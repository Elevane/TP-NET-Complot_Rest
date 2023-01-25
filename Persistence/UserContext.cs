using Microsoft.EntityFrameworkCore;
using TP_Complot_Rest.Persistence.Entities;
using TP_Complot_Rest.Persistence.Entitites;

namespace TP_Complot_Rest.Persistence
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Complot> Complots { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<ComplotGenre> ComplotGenres { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>(user =>
            {
                user.ToTable("users");
                user.HasIndex(u => u.Id);
            });

            builder.Entity<Complot>(complot =>
            {
                complot.ToTable("complot");
                complot.HasIndex(complot => complot.Id);
                complot.HasOne<User>(c => c.User).WithMany(u => u.Complots).HasForeignKey(c => c.UserId);
                complot.HasMany<Genre>(c => c.Genres).WithMany(g => g.Complots);
            });

            builder.Entity<Genre>(genre =>
            {
                genre.ToTable("genre");
                genre.HasIndex(g => g.Id);
                genre.HasMany<Complot>(g => g.Complots).WithMany(g => g.Genres);
            });

            base.OnModelCreating(builder);
        }
    }
}