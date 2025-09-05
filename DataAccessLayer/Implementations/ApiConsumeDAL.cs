using DataAccessLayer.Interfaces;
using Entities.AnimeS;
using Entities.Common;
using Entities.MangaS;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Implementations
{
    public class ApiConsumeDal : IApiConsumeDAL
    {
        private readonly DbContext _context;

        public ApiConsumeDal(DbContext context)
        {
            _context = context;
        }
        public async Task<List<int>> GetIdsFromApi<T>(string apiName, ConsumoTipo tipo) where T : MediaBase
        {
            // Pega os stats da API
            var stat = await _context.Set<ApiConsumeStats>()
                .FirstOrDefaultAsync(s => s.ApiName == apiName);

            if (stat == null)
                return new List<int>();

            // Define de onde começar a buscar baseado no tipo
            int startFrom = 0;
            switch (tipo)
            {
                case ConsumoTipo.Unitario:
                    if (typeof(T) == typeof(Anime))
                        startFrom = stat.UnitarioAnime ?? 0;
                    else if (typeof(T) == typeof(Manga))
                        startFrom = stat.UnitarioManga ?? 0;
                    break;

                case ConsumoTipo.Pagina:
                    if (typeof(T) == typeof(Anime))
                        startFrom = stat.PagesConsumedAnime ?? 0;
                    else if (typeof(T) == typeof(Manga))
                        startFrom = stat.PagesConsumedManga ?? 0;
                    break;
            }

            // Retorna os IDs existentes a partir do startFrom
            if (typeof(T) == typeof(Anime))
                return await _context.Set<Anime>()
                    .Where(a => a.MalId >= startFrom)
                    .Select(a => a.MalId)
                    .ToListAsync();

            if (typeof(T) == typeof(Manga))
                return await _context.Set<Manga>()
                    .Where(m => m.MalId >= startFrom)
                    .Select(m => m.MalId)
                    .ToListAsync();

            return new List<int>();
        }
        public async Task<ApiConsumeStats> GetStats(string api)
        {
            return await _context.Set<ApiConsumeStats>()
                .FirstOrDefaultAsync(s => s.ApiName == api);
        }

        public async Task UpdateConsumeStats<T>(ConsumoTipo tipo, string api, int valor) where T : MediaBase
        {
            var stat = await _context.Set<ApiConsumeStats>()
                .FirstOrDefaultAsync(s => s.ApiName == api);

            if (stat == null)
            {
                stat = new ApiConsumeStats { ApiName = api };
                _context.Set<ApiConsumeStats>().Add(stat);
            }

            switch (tipo)
            {
                case ConsumoTipo.Pagina:
                    if (typeof(T) == typeof(Anime))
                        stat.PagesConsumedAnime = valor;
                    else if (typeof(T) == typeof(Manga))
                        stat.PagesConsumedManga = valor;
                    break;

                case ConsumoTipo.Unitario:
                    if (typeof(T) == typeof(Anime))
                        stat.UnitarioAnime = valor;
                    else if (typeof(T) == typeof(Manga))
                        stat.UnitarioManga = valor;
                    break;
            }

            await _context.SaveChangesAsync();
        }
    }
}