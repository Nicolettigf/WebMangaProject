// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
using static BusinessLogicalLayer.ApiConsumer.DTOBASE;

public class DataAni
{
    public int? mal_id { get; set; }
    public string? url { get; set; }
    public bool? approved { get; set; }
    public string? title { get; set; }
    public string? title_english { get; set; }
    public string? title_japanese { get; set; }
    public string? type { get; set; }
    public string? source { get; set; }
    public int? episodes { get; set; }
    public bool? airing { get; set; }
    public string? status { get; set; }
    public string? duration { get; set; }
    public string? rating { get; set; }
    public double? score { get; set; }
    public int? scored_by { get; set; }
    public int? rank { get; set; }
    public int? popularity { get; set; }
    public int? members { get; set; }
    public int? favorites { get; set; }
    public string? synopsis { get; set; }
    public string? background { get; set; }
    public string? season { get; set; }
    public int? year { get; set; }
    public Images? images { get; set; }
    public Broadcast? broadcast { get; set; }
    public Trailer? trailer { get; set; }
    public Aired? aired { get; set; }
    public Theme? theme { get; set; }
    public List<string?> title_synonyms { get; set; }
    public Theme2? theme2 { get; set; }
    public List<ExplicitGenre>? explicit_genres { get; set; }
    public List<Demographic>? demographics { get; set; }
    public List<Genre>? genres { get; set; }
    public List<Title>? titles { get; set; }

    public List<Producer>? producers { get; set; }
    public List<Licensor>? licensors { get; set; }
    public List<Relation>? relations { get; set; }
    public List<Studio>? studios { get; set; }
    public List<Theme>? themes { get; set; }
    public List<External>? external { get; set; }
    public List<Streaming>? streaming { get; set; }
}

public class RootAni
{
    public DataAni? data { get; set; }
}




