using BusinessLogicalLayer.ApiConsumer.Converter;
using BusinessLogicalLayer.Implementations;
using BusinessLogicalLayer.Interfaces;
using BusinessLogicalLayer.Interfaces.IAnimeInterfaces;
using BusinessLogicalLayer.Interfaces.IMangaInterfaces;
using DataAccessLayer;
using DataAccessLayer.Interfaces.IMangaInterfaces;
using Entities.AnimeS;
using Entities.MangaS;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Net.Http;

namespace BusinessLogicalLayer.ApiConsumer.MangaApi
{
    public class JikanApi : IJikanApi
    {
        private readonly MangaProjectDbContext _db;
        private readonly IMangaService _mangaService;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly Uri baseAddress = new Uri("https://api.jikan.moe/v4/");
        private readonly int LimiteManga = 155820;
        private readonly int LimiteAnime = 62240;
        private readonly int LoteTamanho = 25; // Máximo permitido pelo Jikan

        public JikanApi(IMangaService mangaService, MangaProjectDbContext db, IServiceScopeFactory scopeFactory)
        {
            _mangaService = mangaService;
            _db = db;
            _scopeFactory = scopeFactory;
        }

        public async Task ConsumeManga()
        {
            int lastPage;
            using (var scope = _scopeFactory.CreateScope())
            {
                var mangaService = scope.ServiceProvider.GetRequiredService<IMangaService>();
                lastPage = await mangaService.GetLastIndex(); // pode ser última página salva
            }

            int totalPages = (int)Math.Ceiling(LimiteManga / (double)LoteTamanho);

            Console.WriteLine($"📚 Começando da página {lastPage + 1} até {totalPages}...");

            using (var httpClient = new HttpClient { BaseAddress = baseAddress })
            {
                for (int page = lastPage + 1; page <= totalPages; page++)
                {
                    var dtos = await BuscarPagina<Root>(httpClient, "manga", page, LoteTamanho);

                    if (dtos.Count > 0)
                    {
                        await _mangaService.InsertRange(mangaEntities);
                    }

                    Console.WriteLine($"✅ Página {page} processada ({dtos.Count} mangas)");
                    await Task.Delay(500);
                }
            }

            Console.WriteLine("🏁 Finalizado!");
        }
        public async Task ConsumeAnime()
        {
            int lastPage;
            using (var scope = _scopeFactory.CreateScope())
            {
                var animeService = scope.ServiceProvider.GetRequiredService<IAnimeService>();
                lastPage = await animeService.GetLastIndex();
            }

            int totalPages = (int)Math.Ceiling(LimiteAnime / (double)LoteTamanho);

            Console.WriteLine($"📡 Começando da página {lastPage + 1} até {totalPages}...");
            using (var httpClient = new HttpClient { BaseAddress = baseAddress })
            {
                for (int page = lastPage + 1; page <= totalPages; page++)
                {
                    var animes = await BuscarPagina<RootAniPage>(httpClient, "anime", page, LoteTamanho);

                    if (animes.Count > 0)
                    {
                        using (var scope = _scopeFactory.CreateScope())
                        {
                            var animeService = scope.ServiceProvider.GetRequiredService<IAnimeService>();
                            var animeEntities = new List<Anime>();

                            foreach (var animeDto in animes)
                            {
                                var anime = AnimeConverter.ConvertDTOToAnime(animeDto);
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
        public async Task ConsumeMissingAnime()
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
                        var animeDto = await BuscarPorId(httpClient, "anime", malId);

                        if (animeDto != null)
                        {
                            var anime = AnimeConverter.ConvertDTOToAnime(animeDto);
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
                        var mangaDto = await BuscarPorId(httpClient, "manga", malId);

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
        private async Task<List<MediaDto>> BuscarPagina<T>
        (HttpClient httpClient, string endpoint, int page, int limit) where T : class
        {
            try
            {
                var response = await httpClient.GetAsync($"{endpoint}?page={page}&limit={limit}");
                var jsonString = await response.Content.ReadAsStringAsync();

                if (string.IsNullOrWhiteSpace(jsonString) ||
                    jsonString.Contains("errors") ||
                    jsonString.Contains("BadResponseException"))
                    return new List<MediaDto>();

                var dto = JsonConvert.DeserializeObject<T>(jsonString);

                // procura a prop "data" no Root recebido
                var dataProp = typeof(T).GetProperty("data");
                var value = dataProp?.GetValue(dto) as List<MediaDto>;
                return value ?? new List<MediaDto>();
            }
            catch
            {
                return new List<MediaDto>();
            }
        }

        private async Task<MediaDto?> BuscarPorId(HttpClient httpClient, string endpoint, int malId)
        {
            try
            {
                var response = await httpClient.GetAsync($"{endpoint}/{malId}");
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

        public async Task ConsumeGenre()
        {
            await PopularGenerosAsync("anime");
            await PopularGenerosAsync("manga");
        }
        private async Task PopularGenerosAsync(string type)
        {
            using var httpClient = new HttpClient { BaseAddress = baseAddress };

            var response = await httpClient.GetAsync($"genres/{type}");
            if (!response.IsSuccessStatusCode) return;

            var jsonString = await response.Content.ReadAsStringAsync();
            var genresRoot = JsonConvert.DeserializeObject<RootCate>(jsonString);

            if (genresRoot?.data == null) return;

            foreach (var datum in genresRoot.data)
            {
                var exists = await _db.Categories.AnyAsync(c => c.MalId == datum.mal_id);
                if (exists) continue;

                var genre = new Entities.MediaBase.Genre
                {
                    Id = datum.mal_id,
                    MalId = datum.mal_id,
                    Name = datum.name,
                    Count = datum.count
                };

                await _mangaService.InsertCategory(genre);
            }
        }

    }
}
