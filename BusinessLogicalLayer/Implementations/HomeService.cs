using BusinessLogicalLayer.Interfaces;
using DataAccessLayer.UnitOfWork;
using Shared;
using Shared.DTOS;
using Shared.Extensions;
using Shared.Models.Anime;
using Shared.Models.Manga;
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
                    var list = await reader.MapToListAsync<AnimeCatalog>();

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
                    var list = await reader.MapToListAsync<MangaCatalog>();

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
