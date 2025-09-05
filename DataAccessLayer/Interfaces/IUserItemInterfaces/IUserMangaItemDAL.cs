using Entities.MangaS;
using Entities.UserS;
using Shared.Interfaces;

namespace DataAccessLayer.Interfaces.IUserItem
{
    public interface IUserMangaItemDAL : ICrudUserItem<UserMangaItem,Manga>
    {

    }
}
