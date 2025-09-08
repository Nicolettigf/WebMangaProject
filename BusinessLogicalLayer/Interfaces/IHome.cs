using Shared.Models;
using Shared.Responses;

namespace BusinessLogicalLayer.Interfaces
{
    public interface IHomeService
    {
        Task<SingleResponse<HomePageData>> GetTopAnimeManga(int skip, int take);

        Task<SingleResponse<ItemPageData>> GetHomeMedia(int skip, int take,string TableName);

        Task<SingleResponse<SearchData>> GetByName(string name);
    }
}
