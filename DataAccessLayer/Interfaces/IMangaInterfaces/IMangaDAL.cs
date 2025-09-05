using Entities.MangaS;
using Shared.Interfaces;

namespace DataAccessLayer.Interfaces.IMangaInterfaces
{
    public interface IMangaDAL : IMangaGet, IMangaPost,ICRUD<Manga>
    {
        
    }
}
