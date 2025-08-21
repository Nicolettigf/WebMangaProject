using Shared.DTOS;
using Shared.Responses;

namespace BusinessLogicalLayer.Interfaces
{
    public interface IHomeService
    {
        Task<SingleResponse<Top7Data>> GetTopAnimeManga(int skip, int take);
    }
}
