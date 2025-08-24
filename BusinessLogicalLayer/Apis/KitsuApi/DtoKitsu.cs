// Root myDeserializedClass = JsonConvert.Deserializeobject?<Root>(myJsonResponse);
using Entities;
using Newtonsoft.Json;
using Shared.Extensions;
public class RootKitsu
{
    public List<DatumKitsu> data { get; set; }
    public Meta? meta { get; set; }
    public links? links { get; set; }
}

public class DatumKitsu
{
    public string? id { get; set; }
    public string? type { get; set; }
    public links? links { get; set; }
    public Attributes? attributes { get; set; }
    public Relationships? relationships { get; set; }

    public bool IsMatch(string dbMedia)
    {
        var titles = new List<string>
            {
                this.attributes.canonicalTitle?.NormalizeTitle(),
                this.attributes.titles?.en.NormalizeTitle(),
                this.attributes.titles?.en_us.NormalizeTitle(),
                this.attributes.titles?.en_jp.NormalizeTitle(),
                this.attributes.titles?.ja_jp.NormalizeTitle()
            }
        .Where(t => !string.IsNullOrEmpty(t))
        .ToList();

        var normalizedDbTitle = dbMedia.NormalizeTitle() ?? "";

        bool titleMatch = titles.Any(t => t == normalizedDbTitle);

        // Aqui você pode reativar comparações extras se quiser:
        // bool dateMatch = !string.IsNullOrWhiteSpace(this.attributes.startDate) &&
        //                  dbMedia.StartDate?.ToString("yyyy-MM-dd") == this.attributes.startDate;

        // bool episodesMatch = this.attributes.episodeCount == null ||
        //                      dbMedia.EpisodeCount == this.attributes.episodeCount;

        // bool subtypeMatch = string.IsNullOrEmpty(this.attributes.subtype) ||
        //                     dbMedia.Type?.ToLowerInvariant() == this.attributes.subtype.ToLowerInvariant();

        return titleMatch; // && dateMatch && episodesMatch && subtypeMatch;
    }
}
public class RootKitsuUnitario
{
    public DatumKitsu data { get; set; }
}


public class Meta
{
    public Dimensions? dimensions { get; set; }
    public int? count { get; set; }
}
public class Dimensions
{
    public Tiny? tiny { get; set; }
    public Large? large { get; set; }
    public Small? small { get; set; }
    public Medium? medium { get; set; }
    public TinyWebp? tiny_webp { get; set; }
    public LargeWebp? large_webp { get; set; }
    public SmallWebp? small_webp { get; set; }
}
public class links
{
    public string? self { get; set; }
    public string? related { get; set; }
    public string? first { get; set; }
    public string? next { get; set; }
    public string? last { get; set; }
}
public class Attributes
{
    public DateTime? createdAt { get; set; }
    public DateTime? updatedAt { get; set; }
    public string? slug { get; set; }
    public string? synopsis { get; set; }
    public string? description { get; set; }
    public int? coverImageTopOffset { get; set; }
    public Titles? titles { get; set; }
    public string? canonicalTitle { get; set; }
    public List<string?> abbreviatedTitles { get; set; }
    public string? averageRating { get; set; }
    public RatingFrequencies? ratingFrequencies { get; set; }
    public int? userCount { get; set; }
    public int? favoritesCount { get; set; }
    public string? startDate { get; set; }
    public string? endDate { get; set; }
    public object? nextRelease { get; set; }
    public int? popularityRank { get; set; }
    public int? ratingRank { get; set; }
    public string? ageRating { get; set; }
    public string? ageRatingGuide { get; set; }
    public string? subtype { get; set; }
    public string? status { get; set; }
    public string? tba { get; set; }
    public PosterImage? posterImage { get; set; }
    public CoverImage? coverImage { get; set; }
    public int? episodeCount { get; set; }
    public int? episodeLength { get; set; }
    public int? totalLength { get; set; }
    public string? youtubeVideoId { get; set; }
    public string? showType { get; set; }
    public bool? nsfw { get; set; }
    public int? chapterCount { get; set; }
    public int? volumeCount { get; set; }
    public string? serialization { get; set; }
    public string? mangaType { get; set; }
}
public class Medium
{
    public int? width { get; set; }
    public int? height { get; set; }
}
public class Large
{
    public int? width { get; set; }
    public int? height { get; set; }
}
public class PosterImage
{
    public string? tiny { get; set; }
    public string? large { get; set; }
    public string? small { get; set; }
    public string? medium { get; set; }
    public string? original { get; set; }
    public Meta meta { get; set; }
}
public class RatingFrequencies
{
    [JsonProperty("2")]
    public string? _2 { get; set; }

    [JsonProperty("3")]
    public string? _3 { get; set; }

    [JsonProperty("4")]
    public string? _4 { get; set; }

    [JsonProperty("5")]
    public string? _5 { get; set; }

    [JsonProperty("6")]
    public string? _6 { get; set; }

    [JsonProperty("7")]
    public string? _7 { get; set; }

    [JsonProperty("8")]
    public string? _8 { get; set; }

    [JsonProperty("9")]
    public string? _9 { get; set; }

    [JsonProperty("10")]
    public string? _10 { get; set; }

    [JsonProperty("11")]
    public string? _11 { get; set; }

    [JsonProperty("12")]
    public string? _12 { get; set; }

    [JsonProperty("13")]
    public string? _13 { get; set; }

    [JsonProperty("14")]
    public string? _14 { get; set; }

    [JsonProperty("15")]
    public string? _15 { get; set; }

    [JsonProperty("16")]
    public string? _16 { get; set; }

    [JsonProperty("17")]
    public string? _17 { get; set; }

    [JsonProperty("18")]
    public string? _18 { get; set; }

    [JsonProperty("19")]
    public string? _19 { get; set; }

    [JsonProperty("20")]
    public string? _20 { get; set; }
}
public class Relationships
{
    public StreamingLinks? streamingLinks { get; set; }
    public Genres? genres { get; set; }
    public Categories? categories { get; set; }
    public Castings? castings { get; set; }
    public Installments? installments { get; set; }
    public Mappings? mappings { get; set; }
    public Reviews? reviews { get; set; }
    public MediaRelationships? mediaRelationships { get; set; }
    public Characters? characters { get; set; }
    public Staff? staff { get; set; }
    public Productions? productions { get; set; }
    public Quotes? quotes { get; set; }
    public Episodes? episodes { get; set; }
    public Streaminglinks? streaminglinks { get; set; }
    public AnimeProductions? animeProductions { get; set; }
    public AnimeCharacters? animeCharacters { get; set; }
    public AnimeStaff? animeStaff { get; set; }
    public Chapters? chapters { get; set; }
    public MangaCharacters? mangaCharacters { get; set; }
    public MangaStaff? mangaStaff { get; set; }
}
public class Tiny
{
    public int? width { get; set; }
    public int? height { get; set; }
}
public class Titles
{
    public string? en { get; set; }
    public string? en_jp { get; set; }
    public string? ja_jp { get; set; }
    public string? en_us { get; set; }
}
public class Small
{
    public int? width { get; set; }
    public int? height { get; set; }
}
public class Staff
{
    public links? links { get; set; }
}
public class Streaminglinks
{
    public links? links { get; set; }
}
public class AnimeCharacters
{
    public links? links { get; set; }
}
public class AnimeProductions
{
    public links? links { get; set; }
}
public class AnimeStaff
{
    public links? links { get; set; }
}
public class Reviews
{
    public links? links { get; set; }
}
public class Productions
{
    public links? links { get; set; }
}
public class Quotes
{
    public links? links { get; set; }
}
public class Mappings
{
    public links? links { get; set; }
}
public class MediaRelationships
{
    public links? links { get; set; }
}
public class Castings
{
    public links? links { get; set; }
}
public class Categories
{
    public links? links { get; set; }
}
public class Characters
{
    public links? links { get; set; }
}
public class Episodes
{
    public links? links { get; set; }
}
public class Genres
{
    public links? links { get; set; }
}
public class Installments
{
    public links? links { get; set; }
}
public class CoverImage
{
    public string? tiny { get; set; }
    public string? large { get; set; }
    public string? small { get; set; }
    public string? original { get; set; }
    public Meta? meta { get; set; }
    public string? tiny_webp { get; set; }
    public string? large_webp { get; set; }
    public string? small_webp { get; set; }
}
public class Chapters
{
    public links? links { get; set; }
}
public class LargeWebp
{
    public int? width { get; set; }
    public int? height { get; set; }
}
public class MangaCharacters
{
    public links? links { get; set; }
}
public class StreamingLinks
{
    public links? links { get; set; }
}
public class MangaStaff
{
    public links? links { get; set; }
}
public class SmallWebp
{
    public int? width { get; set; }
    public int? height { get; set; }
}
public class TinyWebp
{
    public int? width { get; set; }
    public int? height { get; set; }
}


