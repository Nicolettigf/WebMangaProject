using BusinessLogicalLayer.Interfaces;
using DataAccessLayer.UnitOfWork;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Shared.Extensions;
using Shared.Models;
using Shared.Responses;

namespace BusinessLogicalLayer.Implementations
{
    public class HomeService : IHomeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<SingleResponse<SearchData>> GetByName(string name)
        {
            return await _unitOfWork.HomeRepository.GetByName(name);
        }

        public async Task<SingleResponse<ItemPageData>> GetHomeMedia(int skip, int take, string tableName)
        {
            using var reader = await _unitOfWork.HomeRepository.GetHomeMedia(skip, take, tableName);
            var pageData = new ItemPageData();

            do
            {
                // Lê a primeira linha do result set para pegar o ListType
                var hasRow = await reader.ReadAsync();
                if (!hasRow) continue;

                var listType = reader["ListType"].ToString();

                // Mapear todo o result set
                var list = await reader.MapToListAsync<MediaCatalog>();

                // Direciona a lista correta
                switch (listType)
                {
                    case "TopByPopularity":
                        pageData.TopByPopularity = list;
                        break;
                    case "TopByFavorites":
                        pageData.TopByFavorites = list;
                        break;
                    case "TopByMembers":
                        pageData.TopByMembers = list;
                        break;
                    case "TopByRank":
                        pageData.TopByRank = list;
                        break;
                    case "TopByScore":
                        pageData.TopByScore = list;
                        break;
                    case "TopByScoredBy":
                        pageData.TopByScoredBy = list;
                        break;
                    case "Latest":
                        pageData.Latest = list;
                        break;
                }

            } while (await reader.NextResultAsync());

            return ResponseFactory.CreateInstance().CreateSuccessSingleResponse(pageData);
        }

        public async Task<SingleResponse<HomePageData>> GetTopAnimeManga(int skip, int take)
        {
            using var reader = await _unitOfWork.HomeRepository.GetTopAnimeManga(skip, take);
            var top7Data = new HomePageData();

            do
            {
                // Lê a primeira linha para pegar o ListType
                var hasRow = await reader.ReadAsync();
                if (!hasRow) continue;

                var listType = reader["ListType"].ToString();

                // Mapear todo o result set sem percorrer cada item
                if (listType.Contains("Anime"))
                {
                    var list = await reader.MapToListAsync<MediaCatalog>();

                    // Direciona a lista correta usando switch
                    switch (listType)
                    {
                        case "AnimeByRank": top7Data.TopAnimeByRank = list; break;
                        case "AnimeByMembers": top7Data.TopAnimeByMembers = list; break;
                        case "AnimeByFavorites": top7Data.TopAnimeByFavorites = list; break;
                        case "AnimeLastAdded": top7Data.LatestAnimes = list; break;
                    }
                }
                else if (listType.Contains("Manga"))
                {
                    var list = await reader.MapToListAsync<MediaCatalog>();

                    switch (listType)
                    {
                        case "MangaByRank": top7Data.TopMangaByRank = list; break;
                        case "MangaByMembers": top7Data.TopMangaByMembers = list; break;
                        case "MangaByFavorites": top7Data.TopMangaByFavorites = list; break;
                    }
                }

            } while (await reader.NextResultAsync());

            return ResponseFactory.CreateInstance().CreateSuccessSingleResponse(top7Data);
        }

    }
}
