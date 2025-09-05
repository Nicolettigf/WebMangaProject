using Shared.Responses;
using static Entities.Common.MediaBase;

namespace DataAccessLayer.Interfaces.IMangaInterfaces
{
    public interface IMangaPost
    {
        Task<Response> InsertCategory(Genre id);
    }
}
