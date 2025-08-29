using DataAccessLayer.UnitOfWork;
using Entities;
using Entities.AnimeS;
using Newtonsoft.Json;
using Shared.Extensions;
using System.Diagnostics.Metrics;

namespace BusinessLogicalLayer.Apis
{
    public class ApiConsume
    {
        private readonly IUnitOfWork _unitOfWork;

        public ApiConsume(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async static Task<int> GetLastId<T>(string apiName)
        {
            var url = string.Empty;

            if (apiName == "Jikan")
            {
                url = typeof(T) == typeof(Anime) ? "https://api.jikan.moe/v4/anime?order_by=mal_id&sort=desc&limit=1" :
                                                   "https://api.jikan.moe/v4/manga?order_by=mal_id&sort=desc&limit=1";
            }
            else
            {
                url = typeof(T) == typeof(Anime) ? "https://kitsu.io/api/edge/anime?page[limit]=1&page[offset]=0&sort=-id" :
                                                   "https://kitsu.io/api/edge/manga?page[limit]=1&page[offset]=0&sort=-id";
            }

            try
            {
                using var httpClient = new HttpClient();
                var response = await httpClient.GetAsync(url);
                var jsonString = await response.Content.ReadAsStringAsync();

                if (apiName == "Jikan")
                {
                    var dto = JsonConvert.DeserializeObject<RootPageJikan>(jsonString);
                    return dto?.data[0].mal_id ?? 100000;
                }
                else 
                {
                    var dto = JsonConvert.DeserializeObject<RootKitsu>(jsonString);
                    return dto?.data[0].id.ToIntOrDefault() ?? 100000;
                }
            }
            catch
            {
                return 0;
            }
        }
    }
}
