using Entities.AnimeS;
using Entities.MangaS;
using Shared.Interfaces;
using Shared.Responses;

namespace DataAccessLayer.Interfaces.IUserComentary
{
    public interface IMangaComentaryDAL : ICRUD<MangaComentary>
    {
        Task<DataResponse<MangaComentary>> GetByUser(int userid);
        Task<DataResponse<MangaComentary>> GetByManga(int MangaID);

    }
}
