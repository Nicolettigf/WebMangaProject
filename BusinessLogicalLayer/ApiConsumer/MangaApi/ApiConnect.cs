using BusinessLogicalLayer.Interfaces.IMangaInterfaces;
using Entities.MangaS;
using Newtonsoft.Json;
using Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLogicalLayer.ApiConsumer.MangaApi
{
    public class ApiConnect : IApiConnect
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly Uri baseAddress = new Uri("https://kitsu.io/api/edge/");
        private readonly int LimiteManga = 64595;
        private readonly int LoteTamanho = 50; // quantos mangas pegar por vez

        public ApiConnect(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public async Task Consume()
        {
            int last;
            using (var scope = _scopeFactory.CreateScope())
            {
                var mangaService = scope.ServiceProvider.GetRequiredService<IMangaService>();
                last = await mangaService.GetLastIndex();
            }

            if (last >= LimiteManga)
                return;

            Console.WriteLine($"📚 Começando do ID {last + 1} até {LimiteManga}...");

            using (var httpClient = new HttpClient { BaseAddress = baseAddress })
            {
                for (int i = last + 1; i <= LimiteManga; i += LoteTamanho)
                {
                    var tasks = new List<Task<Manga?>>();

                    // cria tasks para buscar mangas em paralelo
                    for (int id = i; id < i + LoteTamanho && id <= LimiteManga; id++)
                    {
                        tasks.Add(BuscarManga(httpClient, id));
                    }

                    var resultados = await Task.WhenAll(tasks);
                    var mangasValidos = resultados.Where(m => m != null).ToList();

                    if (mangasValidos.Count > 0)
                    {
                        using (var scope = _scopeFactory.CreateScope())
                        {
                            var mangaService = scope.ServiceProvider.GetRequiredService<IMangaService>();
                            foreach (var manga in mangasValidos)
                            {
                                if (string.IsNullOrWhiteSpace(manga.PosterImageLink))
                                    continue; // pula esse manga e vai pro próximo

                                await mangaService.Insert(manga!);
                            }
                        }
                    }

                    Console.WriteLine($"✅ Processados até ID {i + LoteTamanho - 1}");
                }
            }

            Console.WriteLine("🏁 Finalizado!");
        }

        private async Task<Manga?> BuscarManga(HttpClient httpClient, int id)
        {
            try
            {
                var response = await httpClient.GetAsync($"manga/{id}");
                var jsonString = await response.Content.ReadAsStringAsync();

                if (jsonString.Contains("errors"))
                    return null;

                var dto = JsonConvert.DeserializeObject<Root>(jsonString);
                if (dto == null)
                    return null;

                var manga = ConverterToCategory.ConvertDTOToManga(dto);
                manga.Genres = await CategoryToMangaApi.MangaCategory(Convert.ToInt32(manga.Id));

                // só salva se tiver sinopse
                if (string.IsNullOrWhiteSpace(manga.Synopsis))
                    return null;

                return manga;
            }
            catch
            {
                return null;
            }
        }
    }
}
