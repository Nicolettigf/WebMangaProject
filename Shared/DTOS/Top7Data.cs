using Shared.Models.Anime;
using Shared.Models.Manga;

namespace Shared.DTOS
{
    public class Top7Data
    {
        public List<AnimeCatalog> TopAnimeByRank { get; set; }
        public List<AnimeCatalog> TopAnimeByMembers { get; set; }
        public List<AnimeCatalog> TopAnimeByFavorites { get; set; }
        public List<MangaCatalog> TopMangaByRank { get; set; }
        public List<MangaCatalog> TopMangaByMembers { get; set; }
        public List<MangaCatalog> TopMangaByFavorites { get; set; }
        public List<AnimeCatalog> LatestAnimes { get; set; }
    }

}
