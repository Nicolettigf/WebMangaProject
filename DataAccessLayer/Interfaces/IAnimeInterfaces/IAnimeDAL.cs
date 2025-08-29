using Entities.AnimeS;
using Shared;

namespace DataAccessLayer.Interfaces.IAnimeInterfaces
{
    public interface IAnimeDAL : ICRUD<Anime>,IAnimeGet,IAnimePost
    {
    }
}
