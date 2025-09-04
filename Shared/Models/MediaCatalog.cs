using Entities;
using System.Linq.Expressions;

namespace Shared.Models
{
    public class MediaCatalog
    {
        public int Id { get; set; }
        public string CanonicalTitle { get; set; }
        public string? WebpLargeImageUrl { get; set; }
        public string? PosterImageLarge { get; set; }
        public string? CoverImageLarge { get; set; }

        // Projeção genérica pra qualquer MediaBase
        public static Expression<Func<MediaBase, MediaCatalog>> Projection(string listType) =>
            x => new MediaCatalog()
            {
                Id = x.Id,
                CanonicalTitle = x.Title,
                WebpLargeImageUrl = x.WebpLargeImageUrl,
                PosterImageLarge = x.PosterImageLarge,
                CoverImageLarge = x.CoverImageLarge,
            };
    }
}
