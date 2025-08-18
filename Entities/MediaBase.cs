using Entities.AnimeS;
using Entities.MangaS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    // Entidade base para elementos comuns entre Anime e Manga
    public abstract class MediaBase : Entity
    {
        public int MalId { get; set; }
        public string? Url { get; set; }
        public bool? Approved { get; set; }
        public string? Title { get; set; }
        public string? TitleEnglish { get; set; }
        public string? TitleJapanese { get; set; }
        public string? Status { get; set; }
        public string? Type { get; set; }
        public double? Score { get; set; }
        public int? ScoredBy { get; set; }
        public int? Rank { get; set; }
        public int? Popularity { get; set; }
        public int? Members { get; set; }
        public int? Favorites { get; set; }
        public string? Synopsis { get; set; }
        public string? Background { get; set; }
        public string? From { get; set; }
        public string? To { get; set; }

        public MediaRatingFrequency? MediaRatingFrequency { get; set; }
        public Images? Imagens { get; set; }

        //public List<string>? TitleSynonyms { get; set; }
        public ICollection<ExplicitGenre>? ExplicitGenres { get; set; }
        public ICollection<Demographic>? Demographics { get; set; } = new List<Demographic>();
        public ICollection<Genre>? Genres { get; set; } = new List<Genre>();
        public ICollection<Theme>? Themes { get; set; } = new List<Theme>();

        public class ExplicitGenre : BaseClassesApi { }

        public class Demographic : BaseClassesApi { }

        public class Genre : BaseClassesApi
        {
            [NotMapped]
            public ICollection<Manga>? MangasID { get; set; }

            [NotMapped]
            public ICollection<Anime>? AnimesID { get; set; }

        }

        public class Theme : BaseClassesApi { }

        public class Images : BaseClassesApi
        {
            public string? JpgImageUrl { get; set; }
            public string? JpgSmallImageUrl { get; set; }
            public string? JpgLargeImageUrl { get; set; }
            public string? WebpImageUrl { get; set; }
            public string? WebpSmallImageUrl { get; set; }
            public string? WebpLargeImageUrl { get; set; }
        }
    }
    public abstract class BaseClassesApi
    {
        public int Id { get; set; }           // PK
        public int? AnimeId { get; set; }     // FK opcional
        public Anime? Anime { get; set; }
        public int? MangaId { get; set; }     // FK opcional
        public Manga? Manga { get; set; }
        public int? MalId { get; set; }   // mal_id da API
        public string? Type { get; set; }
        public string? Name { get; set; }
        public string? Url { get; set; }

    }
}
