using Shared.Models;
using Shared.Responses;

namespace Shared.Interfaces
{
    public interface IUsualGetInterfaces<W>
    {
        Task<DataResponse<MediaCatalog>> GetByUserCount(int skip, int take);
        Task<DataResponse<MediaCatalog>> GetByFavorites(int skip, int take);
        Task<DataResponse<MediaCatalog>> GetByRating(int skip, int take);
        Task<DataResponse<MediaCatalog>> GetByPopularity(int skip, int take);
        Task<DataResponse<MediaCatalog>> GetHome(int skip, int take);
        Task<DataResponse<MediaCatalog>> GetByCatalog(int skip, int take, string catalog);




        Task<DataResponse<W>> GetByCategory(int ID);
        Task<DataResponse<W>> Get(string name);
        Task<SingleResponse<W>> GetComplete(int ID);
        Task<int> GetLastIndexCategory();
        Task<int> GetLastIndex();
    }
}
