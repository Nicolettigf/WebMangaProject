using BusinessLogicalLayer.ApiConsumer.AnimeApi;
using BusinessLogicalLayer.ApiConsumer.CategoryToItemApi;
using BusinessLogicalLayer.Interfaces.IAnimeInterfaces;
using Entities.AnimeS;
using Entities.Enums;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Shared.Responses;
using System.Collections.Concurrent;
using System.Net.NetworkInformation;

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

        public async Task ConsumeAnime()
        {
            int lastPage;
            using (var scope = _scopeFactory.CreateScope())
            {
                var animeService = scope.ServiceProvider.GetRequiredService<IAnimeService>();
                lastPage = await animeService.GetLastIndex(); // Pode ser último ID ou última página salva
            }

            const int pageSize = 25; // Máximo que o Jikan permite
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

                            await animeService.InsertRange(animeEntities); // chama tudo de uma vez
                        }
                    }

                    Console.WriteLine($"✅ Página {page} processada ({animes.Count} animes)");

                    // Delay para evitar rate limit
                    await Task.Delay(500);
                }
            }
            Console.WriteLine("🏁 Finalizado!");
        }

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

        // Novo modelo para o endpoint de página
        public class RootAniPage
        {
            public List<DataAni> data { get; set; }
        }
    }
}
