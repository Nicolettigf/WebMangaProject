using Entities.MangaS;
using Shared.Interfaces;

namespace MvcPresentationLayer.Apis.MangaProjectApi.Mangas
{
    public interface IMangaProjectApiMangaService : IMangaProjectApiService<Manga>, IUsualGetInterfaces<Manga>
    {
    
    }
}
