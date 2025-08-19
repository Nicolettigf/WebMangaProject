using BusinessLogicalLayer.ApiConsumer.AnimeApi;
using BusinessLogicalLayer.Interfaces.IAnimeInterfaces;
using Entities.AnimeS;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Shared.Responses;

namespace BusinessLogicalLayer.ApiConsumer.NovaPasta
{
    public class AnimeApi : IAnimeApiConnect
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly Uri baseAddress = new Uri("https://api.jikan.moe/v4/");

        public AnimeApi(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        // 🔹 Mantém o consumo em lote por página (já existe)
        public async Task ConsumeAnime()
        {
            int lastPage;
            using (var scope = _scopeFactory.CreateScope())
            {
                var animeService = scope.ServiceProvider.GetRequiredService<IAnimeService>();
                lastPage = await animeService.GetLastIndex();
            }

            const int pageSize = 25;
            const int totalAnimes = 50000;
            int totalPages = (int)Math.Ceiling(totalAnimes / (double)pageSize);

            Console.WriteLine($"📡 Começando da página {lastPage + 1} até {totalPages}...");
            using (var httpClient = new HttpClient { BaseAddress = baseAddress })
            {
                for (int page = lastPage + 1; page <= totalPages; page++)
                {
                    var animes = await BuscarPagina(httpClient, page, pageSize);

                    if (animes.Count > 0)
                    {
                        using (var scope = _scopeFactory.CreateScope())
                        {
                            var animeService = scope.ServiceProvider.GetRequiredService<IAnimeService>();
                            var animeEntities = new List<Anime>();

                            foreach (var animeDto in animes)
                            {
                                var anime = AnimeConverter.ConvertDTOToAnime(new RootAni { data = animeDto });
                                animeEntities.Add(anime);
                            }

                            await animeService.InsertRange(animeEntities);
                        }
                    }

                    Console.WriteLine($"✅ Página {page} processada ({animes.Count} animes)");

                    await Task.Delay(500);
                }
            }
            Console.WriteLine("🏁 Finalizado!");
        }

        // 🔹 Novo método: consumo unitário via lista de MalIds faltando no banco
        public async Task ConsumeMissingAnimes()
        {
            using var scope = _scopeFactory.CreateScope();
            var animeService = scope.ServiceProvider.GetRequiredService<IAnimeService>();

            // Método no serviço que retorna todos os MalIds faltantes
            var missingMalIds = await animeService.GetMissingMalIds();

            Console.WriteLine($"📡 {missingMalIds.Data} MAL IDs faltando. Iniciando consumo unitário...");

            using (var httpClient = new HttpClient { BaseAddress = baseAddress })
            {
                foreach (var malId in missingMalIds.Data)
                {
                    try
                    {
                        var animeDto = await BuscarPorId(httpClient, malId);

                        if (animeDto != null)
                        {
                            var anime = AnimeConverter.ConvertDTOToAnime(new RootAni { data = animeDto });
                            await animeService.Insert(anime);

                            Console.WriteLine($"✅ Anime {malId} inserido.");
                        }
                        else
                        {
                            Console.WriteLine($"⚠️ Nenhum dado retornado para MAL ID {malId}");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"❌ Erro ao processar MAL ID {malId}: {ex.Message}");
                    }

                    // Delay para respeitar o rate limit
                    await Task.Delay(1500);
                }
            }

            Console.WriteLine("🏁 Finalizado consumo unitário!");
        }

        // 🔹 Busca por página
        private async Task<List<DataAni>> BuscarPagina(HttpClient httpClient, int page, int limit)
        {
            try
            {
                var response = await httpClient.GetAsync($"anime?page={page}&limit={limit}");
                var jsonString = await response.Content.ReadAsStringAsync();

                if (jsonString.Contains("errors") || jsonString.Contains("BadResponseException"))
                    return new List<DataAni>();

                var dto = JsonConvert.DeserializeObject<RootAniPage>(jsonString);
                return dto?.data ?? new List<DataAni>();
            }
            catch
            {
                return new List<DataAni>();
            }
        }

        // 🔹 Busca unitária por MAL ID
        private async Task<DataAni?> BuscarPorId(HttpClient httpClient, int malId)
        {
            try
            {
                var response = await httpClient.GetAsync($"anime/{malId}");
                var jsonString = await response.Content.ReadAsStringAsync();

                if (jsonString.Contains("errors") || jsonString.Contains("BadResponseException"))
                    return null;

                var dto = JsonConvert.DeserializeObject<RootAni>(jsonString);
                return dto?.data;
            }
            catch
            {
                return null;
            }
        }

        // 🔹 Modelo para resposta de página
        public class RootAniPage
        {
            public List<DataAni> data { get; set; }
        }
    }
}
