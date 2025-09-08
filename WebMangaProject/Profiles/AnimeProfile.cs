using AutoMapper;
using Entities.AnimeS;
using MvcPresentationLayer.Models.AnimeModel;
using Shared.Models;

namespace MvcPresentationLayer.Profiles
{
    public class AnimeProfile : Profile
    {
        public AnimeProfile()
        {
            CreateMap<AnimeShortViewModel, MediaCatalog>();
            CreateMap<MediaCatalog, AnimeShortViewModel>();

            CreateMap<AnimeShortViewModel, Anime>();
            CreateMap<Anime, AnimeShortViewModel>();

            CreateMap<AnimeOnpageViewModel, Anime>();
            CreateMap<Anime, AnimeOnpageViewModel>();

            CreateMap<AnimeComentaryViewModel, AnimeComentary>();
            CreateMap<AnimeComentary, AnimeComentaryViewModel>();

            
        }
    }
}
