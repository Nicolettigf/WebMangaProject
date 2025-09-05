using DataAccessLayer.Interfaces.IAnimeInterfaces;
using Entities.AnimeS;
using Microsoft.EntityFrameworkCore;
using Shared.Models;
using Shared.Responses;
using static Entities.Common.MediaBase;

namespace DataAccessLayer.Implementations
{
    public class AnimeDAL : IAnimeDAL
    {
        private readonly MangaProjectDbContext _db;
        public AnimeDAL(MangaProjectDbContext db)
        {
            this._db = db;
        }

        public async Task<Response> Insert(Anime Anime)
        {
            var genreCache = await _db.Genre.ToDictionaryAsync(g => g.MalId);
            try
            {
                var cate = new List<Genre>();
                if (Anime.Genres != null)
                {
                    foreach (var item in Anime.Genres)
                    {
                        if (item.MalId.HasValue && genreCache.TryGetValue(item.MalId.Value, out var existingGenre))
                        {
                            cate.Add(existingGenre); // já existe no banco, usa a entidade rastreada
                        }
                        else
                        {
                            cate.Add(item);           // novo gênero
                            if (item.MalId.HasValue)
                                genreCache[item.MalId.Value] = item; // adiciona ao cache
                        }
                    }
                }
                Anime.Genres = cate;
                _db.Animes.Add(Anime);

                await _db.SaveChangesAsync();
                return ResponseFactory.CreateInstance().CreateSuccessResponse();
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedResponse(ex);
            }
        }
        public async Task<Response> InsertRange(IEnumerable<Anime> items)
        {
            try
            {
                // Carrega todos os gêneros existentes no banco para evitar múltiplas queries
                var genreCache = await _db.Genre.ToDictionaryAsync(g => g.MalId);

                foreach (var anime in items)
                {
                    var cate = new List<Genre>();

                    if (anime.Genres != null)
                    {
                        foreach (var item in anime.Genres)
                        {
                            if (item.MalId.HasValue && genreCache.TryGetValue(item.MalId.Value, out var existingGenre))
                            {
                                cate.Add(existingGenre); // já existe no banco, usa a entidade rastreada
                            }
                            else
                            {
                                cate.Add(item);           // novo gênero
                                if (item.MalId.HasValue)
                                    genreCache[item.MalId.Value] = item; // adiciona ao cache
                            }
                        }
                    }

                    anime.Genres = cate;

                    _db.Animes.Add(anime);
                }

                await _db.SaveChangesAsync();
                return ResponseFactory.CreateInstance().CreateSuccessResponse();
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedResponse(ex);
            }
        }

        public async Task<Response> Update(Anime Item)
        {
            Anime? AnimeDB = await _db.Animes.FindAsync(Item.Id);
            if (AnimeDB == null)
                return ResponseFactory.CreateInstance().CreateFailedResponseNotFoundId();
            try
            {
                _db.Animes.Update(Item);
                await _db.SaveChangesAsync();
                return ResponseFactory.CreateInstance().CreateSuccessResponse();
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedResponse(ex);
            }
        }

        public async Task<SingleResponse<Anime>> Get(int id)
        {
            try
            {
                Anime? Select = await _db.Animes.FindAsync(id);
                return ResponseFactory.CreateInstance().CreateSuccessSingleResponse<Anime>(Select);
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedSingleResponseNotFoundItem<Anime>(ex);
            }
        }

        public async Task<Response> Delete(int id)
        {
            Anime? AnimeDB = await _db.Animes.FindAsync(id);
            if (AnimeDB == null)
                return ResponseFactory.CreateInstance().CreateFailedResponseNotFoundId();
            try
            {
                _db.Animes.Remove(AnimeDB);
                await _db.SaveChangesAsync();
                return ResponseFactory.CreateInstance().CreateSuccessResponse();
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedResponse(ex);
            }
        }

        public async Task<DataResponse<Anime>> Get(int skip, int take)
        {
            try
            {
                List<Anime> anime = await _db.Animes
                    .AsNoTracking()
                    .Skip(skip)
                    .Take(take)
                    .ToListAsync();
                return ResponseFactory.CreateInstance().CreateResponseBasedOnCollectionData(anime);

            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedDataResponse<Anime>(ex);
            }
        }

        public async Task<DataResponse<Anime>> Get(string name)
        {
            try
            {
                List<Anime> anime = await _db.Animes.Where(M => M.Title.Contains(name)).ToListAsync();
                return ResponseFactory.CreateInstance().CreateResponseBasedOnCollectionData<Anime>(anime);
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedDataResponse<Anime>(ex);
            }
        }

        public async Task<SingleResponse<Anime>> GetComplete(int ID)
        {
            try
            {
                Anime? Select = _db.Animes.Include(d => d.Demographics).Include(c => c.GenreItems).Include(c => c.Comentaries).ThenInclude(u => u.User).Include(r => r.MediaRatingFrequency).FirstOrDefault(m => m.Id == ID);
                return ResponseFactory.CreateInstance().CreateSuccessSingleResponse<Anime>(Select);
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedSingleResponse<Anime>(ex);
            }
        }

        public async Task<int> GetLastIndex()
        {
            try
            {
                Anime? a = _db.Animes.OrderBy(c => c.Id).AsNoTracking().LastOrDefault();
                return a.Id;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<int> GetLastIndexCategory()
        {
            try
            {
                Genre? a = _db.Genre.OrderBy(c => c.Id).LastOrDefault();
                return a.Id;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<DataResponse<Anime>> GetByCategory(int ID)
        {
            try
            {
                Genre? Select = _db.Genre.Include(c => c.AnimeId).FirstOrDefault(m => m.Id == ID);
                return ResponseFactory.CreateInstance().CreateResponseBasedOnCollectionData(Select.AnimesID.ToList());
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedDataResponse<Anime>(ex);
            }
        }



        public async Task<DataResponse<int>> GetMissingMalIds()
        {
            // Pega todos os MalIds já salvos no banco
            var existingIds = await _db.Animes
                .Select(a => a.MalId)
                .ToListAsync();

            // Garante que não tenha null
            // existingIds = existingIds.Where(id => id.HasValue).Select(id => id.Value).ToList();

            // Descobre o maior MalId já salvo no banco
            int maxMalId = existingIds.Count > 0 ? existingIds.Max() : 0;

            // 🔹 Gera a lista de MalIds esperados (de 1 até o maior encontrado no banco ou até um limite pré-definido)
            var expectedIds = Enumerable.Range(1, maxMalId).ToList();

            // Faz a diferença entre o esperado e o existente
            var missingIds = expectedIds.Except(existingIds).ToList();

            return ResponseFactory.CreateInstance().CreateResponseBasedOnCollectionData(missingIds);
        }

        public async Task<DataResponse<MediaCatalog>> GetHome(int skip, int take)
        {
            throw new NotImplementedException();
        }

        public async Task<DataResponse<MediaCatalog>> GetByFavorites(int skip, int take)
        {
            try
            {

                List<MediaCatalog> animes = await _db.Animes
                    .OrderByDescending(m => m.Favorites)
                    .AsNoTracking()
                    .Skip(skip)
                    .Take(take)
                    .Select(MediaCatalog.Projection)
                    .ToListAsync();

                return ResponseFactory.CreateInstance().CreateResponseBasedOnCollectionData(animes);
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedDataResponse<MediaCatalog>(ex);
            }
        }
        public async Task<DataResponse<MediaCatalog>> GetByRating(int skip, int take)
        {
            try
            {
                List<MediaCatalog> animes = await _db.Animes.Where(w => w.Score != null && Convert.ToInt32(w.Score) != 0)
                    .OrderByDescending(m => m.Score)
                    .AsNoTracking()
                    .Skip(skip)
                    .Take(take)
                    .Select(MediaCatalog.Projection)
                    .ToListAsync();

                return ResponseFactory.CreateInstance().CreateResponseBasedOnCollectionData(animes);

            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedDataResponse<MediaCatalog>(ex);
            }
        }
        public async Task<DataResponse<MediaCatalog>> GetByUserCount(int skip, int take)
        {
            try
            {
                List<MediaCatalog> Animes = await _db.Animes
                    .AsNoTracking()
                    .OrderByDescending(m => m.Members)
                    .Skip(skip)
                    .Take(take)
                    .Select(MediaCatalog.Projection)
                    .ToListAsync();
                return ResponseFactory.CreateInstance().CreateResponseBasedOnCollectionData(Animes);

            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedDataResponse<MediaCatalog>(ex);
            }
        }
        public async Task<DataResponse<MediaCatalog>> GetByPopularity(int skip, int take)
        {
            try
            {
                List<MediaCatalog> Animes = await _db.Animes.Where(w => w.Popularity != 0)
                    .OrderBy(m => m.Popularity)
                    .AsNoTracking()
                    .Skip(skip)
                    .Take(take)
                    .Select(MediaCatalog.Projection)
                    .ToListAsync();
                return ResponseFactory.CreateInstance().CreateResponseBasedOnCollectionData(Animes);

            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedDataResponse<MediaCatalog>(ex);
            }
        }
        public async Task<DataResponse<MediaCatalog>> GetByCatalog(int skip, int take, string catalog)
        {
            try
            {
                IQueryable<Anime> query = _db.Animes.AsNoTracking();

                // Define a ordenação com base no catalog
                switch (catalog?.ToLower())
                {
                    case "favorites":
                        query = query.OrderByDescending(a => a.Favorites);
                        break;

                    case "score":
                        query = query.Where(a => a.Score != null && Convert.ToInt32(a.Score) != 0)
                                     .OrderByDescending(a => a.Score);
                        break;

                    case "usercount":
                        query = query.OrderByDescending(a => a.Members);
                        break;

                    case "popularity":
                        query = query.Where(a => a.Popularity != 0)
                                     .OrderBy(a => a.Popularity);
                        break;

                    case "scoredby":
                        query = query.Where(a => a.ScoredBy != null)
                                     .OrderByDescending(a => a.ScoredBy);
                        break;

                    case "rank":
                        query = query.Where(a => a.Rank != null && Convert.ToInt32(a.Rank) != 0)
                                     .OrderByDescending(a => a.Rank);
                        break;

                    default:
                        query = query.OrderBy(a => a.Title); // ordem padrão
                        break;
                }

                // Aplica paginação e projeção
                List<MediaCatalog> animes = await query
                    .Skip(skip)
                    .Take(take)
                    .Select(MediaCatalog.Projection)
                    .ToListAsync();

                return ResponseFactory.CreateInstance()
                    .CreateResponseBasedOnCollectionData(animes);
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance()
                    .CreateFailedDataResponse<MediaCatalog>(ex);
            }
        }

    }
}
