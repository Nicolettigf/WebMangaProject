using Entities.MangaS;
using System.ComponentModel.DataAnnotations;

namespace MvcPresentationLayer.Models.MangaModels
{
    public class MangaShortViewModel
    {
        public int Id { get; set; }
        public string CanonicalTitle { get; set; }
        public string WebpLargeImageUrl { get; set; }

        public string? PosterImageLarge { get; set; }

        public string? CoverImageLarge { get; set; }
    }
}
