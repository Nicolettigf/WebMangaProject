namespace Entities.MangaS
{
    public class Manga : KitsuMember
    {
        public int? Chapters { get; set; }
        public int? Volumes { get; set; }
        public bool? Publishing { get; set; }
        public DateTime? PublishedFrom { get; set; }
        public DateTime? PublishedTo { get; set; }
        public ICollection<MangaComentary>? Comentaries { get; set; }
        public List<Serialization>? Serializations { get; set; }
        public List<Author>? Authors { get; set; }

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
