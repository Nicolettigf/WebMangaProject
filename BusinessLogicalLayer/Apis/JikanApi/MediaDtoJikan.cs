
using Entities;
using Entities.AnimeS;
using Entities.MangaS;

public class MediaDtoJikan
{
    // ===== Comuns =====
    public int? mal_id { get; set; }
    public string? url { get; set; }
    public bool? approved { get; set; }
    public string? title { get; set; }
    public string? title_english { get; set; }
    public string? title_japanese { get; set; }
    public string? type { get; set; }
    public string? status { get; set; }
    public double? score { get; set; }
    public int? scored_by { get; set; }
    public int? rank { get; set; }
    public int? popularity { get; set; }
    public int? members { get; set; }
    public int? favorites { get; set; }
    public string? synopsis { get; set; }
    public string? background { get; set; }
    public List<string> title_synonyms { get; set; }
    public List<ExplicitGenre>? explicit_genres { get; set; }
    public List<Title>? titles { get; set; }
    public List<Genre>? genres { get; set; }
    public List<Demographic>? demographics { get; set; }
    public List<Theme>? themes { get; set; }
    public Images? images { get; set; }

    // ===== Só Anime =====
    public string? source { get; set; }
    public int? episodes { get; set; }
    public bool? airing { get; set; }
    public string? duration { get; set; }
    public string? rating { get; set; }
    public string? season { get; set; }
    public int? year { get; set; }
    public Broadcast? broadcast { get; set; }
    public Trailer? trailer { get; set; }
    public Aired? aired { get; set; }
    public Theme2? theme2 { get; set; }
    public Theme? theme { get; set; }
    public List<External>? external { get; set; }
    public List<Producer>? producers { get; set; }
    public ICollection<Relation>? relations { get; set; }
    public List<Licensor>? licensors { get; set; }
    public List<Studio>? studios { get; set; }
    public List<Streaming>? streaming { get; set; }

    // ===== Só Manga =====
    public bool? publishing { get; set; }
    public int? chapters { get; set; }
    public int? volumes { get; set; }
    public List<Serialization> serializations { get; set; }
    public Published? published { get; set; }
    public List<Author>? authors { get; set; }

    public string? GetImageUrl(string format, string size)
    {
        return format.ToLower() switch
        {
            "jpg" => size.ToLower() switch
            {
                "small" => images?.jpg?.small_image_url,
                "large" => images?.jpg?.large_image_url,
                _ => images?.jpg?.image_url
            },
            "webp" => size.ToLower() switch
            {
                "small" => images?.webp?.small_image_url,
                "large" => images?.webp?.large_image_url,
                _ => images?.webp?.image_url
            },
            _ => null
        };
    }

    public List<T> MapList<TSource, T>(IEnumerable<TSource> source, Func<TSource, T> converter)
    {
        return source?.Select(converter).ToList() ?? new List<T>();
    }

    public void MapToMediaBase<T>(T media) where T : MediaBase
    {
        if (media == null) return;

        // Propriedades comuns
        media.Id = this.mal_id ?? 0;
        media.MalId = this.mal_id ?? 0;
        media.Title = this.title;
        media.TitleEnglish = this.title_english;
        media.TitleJapanese = this.title_japanese;
        media.TitleSynonyms = this.title_synonyms?.FirstOrDefault();
        media.Url = this.url;
        media.Approved = this.approved;
        media.Status = this.status;
        media.Type = this.type;
        media.Score = this.score;
        media.ScoredBy = this.scored_by;
        media.Rank = this.rank;
        media.Popularity = this.popularity;
        media.Members = this.members;
        media.Favorites = this.favorites;
        media.Synopsis = this.synopsis;
        media.Background = this.background;

        // Datas
        media.From = this.aired?.from ?? this.published?.from?.ToString("yyyy-MM-dd");
        media.To = this.aired?.to ?? this.published?.to?.ToString("yyyy-MM-dd");

        // Imagens
        media.JpgImageUrl = GetImageUrl("jpg", "default");
        media.JpgSmallImageUrl = GetImageUrl("jpg", "small");
        media.JpgLargeImageUrl = GetImageUrl("jpg", "large");
        media.WebpImageUrl = GetImageUrl("webp", "default");
        media.WebpSmallImageUrl = GetImageUrl("webp", "small");
        media.WebpLargeImageUrl = GetImageUrl("webp", "large");

        // Collections
        media.Themes = MapList(this.themes, t => new MediaBase.Theme
        {
            AnimeId = media is Anime a ? a.MalId : null,
            MangaId = media is Manga m ? m.MalId : null,
            Anime = media as Anime,
            Manga = media as Manga,
            MalId = t.mal_id,
            Type = t.type,
            Name = t.name,
            Url = t.url
        });

        media.Genres = MapList(this.genres, g => new MediaBase.Genre
        {
            AnimeId = media is Anime a ? a.MalId : null,
            MangaId = media is Manga m ? m.MalId : null,
            Anime = media as Anime,
            Manga = media as Manga,
            MalId = g.mal_id,
            Type = g.type,
            Name = g.name
        });

        media.ExplicitGenres = MapList(this.explicit_genres, e => new MediaBase.ExplicitGenre
        {
            AnimeId = media is Anime a ? a.MalId : null,
            MangaId = media is Manga m ? m.MalId : null,
            Anime = media as Anime,
            Manga = media as Manga,
            MalId = e.mal_id,
            Type = e.type,
            Name = e.name
        });

        media.Demographics = MapList(this.demographics, d => new MediaBase.Demographic
        {
            AnimeId = media is Anime a ? a.MalId : null,
            MangaId = media is Manga m ? m.MalId : null,
            Anime = media as Anime,
            Manga = media as Manga,
            MalId = d.mal_id,
            Type = d.type,
            Name = d.name
        });

        media.GenreItems = MapList(this.genres, d => new MediaBase.GenreItem
        {
            AnimeId = media is Anime a ? a.MalId : null,
            MangaId = media is Manga m ? m.MalId : null,
            Anime = media as Anime,
            Manga = media as Manga,
            MalId = d.mal_id,
            Type = d.type,
            Name = d.name
        });

        media.EnableEntity();
    }
}


#region roots
// DTOs
public class DataCategoria
{
    public int mal_id { get; set; }
    public string name { get; set; }
    public string url { get; set; }
    public int count { get; set; }
}
public class RootCate
{
    public List<DataCategoria> data { get; set; }
}

public class RootSingleJikan
{
    public MediaDtoJikan data { get; set; }
}
public class RootPageJikan
{
    public Pagination? pagination { get; set; }
    public List<MediaDtoJikan> data { get; set; }
}
#endregion

#region classes
public class Demographic
{
    public int? mal_id { get; set; }
    public string? type { get; set; }
    public string? name { get; set; }
    public string? url { get; set; }
}
public class Genre
{
    public int? mal_id { get; set; }
    public string? type { get; set; }
    public string? name { get; set; }
    public string? url { get; set; }
}
public class ExplicitGenre
{
    public int? mal_id { get; set; }
    public string? type { get; set; }
    public string? name { get; set; }
    public string? url { get; set; }
}
public class From
{
    public int? day { get; set; }
    public int? month { get; set; }
    public int? year { get; set; }
}
public class Images
{
    public Jpg? jpg { get; set; }
    public Webp? webp { get; set; }
    public string? image_url { get; set; }
    public string? small_image_url { get; set; }
    public string? medium_image_url { get; set; }
    public string? large_image_url { get; set; }
    public string? maximum_image_url { get; set; }
}
public class Jpg
{
    public string? image_url { get; set; }
    public string? small_image_url { get; set; }
    public string? large_image_url { get; set; }
}
public class Prop
{
    public From? from { get; set; }
    public To? to { get; set; }
    public string? @string { get; set; }
}
public class Webp
{
    public string? image_url { get; set; }
    public string? small_image_url { get; set; }
    public string? large_image_url { get; set; }
}
public class To
{
    public int? day { get; set; }
    public int? month { get; set; }
    public int? year { get; set; }
}
public class Title
{
    public string? type { get; set; }
    public string? title { get; set; }
}
public class Entry
{
    public int? mal_id { get; set; }
    public string? type { get; set; }
    public string? name { get; set; }
    public string? url { get; set; }
}
public class External
{
    public string? name { get; set; }
    public string? url { get; set; }
}
public class Items
{
    public int? count { get; set; }
    public int? total { get; set; }
    public int? per_page { get; set; }
}
public class Author
{
    public int? mal_id { get; set; }
    public string? type { get; set; }
    public string? name { get; set; }
    public string? url { get; set; }
}
public class Pagination
{
    public int? last_visible_page { get; set; }
    public bool? has_next_page { get; set; }
    public int? current_page { get; set; }
    public Items? items { get; set; }
}
public class Published
{
    public DateTime? from { get; set; }
    public DateTime? to { get; set; }
    public Prop? prop { get; set; }
    public string? @string { get; set; }
}
public class Serialization
{
    public int? mal_id { get; set; }
    public string? type { get; set; }
    public string? name { get; set; }
    public string? url { get; set; }
}
public class Aired
{
    public string? from { get; set; }
    public string? to { get; set; }
    public Prop? prop { get; set; }
}
public class Broadcast
{
    public string? day { get; set; }
    public string? time { get; set; }
    public string? timezone { get; set; }
    public string? @string { get; set; }
}
public class Licensor
{
    public int? mal_id { get; set; }
    public string? type { get; set; }
    public string? name { get; set; }
    public string? url { get; set; }
}
public class Producer
{
    public int? mal_id { get; set; }
    public string? type { get; set; }
    public string? name { get; set; }
    public string? url { get; set; }
}
public class Relation
{
    public string? relation { get; set; }
    public List<Entry>? entry { get; set; }
}
public class Streaming
{
    public string? name { get; set; }
    public string? url { get; set; }
}
public class Theme2
{
    public List<string?> openings { get; set; }
    public List<string?> endings { get; set; }
}
public class Trailer
{
    public string? youtube_id { get; set; }
    public string? url { get; set; }
    public string? embed_url { get; set; }
    public Images? images { get; set; }
}
public class Theme
{
    public int? mal_id { get; set; }
    public string? type { get; set; }
    public string? name { get; set; }
    public string? url { get; set; }
}
public class Studio
{
    public int? mal_id { get; set; }
    public string? type { get; set; }
    public string? name { get; set; }
    public string? url { get; set; }
}
#endregion

