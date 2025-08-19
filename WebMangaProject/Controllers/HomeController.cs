using AutoMapper;
using BusinessLogicalLayer.ApiConsumer.AnimeApi;
using BusinessLogicalLayer.ApiConsumer.CategoryApi;
using BusinessLogicalLayer.ApiConsumer.MangaApi;
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
        private readonly IAnimeApiConnect _AnimeApi;
        private readonly IMangaApi _ApiConnect;
        private readonly ICategoryApiConnect _CategoryApiConnect;

        
        public HomeController(IMapper mapper, ICacheService cacheService, IAnimeApiConnect AnimeApi,
            IMangaApi ApiConnect, ICategoryApiConnect CategoryApiConnect)
        {
            this._mapper = mapper;
            this._cacheService = cacheService;
            this._ApiConnect = ApiConnect;
            this._AnimeApi = AnimeApi;
            this._CategoryApiConnect = CategoryApiConnect;
        }

        public async Task<IActionResult> Index()
        {
            await _AnimeApi.ConsumeMissingAnimes();
            await _ApiConnect.ConsumeMissingMangas();
            //await _CategoryApiConnect.CovertiCatego();
            //await _ApiConnect.Consume();
            //await _AnimeApi.ConsumeAnime();


            var responseAnimesFavorites = await _cacheService.GetTop7AnimesCatalogByFavorites();
            var responseAnimesByCount = await _cacheService.GetTop7AnimesCatalogByUserCount();
            var responseAnimesByRating = await _cacheService.GetTop7AnimesCatalogByRating();

            if (!responseAnimesFavorites.HasSuccess || !responseAnimesByCount.HasSuccess || !responseAnimesByRating.HasSuccess)
            {
                return BadRequest(responseAnimesFavorites);
            }

            List<AnimeShortViewModel> animesFavorites =
                _mapper.Map<List<AnimeShortViewModel>>(responseAnimesFavorites.Data);

            List<AnimeShortViewModel> animesByCount =
                _mapper.Map<List<AnimeShortViewModel>>(responseAnimesByCount.Data);

            List<AnimeShortViewModel> animesByRating = _mapper.Map<List<AnimeShortViewModel>>(responseAnimesByRating.Data);

            DataResponse<MangaCatalog> responseMangaFavorites = await _cacheService.GetTop7MangasCatalogByFavorites();
            DataResponse<MangaCatalog> responseMangaCount = await _cacheService.GetTop7MangasCatalogByUserCount();
            DataResponse<MangaCatalog> responseMangaRating = await _cacheService.GetTop7MangasCatalogByRating();

            if (!responseMangaFavorites.HasSuccess || !responseMangaCount.HasSuccess || !responseMangaRating.HasSuccess)
            {
                return BadRequest(responseMangaFavorites);
            }

            List<MangaShortViewModel> MangaFavorite =
                _mapper.Map<List<MangaShortViewModel>>(responseMangaFavorites.Data);

            List<MangaShortViewModel> MangaCount =
                _mapper.Map<List<MangaShortViewModel>>(responseMangaCount.Data);

            List<MangaShortViewModel> MangaRating = _mapper.Map<List<MangaShortViewModel>>(responseMangaRating.Data);

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