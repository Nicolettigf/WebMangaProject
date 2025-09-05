using Entities.AnimeS;
using Entities.UserS;
using Shared.Interfaces;
using Shared.Responses;

namespace DataAccessLayer.Interfaces.IUserItem
{
    public interface IUserAnimeItemDAL : ICrudUserItem<UserAnimeItem,Anime>
    {
       
    }
}
