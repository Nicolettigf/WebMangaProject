namespace Shared.Models
{
    public class HomePageData
    {
        public List<MediaCatalog> TopAnimeByRank { get; set; }
        public List<MediaCatalog> TopAnimeByMembers { get; set; }
        public List<MediaCatalog> TopAnimeByFavorites { get; set; }
        public List<MediaCatalog> TopMangaByRank { get; set; }
        public List<MediaCatalog> TopMangaByMembers { get; set; }
        public List<MediaCatalog> TopMangaByFavorites { get; set; }
        public List<MediaCatalog> LatestAnimes { get; set; }
    }

}
