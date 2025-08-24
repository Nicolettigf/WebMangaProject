namespace Entities
{
    public class KitsuMember : MediaBase
    {
        public int? IdKitsu { get; set; }         
        public bool? Nsfw { get; set; }        
        public string? YoutubeVideoId { get; set; }
        public int? EpisodeLength { get; set; }   
        public string? PosterImageLarge { get; set; } 
        public string? CoverImageLarge { get; set; } 
    }
}
