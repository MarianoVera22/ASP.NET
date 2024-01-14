using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Domain
{
    public class Streamer : BaseDomainModel
    {
        public string? Nombre { get; set; } = string.Empty;
        public string? Url { get; set; }
        public List<Video>? Videos { get; set; }


    }
}
