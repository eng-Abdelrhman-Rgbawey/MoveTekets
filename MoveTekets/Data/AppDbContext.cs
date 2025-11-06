using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Actor> Actors { get; set; }
        public DbSet<Move> Movies { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Producer> Producers { get; set; }
        public DbSet<ActorMovie> ActorMovies { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<ActorMovie>().HasKey(am => new
            {
                am.ActorId,
                am.MovieId
            });

            modelBuilder.Entity<ActorMovie>()
                .HasOne(m => m.Movie)
                .WithMany(am => am.ActorMovies)
                .HasForeignKey(m => m.MovieId);

            modelBuilder.Entity<ActorMovie>()
                .HasOne(a => a.Actor)
                .WithMany(am => am.ActorMovies)
                .HasForeignKey(a => a.ActorId);

            base.OnModelCreating(modelBuilder);

        }
    }
}
