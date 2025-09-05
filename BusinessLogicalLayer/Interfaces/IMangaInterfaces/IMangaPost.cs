using Entities.Common;
using Shared.Responses;

namespace BusinessLogicalLayer.Interfaces.IMangaInterfaces
{
    public interface IMangaPost
    {
        Task<Response> InsertCategory(MediaBase.Genre id);
    }
}
