using Entities.AnimeS;
using Shared.Interfaces;

namespace BusinessLogicalLayer.Interfaces.IAnimeInterfaces
{
    public interface IAnimeService :ICRUD<Anime>,IAnimePost,IAnimeGet
    {
    }
}
