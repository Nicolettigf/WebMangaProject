using DataAccessLayer.Migrations;
using Entities.AnimeS;
using Entities.Enums;
using static Entities.Common.MediaBase;

namespace MvcPresentationLayer.Models.AnimeModel
{
    public class AnimeOnpageViewModel
    {
        public int Id { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public string? Title { get; set; }
        public string? Synopsis { get; set; }
        public int? Favorites { get; set; }
        public int? Popularity { get; set; }
        public string? Rating { get; set; }
        public string? Status { get; set; }
        public string? JpgLargeImageUrl { get; set; }
        public string? WebpLargeImageUrl { get; set; }
        public string? PosterImageLarge { get; set; }
        public string? CoverImageLarge { get; set; }
        public int? Episodes { get; set; }
        public string? Type { get; set; }
        public ICollection<GenreItem> GenreItems { get; set; }
        public ICollection<AnimeComentary> Comentaries { get; set; }
    }
}
