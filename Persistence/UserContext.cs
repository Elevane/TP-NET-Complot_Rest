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
                complot.HasMany<ComplotGenre>(c => c.Genres).WithOne(g => g.Complot);
            });

            builder.Entity<Genre>(genre =>
            {
                genre.ToTable("genre");
                genre.HasIndex(g => g.Id);
                genre.HasMany<ComplotGenre>(g => g.Complots).WithOne(g => g.Genre);
            });

            builder.Entity<ComplotGenre>(cgenre =>
            {
                cgenre.ToTable("complotgenre");
                cgenre.HasIndex(g => g.Id);
                cgenre.HasOne<Genre>(g => g.Genre).WithMany(g => g.Complots);
                cgenre.HasOne<Complot>(g => g.Complot).WithMany(g => g.Genres);
            });
            base.OnModelCreating(builder);
        }
    }
}