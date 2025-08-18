// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
using static BusinessLogicalLayer.ApiConsumer.DTOBASE;


public class Datum
{
    public int? mal_id { get; set; }
    public string? url { get; set; }
    public Images? images { get; set; }
    public bool? approved { get; set; }
    public List<Title>? titles { get; set; }
    public string? title { get; set; }
    public string? title_english { get; set; }
    public string? title_japanese { get; set; }
    public List<string>? title_synonyms { get; set; }
    public string? type { get; set; }
    public int? chapters { get; set; }
    public int? volumes { get; set; }
    public string? status { get; set; }
    public bool? publishing { get; set; }
    public Published? published { get; set; }
    public double? score { get; set; }
    public double? scored { get; set; }
    public int? scored_by { get; set; }
    public int? rank { get; set; }
    public int? popularity { get; set; }
    public int? members { get; set; }
    public int? favorites { get; set; }
    public string? synopsis { get; set; }
    public string? background { get; set; }
    public List<Author>? authors { get; set; }
    public List<Serialization>? serializations { get; set; }
    public List<Genre>? genres { get; set; }
    public List<ExplicitGenre>? explicit_genres { get; set; }
    public List<Theme>? themes { get; set; }
    public List<Demographic>? demographics { get; set; }
}
public class Root
{
    public Pagination? pagination { get; set; }
    public List<Datum>? data { get; set; }
}
