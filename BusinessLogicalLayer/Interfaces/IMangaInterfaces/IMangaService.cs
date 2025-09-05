using Entities.MangaS;
using Shared.Interfaces;

namespace BusinessLogicalLayer.Interfaces.IMangaInterfaces
{
    public interface IMangaService: IMangaGet,IMangaPost,ICRUD<Manga>
    {
        
    }
}
