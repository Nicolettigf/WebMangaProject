using Entities.Common;
using System.Linq.Expressions;

namespace Shared.Models
{
    public class MediaCatalog
    {
        public int Id { get; set; }
        public string CanonicalTitle { get; set; }
        public string? PosterImageLarge { get; set; }
        public string? WebpLargeImageUrl { get; set; }
        public string? JpgLargeImageUrl { get; set; }

        // Projeção genérica pra qualquer MediaBase
        public static Expression<Func<MediaBase, MediaCatalog>> Projection => x => new MediaCatalog()
        {
            Id = x.Id,
            CanonicalTitle = x.Title,
            WebpLargeImageUrl = x.WebpLargeImageUrl,
            PosterImageLarge = x.PosterImageLarge,
            JpgLargeImageUrl = x.JpgLargeImageUrl
        };
    }
}
