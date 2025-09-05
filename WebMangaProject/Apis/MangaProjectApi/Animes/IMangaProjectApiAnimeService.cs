using Entities.AnimeS;
using Shared.Interfaces;

namespace MvcPresentationLayer.Apis.MangaProjectApi.Animes
{
    public interface IMangaProjectApiAnimeService : IMangaProjectApiService<Anime>, IUsualGetInterfaces<Anime>
    {

    }
}
