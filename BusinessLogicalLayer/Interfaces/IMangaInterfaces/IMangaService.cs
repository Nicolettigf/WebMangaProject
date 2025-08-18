using Entities.MangaS;
using Shared;

namespace BusinessLogicalLayer.Interfaces.IMangaInterfaces
{
    public interface IMangaService: IMangaGet,IMangaPost,ICRUD<Manga>
    {
        
    }
}
