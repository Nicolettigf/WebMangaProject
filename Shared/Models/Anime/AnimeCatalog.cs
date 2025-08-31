using System.Linq.Expressions;

namespace Shared.Models.Anime
{
    //https://benjii.me/2018/01/expression-projection-magic-entity-framework-core/
    public class AnimeCatalog
    {
        public int Id { get; set; }
        public string CanonicalTitle { get; set; }
        public string? WebpLargeImageUrl { get; set; }
        
        public string? PosterImageLarge { get; set; }

        public string? CoverImageLarge { get; set; }

        public string ListType { get; set; }

        public static Expression<Func<Entities.AnimeS.Anime, AnimeCatalog>> Projection => x => new AnimeCatalog()
        {
            Id = x.Id,
            CanonicalTitle = x.Title,
            WebpLargeImageUrl = x.WebpLargeImageUrl,
            PosterImageLarge = x.PosterImageLarge,
            CoverImageLarge = x.CoverImageLarge
        };
    }

}
