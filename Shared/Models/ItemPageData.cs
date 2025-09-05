namespace Shared.Models
{
    public class ItemPageData
    {
        public List<MediaCatalog> TopByRank { get; set; } = new();
        public List<MediaCatalog> TopByMembers { get; set; } = new();
        public List<MediaCatalog> TopByFavorites { get; set; } = new();
        public List<MediaCatalog> TopByScore { get; set; } = new();
        public List<MediaCatalog> TopByPopularity { get; set; } = new();
        public List<MediaCatalog> TopByScoredBy { get; set; } = new();
        public List<MediaCatalog> Latest { get; set; } = new();
    }
}
