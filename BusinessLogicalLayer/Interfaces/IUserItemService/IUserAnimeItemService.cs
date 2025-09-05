using Entities.AnimeS;
using Entities.UserS;
using Shared.Interfaces;

namespace BusinessLogicalLayer.Interfaces.IUserItemService
{
    public interface IUserAnimeItemService : ICrudUserItem<UserAnimeItem,Anime>
    {

    }
}
