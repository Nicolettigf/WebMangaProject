using Entities.AnimeS;
using Shared.Interfaces;

namespace DataAccessLayer.Interfaces.IAnimeInterfaces
{
    public interface IAnimeDAL : ICRUD<Anime>,IAnimeGet,IAnimePost
    {
    }
}
