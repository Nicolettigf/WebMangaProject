using BusinessLogicalLayer.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using MvcPresentationLayer.Apis.MangaProjectApi.Animes;
using MvcPresentationLayer.Apis.MangaProjectApi.Mangas;
using Newtonsoft.Json;
using Shared;
using Shared.Models;
using Shared.Responses;

namespace MvcPresentationLayer.Utilities
{
    ///////////////////TERA QUE SER TRANFERIDO PARA A WEBAPI O LUGAR DELE NAO È NA TELA......///////////////////////
    public class CacheService : ICacheService
    {
        private readonly IMangaProjectApiAnimeService _animeApiService;
        private readonly IMangaProjectApiMangaService _mangaApiService;
        private readonly IDistributedCache _distributedCache;
        private readonly IHomeService _IHomeService;


        public CacheService(IMangaProjectApiAnimeService animeService, IMangaProjectApiMangaService mangaService,
                            IDistributedCache cache, IHomeService homeService)
        {
            _animeApiService = animeService;
            _mangaApiService = mangaService;
            _distributedCache = cache;
            _IHomeService = homeService;
        }

        //animes
        public async Task<DataResponse<MediaCatalog>> GetTop7AnimesCatalogByFavorites()
        {
            var json = await _distributedCache.GetStringAsync(LocationConstants.CacheKey.Anime.GetTop7AnimesCatalogByFavorites);
            if (json != null)
            {
                var MediaCatalog = JsonConvert.DeserializeObject<List<MediaCatalog>>(json);
                return ResponseFactory.CreateInstance().CreateResponseBasedOnCollectionData(MediaCatalog);
            }
            else
            {
                DataResponse<MediaCatalog> response = await _animeApiService.GetByFavorites(0, 7);
                if (response.HasSuccess)
                {
                    json = JsonConvert.SerializeObject(response.Data);
                    await _distributedCache.SetStringAsync(LocationConstants.CacheKey.Anime.GetTop7AnimesCatalogByFavorites, json);
                }
                return response;
            }
        }
        public async Task<DataResponse<MediaCatalog>> GetTop7AnimesCatalogByUserCount()
        {
            var json = await _distributedCache.GetStringAsync(LocationConstants.CacheKey.Anime.GetTop7AnimesCatalogByUserCount);
            if (json != null)
            {
                var MediaCatalog = JsonConvert.DeserializeObject<List<MediaCatalog>>(json);
                return ResponseFactory.CreateInstance().CreateResponseBasedOnCollectionData(MediaCatalog);
            }
            else
            {
                DataResponse<MediaCatalog> response = await _animeApiService.GetByUserCount(0, 7);
                if (response.HasSuccess)
                {
                    json = JsonConvert.SerializeObject(response.Data);
                    await _distributedCache.SetStringAsync(LocationConstants.CacheKey.Anime.GetTop7AnimesCatalogByUserCount, json);
                }
                return response;
            }
        }
        public async Task<DataResponse<MediaCatalog>> GetTop7AnimesCatalogByRating()
        {
            var json = await _distributedCache.GetStringAsync(LocationConstants.CacheKey.Anime.GetTop7AnimesCatalogByRating);
            if (json != null)
            {
                var MediaCatalog = JsonConvert.DeserializeObject<List<MediaCatalog>>(json);
                return ResponseFactory.CreateInstance().CreateResponseBasedOnCollectionData(MediaCatalog);
            }
            else
            {
                DataResponse<MediaCatalog> response = await _animeApiService.GetByRating(0, 7);
                if (response.HasSuccess)
                {
                    json = JsonConvert.SerializeObject(response.Data);
                    await _distributedCache.SetStringAsync(LocationConstants.CacheKey.Anime.GetTop7AnimesCatalogByRating, json);
                }
                return response;
            }
        }

        //mangas
        public async Task<DataResponse<MediaCatalog>> GetTop7MangasCatalogByFavorites()
        {
            var json = await _distributedCache.GetStringAsync(LocationConstants.CacheKey.Manga.GetTop7MangasCatalogByFavorites);
            if (json != null)
            {
                var mangasCatalog = JsonConvert.DeserializeObject<List<MediaCatalog>>(json);
                return ResponseFactory.CreateInstance().CreateResponseBasedOnCollectionData(mangasCatalog);
            }
            else
            {
                DataResponse<MediaCatalog> response = await _mangaApiService.GetByFavorites(0, 7);
                if (response.HasSuccess)
                {
                    json = JsonConvert.SerializeObject(response.Data);
                    await _distributedCache.SetStringAsync(LocationConstants.CacheKey.Manga.GetTop7MangasCatalogByFavorites, json);
                }
                return response;
            }
        }
        public async Task<DataResponse<MediaCatalog>> GetTop7MangasCatalogByUserCount()
        {
            var json = await _distributedCache.GetStringAsync(LocationConstants.CacheKey.Manga.GetTop7MangasCatalogByUserCount);
            if (json != null)
            {
                var mangasCatalog = JsonConvert.DeserializeObject<List<MediaCatalog>>(json);
                return ResponseFactory.CreateInstance().CreateResponseBasedOnCollectionData(mangasCatalog);
            }
            else
            {
                DataResponse<MediaCatalog> response = await _mangaApiService.GetByUserCount(0, 7);
                if (response.HasSuccess)
                {
                    json = JsonConvert.SerializeObject(response.Data);
                    await _distributedCache.SetStringAsync(LocationConstants.CacheKey.Manga.GetTop7MangasCatalogByUserCount, json);
                }
                return response;
            }
        }
        public async Task<DataResponse<MediaCatalog>> GetTop7MangasCatalogByRating()
        {
            var json = await _distributedCache.GetStringAsync(LocationConstants.CacheKey.Manga.GetTop7MangasCatalogByRating);
            if (json != null)
            {
                var mangasCatalog = JsonConvert.DeserializeObject<List<MediaCatalog>>(json);
                return ResponseFactory.CreateInstance().CreateResponseBasedOnCollectionData(mangasCatalog);
            }
            else
            {
                DataResponse<MediaCatalog> response = await _mangaApiService.GetByRating(0, 7);
                if (response.HasSuccess)
                {
                    json = JsonConvert.SerializeObject(response.Data);
                    await _distributedCache.SetStringAsync(LocationConstants.CacheKey.Manga.GetTop7MangasCatalogByRating, json);
                }
                return response;
            }
        }

        public async Task<SingleResponse<HomePageData>> GetTopAnimeMangaAsync(int skip, int take)
        {
            var cacheKey = new GetTopAnimeMangaCacheKey(skip, take).ToString();
            var json = await _distributedCache.GetStringAsync(cacheKey);

            if (json != null)
            {
                var cachedData = JsonConvert.DeserializeObject<HomePageData>(json);
                return ResponseFactory.CreateInstance().CreateSuccessSingleResponse<HomePageData>(cachedData);
            }
            else
            {
                var data = await _IHomeService.GetTopAnimeManga(skip, take);

                if (data != null)
                {
                    json = JsonConvert.SerializeObject(data.Item);
                    await _distributedCache.SetStringAsync(cacheKey, json, new DistributedCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
                    });
                }

                return ResponseFactory.CreateInstance().CreateSuccessSingleResponse<HomePageData>(data.Item);
            }
        }
    }

    public interface ICacheService
    {
        Task<SingleResponse<HomePageData>> GetTopAnimeMangaAsync(int skip, int take);
        Task<DataResponse<MediaCatalog>> GetTop7AnimesCatalogByFavorites();
        Task<DataResponse<MediaCatalog>> GetTop7AnimesCatalogByUserCount();
        Task<DataResponse<MediaCatalog>> GetTop7AnimesCatalogByRating();
        Task<DataResponse<MediaCatalog>> GetTop7MangasCatalogByFavorites();
        Task<DataResponse<MediaCatalog>> GetTop7MangasCatalogByUserCount();
        Task<DataResponse<MediaCatalog>> GetTop7MangasCatalogByRating();
    }


}
