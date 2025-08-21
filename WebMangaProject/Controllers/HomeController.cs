using AutoMapper;
using BusinessLogicalLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MvcPresentationLayer.Models.AnimeModel;
using MvcPresentationLayer.Models.HomePage;
using MvcPresentationLayer.Models.MangaModels;
using MvcPresentationLayer.Utilities;
using Shared.Models.Manga;
using Shared.Responses;
using System.Diagnostics;
using WebMangaProject.Models;

namespace MvcPresentationLayer.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;
        private readonly IJikanApi _JikanApi;
        
        public HomeController(IMapper mapper, ICacheService cacheService, IJikanApi JikanApi)
        {
            this._mapper = mapper;
            this._cacheService = cacheService;
            this._JikanApi = JikanApi;
        }

        public async Task<IActionResult> Index()
        {
            //await _JikanApi.ConsumeManga();
            //await _JikanApi.ConsumeAnime();
            //await _JikanApi.ConsumeMissingAnime();
            //await _JikanApi.ConsumeMissingMangas();
            //await _JikanApi.ConsumeGenre();
            var HomeAnimeEMangas = await _cacheService.GetTopAnimeMangaAsync(7,7);

            //var responseAnimesFavorites = await _cacheService.GetTop7AnimesCatalogByFavorites();
            //var responseAnimesByCount = await _cacheService.GetTop7AnimesCatalogByUserCount();
            //var responseAnimesByRating = await _cacheService.GetTop7AnimesCatalogByRating();


            if (!HomeAnimeEMangas.HasSuccess)
            {
                return BadRequest(HomeAnimeEMangas);
            }

            List<AnimeShortViewModel> animesFavorites = _mapper.Map<List<AnimeShortViewModel>>(HomeAnimeEMangas.Item.TopAnimeByFavorites);
            List<AnimeShortViewModel> animesByCount =_mapper.Map<List<AnimeShortViewModel>>(HomeAnimeEMangas.Item.TopAnimeByMembers);
            List<AnimeShortViewModel> animesByRating = _mapper.Map<List<AnimeShortViewModel>>(HomeAnimeEMangas.Item.TopAnimeByRank);

            //DataResponse<MangaCatalog> responseMangaFavorites = await _cacheService.GetTop7MangasCatalogByFavorites();
            //DataResponse<MangaCatalog> responseMangaCount = await _cacheService.GetTop7MangasCatalogByUserCount();
            //DataResponse<MangaCatalog> responseMangaRating = await _cacheService.GetTop7MangasCatalogByRating();

            if (!HomeAnimeEMangas.HasSuccess)
            {
                return BadRequest(HomeAnimeEMangas);
            }

            List<MangaShortViewModel> MangaFavorite = _mapper.Map<List<MangaShortViewModel>>(HomeAnimeEMangas.Item.TopMangaByFavorites);

            List<MangaShortViewModel> MangaCount = _mapper.Map<List<MangaShortViewModel>>(HomeAnimeEMangas.Item.TopMangaByMembers);

            List<MangaShortViewModel> MangaRating = _mapper.Map<List<MangaShortViewModel>>(HomeAnimeEMangas.Item.TopMangaByRank);

            HomePageViewModel homePageViewModel = new()
            {
                MangasFavorites = MangaFavorite,
                MangasByCount = MangaCount,
                MangasByRating = MangaRating,
                AnimesFavorites = animesFavorites,
                AnimesByCount = animesByCount,
                AnimesByRating = animesByRating,
            };
            return View(homePageViewModel);
        }
        public IActionResult AboutUs()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}