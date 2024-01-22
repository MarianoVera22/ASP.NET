using CleanArchitecture.Domain;
using CleanArchitecture.Infrastructure.Persistence;
using Microsoft.Extensions.Logging;


namespace CleanArchitecture.Data.Persistence
{
    public class StreamerDbContextSeed
    {
        public static async Task SeedAsync(StreamerDbContext context, ILogger<StreamerDbContextSeed> logger) {
            
            if (!context.Streamers!.Any()) {
                context.Streamers!.AddRange(GetPreconfiguredStreamer());
                await context.SaveChangesAsync();
                logger.LogInformation("Estamos insertando nuevos records al db {context}", typeof(StreamerDbContext).Name);
            }
        }

        private static IEnumerable<Streamer> GetPreconfiguredStreamer() {
            return new List<Streamer>
            {
                new Streamer {CreateBy= "vaxidrez", Nombre = "Maxi HBP", Url = "http://www.hbp.com"},
                new Streamer {CreateBy= "vaxidrez", Nombre = "Amazon VIP", Url = "http://www.amazonvip.com"},
            };
        }
    }
}
