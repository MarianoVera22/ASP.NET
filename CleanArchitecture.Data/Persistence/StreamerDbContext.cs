using CleanArchitecture.Domain;
using CleanArchitecture.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Persistence
{
    public class StreamerDbContext : DbContext
    {
        public StreamerDbContext(DbContextOptions<StreamerDbContext> options) : base(options) 
        {
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) {
            foreach (var entry in ChangeTracker.Entries<BaseDomainModel>()) {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.Now;
                        entry.Entity.CreateBy = "system";
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModifiedDate = DateTime.Now;
                        entry.Entity.LastModifiedBy = "system";
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-4U5NR5K\SQLEXPRESS; 
                                            Initial Catalog=Streamer; 
                                            Integrated Security=TRUE;
                                            Trust Server Certificate=True")
                                            .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, Microsoft.Extensions.Logging.LogLevel.Information)
                                            .EnableSensitiveDataLogging();
        }
*/
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Streamer>()
                        .HasMany(m => m.Videos)
                        .WithOne(m => m.Streamer)
                        .HasForeignKey(m => m.StreamerId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Video>()
                .HasMany(p => p.Actores)
                .WithMany(t => t.Videos)
                .UsingEntity<VideoActor>(
                    pt => pt.HasKey(e => new { e.ActorId, e.VideoId })
                );
        }

        public DbSet<Streamer>? Streamers { get; set; }
        public DbSet<Video>? Videos { get; set; }
        public DbSet<Actor>? Actores { get; set; }

    }
}
