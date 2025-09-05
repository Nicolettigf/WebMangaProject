using Entities.AnimeS;
using Entities.MangaS;

namespace Entities.Common
{
    public class MediaRatingFrequency
    {
        public int Id { get; set; }

        public int? _1 { get; set; }
        public int? _2 { get; set; }
        public int? _3 { get; set; }
        public int? _4 { get; set; }
        public int? _5 { get; set; }

        // FK opcionais para diferenciar se é Anime ou Manga
        public int? AnimeId { get; set; }
        public Anime? Anime { get; set; }

        public int? MangaId { get; set; }
        public Manga? Manga { get; set; }


    }
}
