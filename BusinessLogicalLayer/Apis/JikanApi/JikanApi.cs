using BusinessLogicalLayer.Interfaces;
using DataAccessLayer.Interfaces.IAnimeInterfaces;
using DataAccessLayer.Interfaces.IMangaInterfaces;
using DataAccessLayer.UnitOfWork;
using Entities;
using Entities.AnimeS;
using Entities.MangaS;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Diagnostics;

namespace BusinessLogicalLayer.Apis.JikanApi
{
    public class JikanApi : IJikanApi
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly string ApiName = "Jikan";
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly Uri baseAddress = new("https://api.jikan.moe/v4/");
        private readonly int LoteTamanho = 25; // Máximo permitido pelo Jikan
        const int maxPerSecond = 3; // limite da API
        private int counter = 0;

        public JikanApi(IServiceScopeFactory scopeFactory, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _scopeFactory = scopeFactory;
        }
        public async Task ConsumeMedia<T>() where T : MediaBase, new() 
        {
            counter = 0; // contador de registros processados

            var firstPage = typeof(T) == typeof(Anime)
                ? _unitOfWork.ApiConsumeRepository.GetStats(ApiName).Result.PagesConsumedAnime ?? 0
                : _unitOfWork.ApiConsumeRepository.GetStats(ApiName).Result.PagesConsumedManga ?? 0;

            int lastId = await ApiConsume.GetLastId<T>(ApiName);
            int lastPage = (int)Math.Ceiling(lastId / (double)LoteTamanho);

            const int maxPagesPerSecond = 3; // limite da API
            using var httpClient = new HttpClient { BaseAddress = baseAddress };

            // Processa páginas em batches de 3
            for (int batchStart = firstPage + 1; batchStart <= lastPage; batchStart += maxPagesPerSecond)
            {
                var sw = Stopwatch.StartNew();

                var pageBatch = Enumerable.Range(batchStart, Math.Min(maxPagesPerSecond, lastPage - batchStart + 1));

                var tasks = pageBatch.Select(async page =>
                {
                    var dtos = await BuscarPagina<T>(httpClient, page);

                    if (dtos.Count > 0)
                    {
                        var entities = dtos
                            .Select(dto => ConverterJikan.ConvertDTOToEntity<T>(dto))
                            .OfType<T>()
                            .ToList();

                        using var scope = _scopeFactory.CreateScope();
                        var uow = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

                        await uow.GetDAL<T>().InsertRange(entities);
                        await uow.Commit();
                    }

                    await SalvarConsumoApi<T>(ConsumoTipo.Pagina, page);
                    Console.WriteLine($"✅ Página {page} processada ({dtos.Count} {typeof(T).Name.ToLower()}s)");
                });

                await Task.WhenAll(tasks);

                // Aguarda o tempo restante para respeitar o limite de 3 páginas/segundo
                sw.Stop();

                int delay = Math.Max(0, 1000 - (int)sw.ElapsedMilliseconds);
                await Task.Delay(delay);
            }
            Console.WriteLine("🏁 Finalizado!");
        }

        public async Task ConsumeMissingMedia<T>() where T : MediaBase, new()
        {
            counter = 0;
            var ids = await _unitOfWork.ApiConsumeRepository.GetIdsFromApi<T>(ApiName, ConsumoTipo.Unitario);
            List<int> missingIds = ApiConsumeStats.GetMissingUnitarios(ids, await ApiConsume.GetLastId<T>(ApiName));
            using var httpClient = new HttpClient { BaseAddress = baseAddress };

            var sw = Stopwatch.StartNew(); // inicia o cronômetro do batch

            foreach (var batch in missingIds.Chunk(maxPerSecond))
            {
                var tasks = batch.Select(async malId =>
                {
                    try
                    {
                        using var scope = _scopeFactory.CreateScope();
                        var uow = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

                        var dto = await BuscarPorId<T>(httpClient, malId);

                        if (dto != null)
                        {
                            var entity = ConverterJikan.ConvertDTOToEntity<T>(dto);
                            await uow.GetDAL<T>().Insert(entity);
                            await uow.Commit();
                        }
                        else
                        {
                            Console.WriteLine($"⚠️ Nenhum dado retornado para MAL ID {malId}");
                        }

                        await SalvarConsumoApi<T>(ConsumoTipo.Unitario, malId);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"❌ Erro ao processar MAL ID {malId}: {ex.Message}");
                    }
                });

                await Task.WhenAll(tasks);

                sw.Stop(); // para o cronômetro
               
                // espera apenas o tempo necessário para completar 1 segundo
                await Task.Delay(Math.Max(0, 1000 - (int)sw.ElapsedMilliseconds));
            }

            Console.WriteLine($"🏁 Finalizado consumo unitário de {typeof(T).Name}s!");
        }
        public async Task ConsumeGenre<T>() where T : MediaBase
        {
            // Define o endpoint baseado no tipo da entidade
            string type = typeof(T) == typeof(Anime) ? "anime" : "manga";

            using var httpClient = new HttpClient { BaseAddress = baseAddress };

            var response = await httpClient.GetAsync($"genres/{type}");
            if (!response.IsSuccessStatusCode) return;

            var jsonString = await response.Content.ReadAsStringAsync();
            var genresRoot = JsonConvert.DeserializeObject<RootCate>(jsonString);

            if (genresRoot?.data == null) return;

            foreach (var datum in genresRoot.data)
            {
                var exists = await _unitOfWork.Query<Entities.MediaBase.Genre>().AnyAsync(c => c.Id == datum.mal_id);

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
        private async Task<MediaDtoJikan?> BuscarPorId<T>(HttpClient httpClient, int malId) where T : MediaBase
        {
            string endpoint = typeof(T) == typeof(Anime) ? "anime" : "manga";

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
        private async Task<List<MediaDtoJikan>> BuscarPagina<T>(HttpClient httpClient, int page)
    where T : MediaBase
        {
            try
            {
                // Define o endpoint baseado no tipo da entidade
                string endpoint = typeof(T) == typeof(Anime) ? "anime" : "manga";

                var response = await httpClient.GetAsync($"{endpoint}?page={page}&limit={LoteTamanho}");
                var jsonString = await response.Content.ReadAsStringAsync();

                if (string.IsNullOrWhiteSpace(jsonString) ||
                    jsonString.Contains("errors") ||
                    jsonString.Contains("BadResponseException"))
                    return new List<MediaDtoJikan>();

                // Hoje só tem um DTO, então sempre desserializa como RootPageJikan
                var dto = JsonConvert.DeserializeObject<RootPageJikan>(jsonString);

                return dto?.data ?? new List<MediaDtoJikan>();
            }
            catch
            {
                return new List<MediaDtoJikan>();
            }
        }
        private async Task SalvarConsumoApi<T>(ConsumoTipo tipo, int valor) where T : MediaBase
        {
            counter++;

            switch (tipo)
            {
                case ConsumoTipo.Unitario:
                    if (counter % 25 == 0) // salva a cada 25
                    {
                        await _unitOfWork.ApiConsumeRepository.UpdateConsumeStats<T>(ConsumoTipo.Unitario, ApiName, valor);
                        Console.WriteLine($"🔄 Stats (unitário) atualizados após {counter} registros.");
                    }
                    break;

                case ConsumoTipo.Pagina:
                    if (counter % 10 == 0) // salva a cada 10
                    {
                        await _unitOfWork.ApiConsumeRepository.UpdateConsumeStats<T>(ConsumoTipo.Pagina, ApiName, valor);
                        Console.WriteLine($"🔄 Stats (página) atualizados após {counter} registros.");
                    }
                    break;
            }
        }
    }
}
