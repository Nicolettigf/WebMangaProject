using Entities;
using Shared.Responses;
using static Entities.MediaBase;

namespace BusinessLogicalLayer.Interfaces.IMangaInterfaces
{
    public interface IMangaPost
    {
        Task<Response> InsertCategory(Genre id);
    }
}
