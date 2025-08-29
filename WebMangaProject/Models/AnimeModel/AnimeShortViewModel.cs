using Entities.AnimeS;
using System.ComponentModel.DataAnnotations;

namespace MvcPresentationLayer.Models.AnimeModel
{
    public class AnimeShortViewModel
    {
        public int Id { get; set; }
        public string canonicalTitle { get; set; }
        public string? WebpLargeImageUrl { get; set; }

        public string? PosterImageLarge { get; set; }

        public string? CoverImageLarge { get; set; }
    }
}
