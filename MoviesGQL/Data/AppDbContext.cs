using Microsoft.EntityFrameworkCore;
using MoviesGQL.Models;

namespace MoviesGQL.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions opts) : base(opts)
        {

        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Actor> Actors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>(e =>
            {
                e.ToTable("Movies");
                e.HasKey(p => p.Id);
                e.Property(p => p.Id).UseIdentityColumn();
                e.HasMany(p => p.Actors).WithMany(p => p.Movies).UsingEntity<MovieActors>();
                e.HasMany(p => p.MovieActors).WithOne(p => p.Movies).HasForeignKey(p => p.MovieId);

            });

            modelBuilder.Entity<Actor>(e =>
            {
                e.ToTable("Actors");
                e.HasKey(p => p.Id);
                e.Property(p => p.Id).UseIdentityColumn();
                e.HasMany(p => p.Movies).WithMany(p => p.Actors).UsingEntity<MovieActors>();
                e.HasMany(p => p.MovieActors).WithOne(p => p.Actors).HasForeignKey(p => p.ActorId);
            });



        }
    }
}
