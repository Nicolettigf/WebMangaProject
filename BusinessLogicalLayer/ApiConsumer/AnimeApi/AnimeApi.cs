using BusinessLogicalLayer.ApiConsumer.AnimeApi;
using BusinessLogicalLayer.ApiConsumer.CategoryToItemApi;
using BusinessLogicalLayer.Interfaces.IAnimeInterfaces;
using Entities.AnimeS;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Shared.Responses;
using System.Collections.Concurrent;

namespace BusinessLogicalLayer.ApiConsumer.NovaPasta
{
    public class AnimeApi : IAnimeApiConnect
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly Uri baseAddress = new Uri("https://kitsu.io/api/edge/");
        private readonly int LimiteAnimes = 50000;
        private readonly int LoteTamanho = 50; // quantidade de animes processados em paralelo

        public AnimeApi(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public async Task ConsumeAnime()
        {
            int last;
            using (var scope = _scopeFactory.CreateScope())
            {
                var animeService = scope.ServiceProvider.GetRequiredService<IAnimeService>();
                last = await animeService.GetLastIndex();
            }

            if (last >= LimiteAnimes)
                return;

            Console.WriteLine($"📡 Começando do ID {last + 1} até {LimiteAnimes}...");

            using (var httpClient = new HttpClient { BaseAddress = baseAddress })
            {
                for (int i = last + 1; i <= LimiteAnimes; i += LoteTamanho)
                {
                    var tasks = new List<Task<Anime?>>();

                    // cria as tasks para pegar animes em paralelo
                    for (int id = i; id < i + LoteTamanho && id <= LimiteAnimes; id++)
                    {
                        tasks.Add(BuscarAnime(httpClient, id));
                    }

                    var resultados = await Task.WhenAll(tasks);
                    var animesValidos = resultados.Where(a => a != null).ToList();

                    if (animesValidos.Count > 0)
                    {
                        // insere o lote no banco
                        using (var scope = _scopeFactory.CreateScope())
                        {
                            var animeService = scope.ServiceProvider.GetRequiredService<IAnimeService>();
                            foreach (var anime in animesValidos)
                            {
                                await animeService.Insert(anime!);
                            }
                        }
                    }

                    Console.WriteLine($"✅ Processados até ID {i + LoteTamanho - 1}");
                }
            }

            Console.WriteLine("🏁 Finalizado!");
        }

        private async Task<Anime?> BuscarAnime(HttpClient httpClient, int id)
        {
            try
            {
                var response = await httpClient.GetAsync($"anime/{id}");
                var jsonString = await response.Content.ReadAsStringAsync();

                if (jsonString.Contains("errors"))
                    return null;

                var dto = JsonConvert.DeserializeObject<RootANI>(jsonString);
                if (dto == null)
                    return null;

                var anime = AnimeConverter.ConvertDTOToAnime(dto);
                anime.Categories = await CategoryToAnime.AnimeCategory(Convert.ToInt32(anime.Id));
                return anime;
            }
            catch
            {
                return null; // ignora erros individuais
            }
        }
    }
}
