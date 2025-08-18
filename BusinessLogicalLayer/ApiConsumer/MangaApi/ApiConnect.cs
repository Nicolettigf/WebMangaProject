using BusinessLogicalLayer.Interfaces.IMangaInterfaces;
using Entities.MangaS;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Net.Http;

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

        private async Task<List<Datum>> BuscarPagina(HttpClient httpClient, int page, int limit)
        {
            try
            {
                var response = await httpClient.GetAsync($"manga?page={page}&limit={limit}");
                var jsonString = await response.Content.ReadAsStringAsync();

                if (string.IsNullOrWhiteSpace(jsonString) || jsonString.Contains("errors"))
                    return new List<Datum>();

                var dto = JsonConvert.DeserializeObject<Root>(jsonString);
                return dto?.data ?? new List<Datum>();
            }
            catch
            {
                return new List<Datum>();
            }
        }
    }
}
