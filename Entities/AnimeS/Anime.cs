using Entities.Enums;
using System.ComponentModel;
using System.Linq.Expressions;
using static System.Net.Mime.MediaTypeNames;

namespace Entities.AnimeS
{
    public class Anime : Entity
    {
        public string? name { get; set; }
        public string? synopsis { get; set; }
        public string? description { get; set; }
        public AnimeSTitles? AnimeTitles { get; set; }
        public string? canonicalTitle { get; set; }
        public string? averageRating { get; set; }
        public AnimeRatingFrequencies? AnimeRatingFrequencies { get; set; }
        public int? userCount { get; set; }
        public int? favoritesCount { get; set; }
        public int? popularityRank { get; set; }
        public string? startDate { get; set; }
        public string? endDate { get; set; }
        public int? ratingRank { get; set; }
        public string? ageRating { get; set; }
        public string? ageRatingGuide { get; set; }
        public string? subtype { get; set; }
        public string? status { get; set; }
        public string? AnimePosterImage { get; set; }
        public string? AnimeCoverImage { get; set; }
        public int? episodeCount { get; set; }
        public string? episodeLength { get; set; }
        // public int? totalLength { get; set; }
        public string? youtubeVideoId { get; set; }
        public string? showType { get; set; }
        public string? source { get; set; }
        public int? scored_by { get; set; }
        public string? season { get; set; }
        public bool? approved { get; set; }

        public Broadcast? broadcast { get; set; }
        public Images? images { get; set; }
        public List<Studio>? studios { get; set; }
        public List<Demographic>? demographics { get; set; }
        public List<Theme>? themes { get; set; }
        public List<Producer>? producers { get; set; }
        public List<Relation>? relations { get; set; }
        public List<Licensor>? licensors { get; set; }
        public List<Genre>? genres { get; set; }
        public List<External>? external { get; set; }
        public List<ExplicitGenre>? explicit_genres { get; set; }
        public ICollection<Category> Categories { get; set; }
        public ICollection<AnimeComentary> Comentaries { get; set; }
        public List<Streaming>? streaming { get; set; }

    }

    public class Demographic
    {
        public int Id { get; set; }               // PK
        public int AnimeId { get; set; }          // FK para Anime
        public Anime Anime { get; set; }
        public int? mal_id { get; set; }
        public string? type { get; set; }
        public string? name { get; set; }
        public string? url { get; set; }
    }
    public class ExplicitGenre
    {
        public int Id { get; set; }               // PK
        public int AnimeId { get; set; }          // FK para Anime
        public Anime Anime { get; set; }
        public int? mal_id { get; set; }
        public string? type { get; set; }
        public string? name { get; set; }
        public string? url { get; set; }
    }

    public class External
    {
        public int Id { get; set; }               // PK
        public int AnimeId { get; set; }          // FK para Anime
        public Anime Anime { get; set; }
        public string? name { get; set; }
        public string? url { get; set; }
    }
    public class Genre
    {
        // Navegação
        public int Id { get; set; }
        public int? mal_id { get; set; }
        public string? type { get; set; }
        public string? name { get; set; }
        public string? url { get; set; }
    }
    public class Images
    {
        public int Id { get; set; }               // PK
        public int AnimeId { get; set; }          // FK para Anime
        public Anime Anime { get; set; }          // Navegação

        public Jpg Jpg { get; set; }              // Componente próprio
        public Webp Webp { get; set; }            // Componente próprio
    }

    public class Jpg
    {
        public int Id { get; set; }               // PK
        public string? ImageUrl { get; set; }
        public string? SmallImageUrl { get; set; }
        public string? LargeImageUrl { get; set; }
    }

    public class Webp
    {
        public int Id { get; set; }               // PK
        public string? ImageUrl { get; set; }
        public string? SmallImageUrl { get; set; }
        public string? LargeImageUrl { get; set; }
    }


    public class Licensor
    {
        public int Id { get; set; }
        public int AnimeId { get; set; }        // FK para Anime
        public Anime Anime { get; set; }
        public int? mal_id { get; set; }
        public string? type { get; set; }
        public string? name { get; set; }
        public string? url { get; set; }
    }
    public class Producer
    {
        public int Id { get; set; }
        public int AnimeId { get; set; }        // FK para Anime
        public Anime Anime { get; set; }
        public int? mal_id { get; set; }
        public string? type { get; set; }
        public string? name { get; set; }
        public string? url { get; set; }
    }

    public class Relation
    {
        public int Id { get; set; }
        public int AnimeId { get; set; }        // FK para Anime
        public Anime Anime { get; set; }
        public string? relation { get; set; }
        public List<Entry>? entry { get; set; }
    }
    public class Entry
    {
        public int Id { get; set; }
        public int? mal_id { get; set; }
        public string? type { get; set; }
        public string? name { get; set; }
        public string? url { get; set; }
    }
    public class Streaming
    {
        public int Id { get; set; }
        public int AnimeId { get; set; }        // FK para Anime
        public Anime Anime { get; set; }
        public string? name { get; set; }
        public string? url { get; set; }
    }

    public class Broadcast
    {
        public int Id { get; set; }
        public int AnimeId { get; set; }        // FK para Anime
        public Anime Anime { get; set; }        // Navegação

        public string? Day { get; set; }
        public string? Time { get; set; }
        public string? Timezone { get; set; }
    }

    public class Studio
    {
        public int Id { get; set; }
        public int AnimeId { get; set; }        // FK para Anime
        public Anime Anime { get; set; }

        public int? mal_id { get; set; }
        public string? type { get; set; }
        public string? name { get; set; }
        public string? url { get; set; }
    }

    public class Theme
    {
        public int Id { get; set; }
        public int AnimeId { get; set; }        // FK para Anime
        public Anime Anime { get; set; }

        public int? mal_id { get; set; }
        public string? type { get; set; }
        public string? name { get; set; }
        public string? url { get; set; }
    }


}
