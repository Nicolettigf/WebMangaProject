using BusinessLogicalLayer.Apis.KitsuApi;
using BusinessLogicalLayer.Interfaces;
using DataAccessLayer.UnitOfWork;
using Entities;
using Entities.AnimeS;
using Entities.MangaS;
using Newtonsoft.Json;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Globalization;
namespace BusinessLogicalLayer.Apis.KitsuApi
{
    public class KitsuApi : IKitsuApi
    {
        private readonly Uri baseAddress = new("https://kitsu.io/api/edge/");
        private readonly string ApiName = "Kitsu";
        private readonly IUnitOfWork _unitOfWork;
        private int counter = 0;
        const int batchSize = 100;

        public KitsuApi(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task BuscarECompararPorIds<T>() where T : MediaBase
        {
            counter = 0;

            using var httpClient = new HttpClient();
            string endpoint = typeof(T) == typeof(Anime) ? "anime" : "manga";

            var dbItemsQuery = _unitOfWork.Query<T>().Where(x => x.IdKitsu == null).ToList();

            // Pega o último ID consumido do tipo T
            var stats = _unitOfWork.ApiConsumeRepository.GetStats(ApiName).Result;
            var startId = typeof(T) == typeof(Anime) ? stats.UnitarioAnime ?? 0 : stats.UnitarioManga ?? 0;

            // Define até onde buscar (pode vir do GetLastId<T>())
            int lastId = await ApiConsume.GetLastId<T>(ApiName);

            for (; startId <= lastId; startId += batchSize)
            {
                var statsToInsert = new List<ApiReInsertStats>();

                var tasks = Enumerable.Range(startId, batchSize).Select(async id =>
                {
                    try
                    {
                        var url = $"{baseAddress}/{endpoint}/{id}";
                        var response = await httpClient.GetStringAsync(url);

                        return JsonConvert.DeserializeObject<RootKitsuUnitario>(response)?.data;
                    }
                    catch
                    {
                        return null;
                    }
                });

                var results = await Task.WhenAll(tasks); // agora de fato executa

                foreach (var apiItem in results.Where(x => x != null))
                {
                    var matches = dbItemsQuery
                        .Where(a => apiItem.IsMatch(a.Title))
                        .OrderBy(a => a.Title)
                        .ToList();

                    if (matches.Count == 1)
                    {
                        var entidade = matches.First();

                        entidade.IdKitsu = Convert.ToInt32(apiItem.id);  // int.TryParse(apiItem.id, out var idParsed) ? idParsed : null;
                        entidade.Nsfw = apiItem.attributes?.nsfw;
                        entidade.YoutubeVideoId = apiItem.attributes?.youtubeVideoId;
                        entidade.EpisodeLength = apiItem.attributes?.episodeLength;
                        entidade.PosterImageLarge = apiItem.attributes?.posterImage?.large;
                        entidade.CoverImageLarge = apiItem.attributes?.coverImage?.large;

                        // Cria MediaRatingFrequency
                        entidade.MediaRatingFrequency = apiItem.attributes?.ratingFrequencies?.ColocarDados(
                            animeId: entidade is Anime a ? a.Id : null,
                            anime: entidade as Anime,
                            mangaId: entidade is Manga m ? m.Id : null,
                            manga: entidade as Manga
                        );

                        dbItemsQuery.Remove(entidade);

                        continue;
                    }

                    statsToInsert.Add(new ApiReInsertStats
                    {
                        ApiName = ApiName,
                        IdFromApi = int.TryParse(apiItem.id, out var idParsed) ? idParsed : 0,
                        Type = typeof(T).Name,
                        Erro = matches.Count.ToString()
                    });
                }

                // Salva no banco a cada batch
                if (statsToInsert.Any())
                {
                    await _unitOfWork.ApiReInsertStatRepository.InserRange(statsToInsert);
                }
                await _unitOfWork.Commit();
                await SalvarConsumoApi<T>(startId);
            }
        }


        private async Task SalvarConsumoApi<T>(int valor) where T : MediaBase
        {
            counter++;

            if (counter % 25 == 0) // salva a cada 25
            {
                await _unitOfWork.ApiConsumeRepository.UpdateConsumeStats<T>(ConsumoTipo.Unitario, ApiName, valor);
                Console.WriteLine($"🔄 Stats (unitário) atualizados após {counter} registros.");
            }
        }

        private async Task<int> GetLastId<T>()
        {
            var url = typeof(T) == typeof(Anime) ? "https://kitsu.io/api/edge/anime?page[limit]=1&page[offset]=0&sort=-id" :
                                                   "https://kitsu.io/api/edge/manga?page[limit]=1&page[offset]=0&sort=-id";

            try
            {
                using var httpClient = new HttpClient();
                var response = await httpClient.GetAsync(url);
                var jsonString = await response.Content.ReadAsStringAsync();

                var dto = JsonConvert.DeserializeObject<RootKitsu>(jsonString);
                return dto.data[0].id == null ? 0 : 100000;
            }
            catch
            {
                return 0;
            }
        }
    }
}