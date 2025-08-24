using BusinessLogicalLayer.Interfaces;
using BusinessLogicalLayer.Interfaces.IMangaInterfaces;
using DataAccessLayer;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Interfaces.IAnimeInterfaces;
using DataAccessLayer.Interfaces.IMangaInterfaces;
using DataAccessLayer.UnitOfWork;
using Entities;
using Entities.AnimeS;
using Entities.MangaS;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Drawing;

namespace BusinessLogicalLayer.Apis.JikanApi
{
    public class JikanApi : IJikanApi
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly string ApiName = "Jikan";
        private readonly MangaProjectDbContext _db;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly Uri baseAddress = new("https://api.jikan.moe/v4/");
        private readonly int LimiteManga = 190000;
        private readonly int LimiteAnime = 80000;
        private readonly int LoteTamanho = 25; // Máximo permitido pelo Jikan
        private int counter = 0;

        public JikanApi(MangaProjectDbContext db, IServiceScopeFactory scopeFactory, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _db = db;
            _scopeFactory = scopeFactory;
        }

        public async Task ConsumeManga()
        {
            counter = 0; // contador de registros processados

            var lastpage = _unitOfWork.ApiConsumeRepository.GetStats(ApiName).Result.PagesConsumedManga ?? 0;

            int totalPages = (int)Math.Ceiling(LimiteManga / (double)LoteTamanho);

            using (var httpClient = new HttpClient { BaseAddress = baseAddress })
            {
                for (int page = lastpage + 1; page <= totalPages; page++)
                {
                    var dtos = await BuscarPagina<RootJikan>(httpClient, "manga", page, LoteTamanho);

                    if (dtos.Count > 0)
                    {
                        using var scope = _scopeFactory.CreateScope();
                        var mangaDal = scope.ServiceProvider.GetRequiredService<IMangaDAL>();

                        var mangaEntities = dtos
                            .Select(dto => ConverterJikan.ConvertDTOToEntity<Manga>(dto))
                            .OfType<Manga>()
                            .ToList();

                        await mangaDal.InsertRange(mangaEntities);
                    }
                    await SalvarConsumoApi<Manga>(ConsumoTipo.Pagina, page);

                    Console.WriteLine($"✅ Página {page} processada ({dtos.Count} mangas)");
                    await Task.Delay(350);
                }
            }


            Console.WriteLine("🏁 Finalizado!");
        }
        public async Task ConsumeAnime()
        {
            counter = 0; // contador de registros processados

            var lastpage = _unitOfWork.ApiConsumeRepository.GetStats(ApiName).Result.PagesConsumedAnime ?? 0;

            int totalPages = (int)Math.Ceiling(LimiteAnime / (double)LoteTamanho);

            using (var httpClient = new HttpClient { BaseAddress = baseAddress })
            {
                for (int page = lastpage + 1; page <= totalPages; page++)
                {
                    var animes = await BuscarPagina<RootAniPageJikan>(httpClient, "anime", page, LoteTamanho);

                    if (animes.Count > 0)
                    {
                        using var scope = _scopeFactory.CreateScope();
                        var animeDal = scope.ServiceProvider.GetRequiredService<IAnimeDAL>();

                        var animeEntities = animes.Select(dto =>
                        ConverterJikan.ConvertDTOToEntity<Anime>(dto)).OfType<Anime>().ToList();

                        await animeDal.InsertRange(animeEntities);
                    }
                    await SalvarConsumoApi<Anime>(ConsumoTipo.Pagina, page);
                    Console.WriteLine($"✅ Página {page} processada ({animes.Count} animes)");

                    await Task.Delay(350);
                }
            }
            Console.WriteLine("🏁 Finalizado!");
        }
        public async Task ConsumeMissingAnime()
        {
            counter = 0; // contador de registros processados

            // Retorna os IDs existentes
            var ids = await _unitOfWork.ApiConsumeRepository.GetIdsFromApi<Anime>(ApiName, ConsumoTipo.Unitario);
            var missingMalIds = ApiConsumeStats.GetMissingUnitarios(ids, LimiteAnime);

            Console.WriteLine($"Iniciando consumo unitário...");

            using (var httpClient = new HttpClient { BaseAddress = baseAddress })
            {
                foreach (var malId in missingMalIds)
                {
                    try
                    {
                        var animeDto = await BuscarPorId(httpClient, "anime", malId);

                        if (animeDto != null)
                        {
                            using var scope = _scopeFactory.CreateScope();
                            var animeDal = scope.ServiceProvider.GetRequiredService<IAnimeDAL>();

                            var animeEntity = ConverterJikan.ConvertDTOToEntity<Anime>(animeDto);
                            await animeDal.Insert(animeEntity);
                        }
                        else
                        {
                            Console.WriteLine($"⚠️ Nenhum dado retornado para MAL ID {malId}");
                        }
                        await SalvarConsumoApi<Anime>(ConsumoTipo.Unitario, malId);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"❌ Erro ao processar MAL ID {malId}: {ex.Message}");
                    }

                    // Delay para respeitar o rate limit
                    await Task.Delay(350);
                }
            }

            Console.WriteLine("🏁 Finalizado consumo unitário!");
        }
        public async Task ConsumeMissingMangas()
        {
            counter = 0; // contador de registros processados

            var ids = await _unitOfWork.ApiConsumeRepository.GetIdsFromApi<Manga>(ApiName, ConsumoTipo.Unitario);
            var missingMalIds = ApiConsumeStats.GetMissingUnitarios(ids, LimiteManga);

            Console.WriteLine($"Iniciando consumo unitário...");

            using var httpClient = new HttpClient { BaseAddress = baseAddress };
            foreach (var malId in missingMalIds)
            {
                try
                {
                    var mangaDto = await BuscarPorId(httpClient, "manga", malId);

                    if (mangaDto != null)
                    {
                        var manga = ConverterJikan.ConvertDTOToEntity<Manga>(mangaDto);
                        await _unitOfWork.MangaRepository.Insert(manga);

                        Console.WriteLine($"✅ Manga {malId} inserido.");
                    }
                    else
                    {
                        Console.WriteLine($"⚠️ Nenhum dado retornado para MAL ID {malId}");
                    }

                    await SalvarConsumoApi<Manga>(ConsumoTipo.Unitario, malId);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ Erro ao processar MAL ID {malId}: {ex.Message}");
                }

                await Task.Delay(350);
            }
        }
        private async Task<List<MediaDtoJikan>> BuscarPagina<T>
        (HttpClient httpClient, string endpoint, int page, int limit) where T : class
        {
            try
            {
                var response = await httpClient.GetAsync($"{endpoint}?page={page}&limit={limit}");
                var jsonString = await response.Content.ReadAsStringAsync();

                if (string.IsNullOrWhiteSpace(jsonString) ||
                    jsonString.Contains("errors") ||
                    jsonString.Contains("BadResponseException"))
                    return new List<MediaDtoJikan>();

                var dto = JsonConvert.DeserializeObject<T>(jsonString);

                // procura a prop "data" no Root recebido
                var dataProp = typeof(T).GetProperty("data");
                var value = dataProp?.GetValue(dto) as List<MediaDtoJikan>;
                return value ?? new List<MediaDtoJikan>();
            }
            catch
            {
                return new List<MediaDtoJikan>();
            }
        }
        private async Task<MediaDtoJikan?> BuscarPorId(HttpClient httpClient, string endpoint, int malId)
        {
            try
            {
                var response = await httpClient.GetAsync($"{endpoint}/{malId}");
                var jsonString = await response.Content.ReadAsStringAsync();

                if (jsonString.Contains("errors") || jsonString.Contains("BadResponseException"))
                    return null;

                var dto = JsonConvert.DeserializeObject<RootSingleJikan>(jsonString);
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
                var exists = await  _db.Genre.AnyAsync(c => c.MalId == datum.mal_id);
                if (exists) continue;

                var genre = new MediaBase.Genre
                {
                    Id = datum.mal_id,
                    MalId = datum.mal_id,
                    Name = datum.name,
                    Count = datum.count
                };

                await _unitOfWork.MangaRepository.InsertCategory(genre);
            }
        }
        public async Task SalvarConsumoApi<T>(ConsumoTipo tipo, int valor) where T : MediaBase
        {
            counter++;

            switch (tipo)
            {
                case ConsumoTipo.Unitario:
                    if (counter % 50 == 0) // salva a cada 50
                    {
                        await _unitOfWork.ApiConsumeRepository.UpdateConsumeStats<T>(ConsumoTipo.Unitario, ApiName, valor);
                        Console.WriteLine($"🔄 Stats (unitário) atualizados após {counter} registros.");
                    }
                    break;

                case ConsumoTipo.Pagina:
                    if (counter % 20 == 0) // salva a cada 20
                    {
                        await _unitOfWork.ApiConsumeRepository.UpdateConsumeStats<T>(ConsumoTipo.Pagina, ApiName, valor);
                        Console.WriteLine($"🔄 Stats (página) atualizados após {counter} registros.");
                    }
                    break;
            }
        }

    }
}
