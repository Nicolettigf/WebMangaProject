using BusinessLogicalLayer.Interfaces.IMangaInterfaces;
using DataAccessLayer;
using DataAccessLayer.Interfaces.IMangaInterfaces;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using static Entities.MediaBase;

namespace BusinessLogicalLayer.ApiConsumer.CategoryApi
{
    public class CategoryApiConnect : ICategoryApiConnect
    {
        private readonly MangaProjectDbContext _db;
        private readonly IMangaService _mangaService;
        private readonly IMangaDAL _mangaDAL;
        private readonly Uri _baseAddress = new("https://api.jikan.moe/v4/");

        public CategoryApiConnect(IMangaService mangaService, IMangaDAL mangaDAL, MangaProjectDbContext db)
        {
            _mangaService = mangaService;
            _mangaDAL = mangaDAL;
            _db = db;
        }

        public async Task CovertiCatego()
        {
            await PopularGenerosAsync("anime");
            await PopularGenerosAsync("manga");
        }

        private async Task PopularGenerosAsync(string type)
        {
            using var httpClient = new HttpClient { BaseAddress = _baseAddress };

            var response = await httpClient.GetAsync($"genres/{type}");
            if (!response.IsSuccessStatusCode) return;

            var jsonString = await response.Content.ReadAsStringAsync();
            var genresRoot = JsonConvert.DeserializeObject<RootCate>(jsonString);

            if (genresRoot?.data == null) return;

            foreach (var datum in genresRoot.data)
            {
                var exists = await _db.Categories.AnyAsync(c => c.MalId == datum.mal_id);
                if (exists) continue;

                var genre = new Genre
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

    // DTOs
    public class Datum
    {
        public int mal_id { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public int count { get; set; }
    }

    public class RootCate
    {
        public List<Datum> data { get; set; }
    }
}
