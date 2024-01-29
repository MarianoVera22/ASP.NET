using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Features.Videos.Queries.GetVideosList;
using CleanArchitecture.Domain;
using CleanArchitecture.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Repositories
{
    public class VideoRepository : RepositoryBase<Video>, IVideoRepository
    {
        public VideoRepository(StreamerDbContext context): base(context) { }
        public async Task<Video> GetVideoByNombre(string nombreVideo)
        {
            return await _context.Videos!.Where(o => o.Nombre == nombreVideo).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Video>> GetVideoByUsername(string username)
        {
            return await _context.Videos!.Where(v => v.CreateBy == username).ToListAsync();
        }
    }
}
