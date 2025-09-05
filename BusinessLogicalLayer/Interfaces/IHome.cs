using Shared.Models;
using Shared.Responses;

namespace BusinessLogicalLayer.Interfaces
{
    public interface IHomeService
    {
        Task<SingleResponse<HomePageData>> GetTopAnimeManga(int skip, int take);
    }
}
