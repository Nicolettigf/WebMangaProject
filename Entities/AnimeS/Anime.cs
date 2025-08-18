using Entities.AnimeS;
using Entities.Enums;
using System.ComponentModel;
using System.Linq.Expressions;
using static System.Net.Mime.MediaTypeNames;

namespace Entities.AnimeS
{
    public class Anime : MediaBase
    {
        public string? Source { get; set; }
        public int? Episodes { get; set; }
        public bool? Airing { get; set; }
        public string? Duration { get; set; }
        public string? Rating { get; set; }
        public string? Season { get; set; }

        public AnimeRatingFrequencies? AnimeRatingFrequencies { get; set; }
        public AnimeSTitles? AnimeTitles { get; set; }
        public ICollection<AnimeComentary?> Comentaries { get; set; }


        #region futuraexclusao
        public string? name { get; set; }
        public string? description { get; set; }
        public string? canonicalTitle { get; set; }
        public string? averageRating { get; set; }
        public int? userCount { get; set; }
        public int? favoritesCount { get; set; }
        public int? popularityRank { get; set; }
        public string? startDate { get; set; }
        public string? endDate { get; set; }
        public int? ratingRank { get; set; }
        public string? ageRating { get; set; }
        public string? ageRatingGuide { get; set; }
        public string? subtype { get; set; }
        public string? AnimePosterImage { get; set; }
        public string? AnimeCoverImage { get; set; }
        public int? episodeCount { get; set; }
        public string? episodeLength { get; set; }
        // public int? totalLength { get; set; }
        public string? youtubeVideoId { get; set; }
        public string? showType { get; set; }
        #endregion

        public string? BroadcastDay { get; set; }
        public string? BroadcastTime { get; set; }
        public string? BroadcastTimezone { get; set; }

        public string? Youtube_id { get; set; }
        public string? Youtubeurl { get; set; }
        public string? Youtubeembed_url { get; set; }

        public string? Themetype { get; set; }
        public string? Themename { get; set; }
        public string? Themeurl { get; set; }

        public List<AnimeThemeSong>? ThemeSongs { get; set; }
        public ICollection<Producer>? producers { get; set; }
        public ICollection<Licensor>? licensors { get; set; }
        public ICollection<Relation>? relations { get; set; }
        public ICollection<Studio>? studios { get; set; }
        public ICollection<External>? external { get; set; }
        public ICollection<Streaming>? streaming { get; set; }
    }
    public class Producer : AnimeBaseReference { }
    public class Licensor : AnimeBaseReference { }
    public class Studio : AnimeBaseReference { }
    public class External : AnimeBaseReference { }
    public class Streaming : AnimeBaseReference { }
    public class AnimeThemeSong : AnimeBaseReference { }
    public class Relation : AnimeBaseReference 
    {
        public string? relation { get; set; }
        public List<Entry>? entry { get; set; }
    }
    public class Entry
    {
        public int? relationID { get; set; }
        public int? ID { get; set; }
        public int? mal_id { get; set; }
        public string? type { get; set; }
        public string? name { get; set; }
        public string? url { get; set; }
    }
    public abstract class AnimeBaseReference
    {
        public int Id { get; set; }               // PK
        public int AnimeId { get; set; }          // FK para Anime
        public Anime Anime { get; set; }
        public int? mal_id { get; set; }
        public string? type { get; set; }
        public string? name { get; set; }
        public string? url { get; set; }
    }
}
