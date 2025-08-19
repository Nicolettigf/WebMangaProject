using BusinessLogicalLayer.Interfaces.IMangaInterfaces;
using Entities.MangaS;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace BusinessLogicalLayer.ApiConsumer.MangaApi
{
    public class MangaApi : IMangaApi
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly Uri baseAddress = new Uri("https://api.jikan.moe/v4/");
        private readonly int LimiteManga = 64595;
        private readonly int LoteTamanho = 25; // Máximo permitido pelo Jikan

        public MangaApi(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public async Task Consume()
        {
            int lastPage;
            using (var scope = _scopeFactory.CreateScope())
            {
                var mangaService = scope.ServiceProvider.GetRequiredService<IMangaService>();
                lastPage = await mangaService.GetLastIndex(); // pode ser última página salva
            }

            const int pageSize = 25;
            int totalPages = (int)Math.Ceiling(LimiteManga / (double)pageSize);

            Console.WriteLine($"📚 Começando da página {lastPage + 1} até {totalPages}...");

            using (var httpClient = new HttpClient { BaseAddress = baseAddress })
            {
                for (int page = lastPage + 1; page <= totalPages; page++)
                {
                    var dtos = await BuscarPagina(httpClient, page, pageSize);

                    if (dtos.Count > 0)
                    {
                        using (var scope = _scopeFactory.CreateScope())
                        {
                            var mangaService = scope.ServiceProvider.GetRequiredService<IMangaService>();

                            var mangaEntities = new List<Manga>();

                            foreach (var dto in dtos)
                            {
                                var manga = MangaConverter.ConvertDTOToManga(dto);
                                if (!string.IsNullOrWhiteSpace(manga.Synopsis))
                                    mangaEntities.Add(manga);
                            }

                            await mangaService.InsertRange(mangaEntities);
                        }
                    }

                    Console.WriteLine($"✅ Página {page} processada ({dtos.Count} mangas)");
                    await Task.Delay(500);
                }
            }

            Console.WriteLine("🏁 Finalizado!");
        }

        private async Task<List<MediaDto>> BuscarPagina(HttpClient httpClient, int page, int limit)
        {
            try
            {
                var response = await httpClient.GetAsync($"manga?page={page}&limit={limit}");
                var jsonString = await response.Content.ReadAsStringAsync();

                if (string.IsNullOrWhiteSpace(jsonString) || jsonString.Contains("errors"))
                    return new List<MediaDto>();

                var dto = JsonConvert.DeserializeObject<Root>(jsonString);
                return dto?.data ?? new List<MediaDto>();
            }
            catch
            {
                return new List<MediaDto>();
            }
        }

        public async Task ConsumeMissingMangas()
        {
            using var scope = _scopeFactory.CreateScope();
            var mangaService = scope.ServiceProvider.GetRequiredService<IMangaService>();

            var missingMalIds = await mangaService.GetMissingMalIds();

            Console.WriteLine($"📡 {missingMalIds.Data.Count} MAL IDs faltando. Iniciando consumo unitário...");

            using (var httpClient = new HttpClient { BaseAddress = baseAddress })
            {
                foreach (var malId in missingMalIds.Data)
                {
                    try
                    {
                        var mangaDto = await BuscarPorId(httpClient, malId);

                        if (mangaDto != null)
                        {
                            var manga = MangaConverter.ConvertDTOToManga(mangaDto);
                            await mangaService.Insert(manga);

                            Console.WriteLine($"✅ Manga {malId} inserido.");
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

                    await Task.Delay(1500); // respeitar rate limit
                }
            }

            Console.WriteLine("🏁 Finalizado consumo unitário!");
        }

        private async Task<MediaDto> BuscarPorId(HttpClient httpClient, int malId)
        {
            try
            {
                var response = await httpClient.GetAsync($"manga/{malId}");
                var jsonString = await response.Content.ReadAsStringAsync();

                if (jsonString.Contains("errors") || jsonString.Contains("BadResponseException"))
                    return null;

                var dto = JsonConvert.DeserializeObject<RootSingle>(jsonString);
                return dto?.data;
            }
            catch
            {
                return null;
            }
        }
    }
}
