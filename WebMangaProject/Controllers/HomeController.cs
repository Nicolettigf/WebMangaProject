using AutoMapper;
using BusinessLogicalLayer.Interfaces;
using Entities.AnimeS;
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
        private readonly IKitsuApi _KitsuApi;
        public HomeController(IMapper mapper, ICacheService cacheService, IJikanApi JikanApi, IKitsuApi kitsuApi)
        {
            this._mapper = mapper;
            this._cacheService = cacheService;
            this._JikanApi = JikanApi;
            this._KitsuApi = kitsuApi;
        }

        public async Task<IActionResult> Index()
        {
            //Task.Run(async () =>
            //{
            await _KitsuApi.BuscarECompararAnimePorIds(20000);
            await _JikanApi.ConsumeAnime();
            await _JikanApi.ConsumeManga();
            await _JikanApi.ConsumeMissingAnime();
            await _JikanApi.ConsumeMissingMangas();
            await _KitsuApi.BuscarPagina<Anime>("anime");
                await _JikanApi.ConsumeGenre();
            //});

            var HomeAnimeEMangas = await _cacheService.GetTopAnimeMangaAsync(0,7);

            if (!HomeAnimeEMangas.HasSuccess)
            {
                return BadRequest(HomeAnimeEMangas);
            }

            HomePageViewModel homePageViewModel = new()
            {
                AnimesFavorites = _mapper.Map<List<AnimeShortViewModel>>(HomeAnimeEMangas.Item.TopAnimeByFavorites),
                AnimesByCount = _mapper.Map<List<AnimeShortViewModel>>(HomeAnimeEMangas.Item.TopAnimeByMembers),
                AnimesByRating = _mapper.Map<List<AnimeShortViewModel>>(HomeAnimeEMangas.Item.TopAnimeByRank),
                MangasFavorites = _mapper.Map<List<MangaShortViewModel>>(HomeAnimeEMangas.Item.TopMangaByFavorites),
                MangasByCount = _mapper.Map<List<MangaShortViewModel>>(HomeAnimeEMangas.Item.TopMangaByMembers),
                MangasByRating = _mapper.Map<List<MangaShortViewModel>>(HomeAnimeEMangas.Item.TopMangaByRank),
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