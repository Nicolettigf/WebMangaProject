using BusinessLogicalLayer.Apis.KitsuApi;
using BusinessLogicalLayer.Interfaces;
using DataAccessLayer.UnitOfWork;
using Entities;
using Entities.AnimeS;
using Newtonsoft.Json;
using System.Globalization;
namespace BusinessLogicalLayer.Apis.KitsuApi
{
    public class KitsuApi : IKitsuApi
    {
        private readonly Uri baseAddress = new("https://kitsu.io/api/edge/");
        private readonly int LoteTamanho = 20; // limite padrão do Kitsu
        private readonly IUnitOfWork _unitOfWork;

        public KitsuApi(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<RootKitsu> BuscarPagina<T>(string endpoint, int offset = 0, int limit = 20) where T : class
        {
            using var httpClient = new HttpClient { BaseAddress = baseAddress };

            // Monta a query no padrão do Kitsu
            var response = await httpClient.GetAsync($"{endpoint}?page[limit]={limit}&page[offset]={offset}");
            var jsonString = await response.Content.ReadAsStringAsync();

            if (string.IsNullOrWhiteSpace(jsonString) || !response.IsSuccessStatusCode)
                return null;

            foreach (var item in JsonConvert.DeserializeObject<RootKitsu>(jsonString).data)
            {
                // Pega apenas animes que estão no ar, e passa para memória
                var dbAnimes = _unitOfWork.Query<Anime>()
                                .Where(a => a.Airing == true) // filtro simples traduzível para SQL
                                .AsEnumerable()               // agora passa para memória
                                .Where(a => item.IsMatch(a.Title)) // compara usando seu método
                                .OrderBy(a => a.Title)
                                .ToList();

                foreach (var match in dbAnimes)
                {
                    // Aqui você pode fazer o que quiser com os matches
                    Console.WriteLine($"Match encontrado: {match.TitleEnglish}");
                }
            }

            return null;
        }

        public async Task BuscarECompararAnimePorIds(int maxId)
        {
            using var httpClient = new HttpClient();

            var dbAnimes = _unitOfWork.Query<Anime>().AsEnumerable().ToList();

            //var dbAnimes = _unitOfWork.Query<Anime>().Select(a => new{ a.Id, a.Title,a.From,a.Episodes,a.Type}).AsEnumerable().ToList();

            const string TYPE = "Anime"; // pode trocar para "Manga" se mudar a rota
            const string API_NAME = "Kitsu";

            int total = 0, um = 0, dois = 0, tres = 0, resto = 0, semMatch = 0;

            const int batchSize = 100;

            for (int startId = 0; startId <= maxId; startId += batchSize)
            {
                var statsToInsert = new List<ApiReInsertStats>();

                var tasks = Enumerable.Range(startId, batchSize)
                    .Select(async id =>
                    {
                        try
                        {
                            var url = $"https://kitsu.io/api/edge/anime/{id}";
                            var response = await httpClient.GetStringAsync(url);
                            return JsonConvert.DeserializeObject<RootKitsuUnitario>(response)?.data;
                        }
                        catch
                        {
                            return null;
                        }
                    });

                var kitsuAnimes = await Task.WhenAll(tasks);

                foreach (var kitsuAnime in kitsuAnimes.Where(x => x != null))
                {
                    total++;

                    var matches = dbAnimes
                        .Where(a => kitsuAnime.IsMatch(a.Title))
                        .OrderBy(a => a.Title)
                        .ToList();

                    switch (matches.Count)
                    {
                        case 0:
                            semMatch++;
                            statsToInsert.Add(new ApiReInsertStats
                            {
                                ApiName = API_NAME,
                                IdFromApi = int.TryParse(kitsuAnime.id, out var idParsed) ? idParsed : 0,
                                Type = TYPE,
                                Erro = matches.Count.ToString()
                            });
                            break;

                        case 1:
                            um++;
                            break; // não insere porque deu certo

                        case 2:
                            dois++;
                            statsToInsert.Add(new ApiReInsertStats
                            {
                                ApiName = API_NAME,
                                IdFromApi = int.TryParse(kitsuAnime.id, out var idParsed2) ? idParsed2 : 0,
                                Type = TYPE,
                                Erro = matches.Count.ToString()
                            });
                            break;

                        case 3:
                            tres++;
                            statsToInsert.Add(new ApiReInsertStats
                            {
                                ApiName = API_NAME,
                                IdFromApi = int.TryParse(kitsuAnime.id, out var idParsed3) ? idParsed3 : 0,
                                Type = TYPE,
                                Erro = matches.Count.ToString()
                            });
                            break;

                        default:
                            resto++;
                            statsToInsert.Add(new ApiReInsertStats
                            {
                                ApiName = API_NAME,
                                IdFromApi = int.TryParse(kitsuAnime.id, out var idParsedN) ? idParsedN : 0,
                                Type = TYPE,
                                Erro = matches.Count.ToString()
                            });
                            break;
                    }
                }

                // Salva no banco a cada batch
                if (statsToInsert.Any())
                {
                    await _unitOfWork.ApiReInsertStatRepository.InserRange(statsToInsert);
                    await _unitOfWork.Commit();
                }

                Console.WriteLine($"\n--- Progresso até agora ---");
                Console.WriteLine($"Consumidos da API: {total}");
                Console.WriteLine($"1 match (OK): {um}");
                Console.WriteLine($"2 matches: {dois}");
                Console.WriteLine($"3 matches: {tres}");
                Console.WriteLine($"4+ matches: {resto}");
                Console.WriteLine($"Sem match: {semMatch}");
                Console.WriteLine("---------------------------\n");
            }

            Console.WriteLine("\n=== Resumo Final ===");
            Console.WriteLine($"Consumidos da API: {total}");
            Console.WriteLine($"1 match (OK): {um}");
            Console.WriteLine($"2 matches: {dois}");
            Console.WriteLine($"3 matches: {tres}");
            Console.WriteLine($"4+ matches: {resto}");
            Console.WriteLine($"Sem match: {semMatch}");
            Console.WriteLine("====================");
        }



    }
}