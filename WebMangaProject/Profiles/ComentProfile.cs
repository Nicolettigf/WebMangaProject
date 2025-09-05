using AutoMapper;
using Entities.MangaS;
using MvcPresentationLayer.Models.AnimeModel;
using MvcPresentationLayer.Models.MangaModels;

namespace MvcPresentationLayer.Profiles
{
    public class ComentProfile:Profile
    {
        public ComentProfile()
        {
            CreateMap<MangaComentaryViewModel, MangaComentary>();
            CreateMap<MangaComentary, MangaComentaryViewModel>();

            CreateMap<AnimeComentaryViewModel, MangaComentary>();
            CreateMap<MangaComentary, AnimeComentaryViewModel>();

        }
    }
}
