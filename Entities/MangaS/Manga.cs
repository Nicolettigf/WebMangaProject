namespace Entities.MangaS
{
    public class Manga : MediaBase
    {
        public int? Chapters { get; set; }
        public int? Volumes { get; set; }
        public bool Publishing { get; set; }
        public DateTime PublishedFrom { get; set; }
        public DateTime? PublishedTo { get; set; }
        public ICollection<MangaComentary>? Comentaries { get; set; }

        #region deletar

        public string PosterImageLink { get; set; }
        public string? CoverImageLink { get; set; }
        public string? CanonicalTitle { get; set; }
        public int? RatingRank { get; set; }
        public int? PopularityRank { get; set; }
        public int? UserCount { get; set; }
        public int? FavoritesCount { get; set; }
      

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
