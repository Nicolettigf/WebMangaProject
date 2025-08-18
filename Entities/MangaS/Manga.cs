using Entities.AnimeS;
using Entities.Enums;

namespace Entities.MangaS
{
    public class Manga : MediaBase
    {
        public int? Chapters { get; set; }
        public int? Volumes { get; set; }
        public bool Publishing { get; set; }
        public DateTime PublishedFrom { get; set; }
        public DateTime? PublishedTo { get; set; }
        public RatingFrequencies? RatingFrequencies { get; set; }
        public ICollection<MangaComentary>? Comentaries { get; set; }

        #region deletar

        public string? CanonicalTitle { get; set; }
        public MangaTitles? Titles { get; set; }
        public string Synopsis { get; set; }
        public string? AverageRating { get; set; }
        public int? RatingRank { get; set; }
        public int? PopularityRank { get; set; }
        public int? UserCount { get; set; }
        public int? FavoritesCount { get; set; }
        public int? VolumeCount { get; set; }
        public string? Serialization { get; set; } 
        public string PosterImageLink { get; set; }
        public string? CoverImageLink { get; set; }
        public string? Subtype { get; set; }
        public int? ChapterCount { get; set; }
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
        public string? Status { get; set; } 
        //public ICollection<Category>? Genres { get; set; }

        #endregion

        public List<Serialization> Serializations { get; set; }
        public List<Author> Authors { get; set; }

    }
    public class Serialization : MangaBaseReference { }
    public class Author : MangaBaseReference { }
    public abstract class MangaBaseReference
    {
        public int Id { get; set; }               // PK
        public int MangaId { get; set; }          // FK para manga
        public Manga Manga { get; set; }
        public int? mal_id { get; set; }
        public string? type { get; set; }
        public string? name { get; set; }
        public string? url { get; set; }
    }

}
