using DataAccessLayer.Interfaces.IMangaInterfaces;
using Entities;
using Entities.AnimeS;
using Entities.MangaS;
using Microsoft.EntityFrameworkCore;
using Shared;
using Shared.Interfaces;
using Shared.Models.Manga;
using Shared.Responses;
using System;
using static Entities.MediaBase;

namespace DataAccessLayer.Implementations
{
    public class MangaDAL : IMangaDAL
    {
        private readonly MangaProjectDbContext _db;
        public MangaDAL(MangaProjectDbContext db)
        {
            this._db = db;
        }

        public async Task<Response> Insert(Manga manga)
        {
           ICollection<Genre> cate = new List<Genre>();
            try
            {

                foreach (var item in manga.Genres)
                {
                    // Tenta pegar do contexto já carregado
                    var category = _db.ChangeTracker.Entries<Genre>()
                                      .FirstOrDefault(e => e.Entity.MalId == item.MalId)?.Entity;

                    if (category != null)
                    {
                        cate.Add(category); // já está sendo rastreado
                    }
                    else
                    {
                        // Busca no banco
                        category = await _db.Genre.FirstOrDefaultAsync(c => c.MalId == item.MalId);
                        if (category != null)
                        {
                            cate.Add(category); // associa entidade existente
                        }
                        else
                        {
                            cate.Add(item); // insere novo
                        }
                    }
                }
                manga.Genres = cate;
                _db.Mangas.Add(manga);
                return ResponseFactory.CreateInstance().CreateSuccessResponse();
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedResponse(ex);
            }
        }
        public async Task<Response> Update(Manga Item)
        {
            Manga? MangaDB = await _db.Mangas.FindAsync(Item.Id);
            if (MangaDB == null)
                return ResponseFactory.CreateInstance().CreateFailedResponseNotFoundId();
            try
            {
                _db.Mangas.Update(Item);
                return ResponseFactory.CreateInstance().CreateSuccessResponse();
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedResponse(ex);
            }
        }
        public async Task<Response> Delete(int id)
        {
            Manga? MangaDB = await _db.Mangas.FindAsync(id);
            if (MangaDB == null)
                return ResponseFactory.CreateInstance().CreateFailedResponseNotFoundId();
            try
            {
                _db.Mangas.Remove(MangaDB);
                return ResponseFactory.CreateInstance().CreateSuccessResponse();
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedResponse(ex);
            }
        }
        public async Task<Response> InsertRange(IEnumerable<Manga> items)
        {
            try
            {
                // Cache local para evitar múltiplas consultas ao banco
                var genreCache = _db.Genre.ToDictionary(g => g.MalId);

                foreach (var manga in items)
                {
                    var cate = new List<Genre>();

                    foreach (var item in manga.Genres)
                    {
                        if (genreCache.TryGetValue(item.MalId ?? 0, out var existingGenre))
                        {
                            cate.Add(existingGenre);
                        }
                        else
                        {
                            cate.Add(item);
                            genreCache[item.MalId ?? 0] = item; // adiciona ao cache
                        }
                    }

                    manga.Genres = cate;
                }

                _db.Mangas.AddRange(items);
                await _db.SaveChangesAsync();
                return ResponseFactory.CreateInstance().CreateSuccessResponse();
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedResponse(ex);
            }
        }
        public async Task<DataResponse<int>> GetMissingMalIds()
        {
            // Pega todos os MalIds já salvos no banco
            var existingIds = await _db.Mangas
                .Select(a => a.MalId)
                .ToListAsync();

            // Garante que não tenha null
            //existingIds = existingIds.Where(id => id.HasValue).Select(id => id.Value).ToList();

            // Descobre o maior MalId já salvo no banco
            int maxMalId = existingIds.Count > 0 ? existingIds.Max() : 0;

            // 🔹 Gera a lista de MalIds esperados (de 1 até o maior encontrado no banco ou até um limite pré-definido)
            var expectedIds = Enumerable.Range(1, maxMalId).ToList();

            // Faz a diferença entre o esperado e o existente
            var missingIds = expectedIds.Except(existingIds).ToList();

            return ResponseFactory.CreateInstance().CreateResponseBasedOnCollectionData(missingIds);
        }
        public async Task<SingleResponse<Manga>> Get(int id)
        {
            try
            {
                Manga Select = _db.Mangas.AsNoTracking().FirstOrDefault(m => m.Id == id);
                return ResponseFactory.CreateInstance().CreateSuccessSingleResponse<Manga>(Select);
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedSingleResponseNotFoundItem<Manga>(ex);
            }
        }
        public async Task<DataResponse<Manga>> Get(int skip, int take)
        {
            try
            {
                List<Manga> mangas = await _db.Mangas
                    .AsNoTracking()
                    .Skip(skip)
                    .Take(take)
                    .ToListAsync();
                return ResponseFactory.CreateInstance().CreateResponseBasedOnCollectionData(mangas);

            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedDataResponse<Manga>(ex);
            }
        }
        public async Task<DataResponse<Manga>> Get(string name)
        {
            try
            {
                List<Manga> mangas = await _db.Mangas.AsNoTracking().Where(M => M.Title.Contains(name)).ToListAsync();
                return ResponseFactory.CreateInstance().CreateResponseBasedOnCollectionData<Manga>(mangas);
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedDataResponse<Manga>(ex);
            }
        }
        public async Task<Response> InsertCategory(Genre id)
        {
            try
            {
                _db.Genre.Add(id);
                //await _db.SaveChangesAsync();
                return ResponseFactory.CreateInstance().CreateSuccessResponse();
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedResponse(ex);
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
        public async Task<int> GetLastIndex()
        {
            try
            {
                Manga? a = _db.Mangas.OrderBy(c => c.Id).LastOrDefault();
                return a.Id;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public async Task<DataResponse<MangaCatalog>> GetByPopularity(int skip, int take)
        {
            try
            {
                List<MangaCatalog> mangas = await _db.Mangas
                    .OrderBy(m => m.Popularity)
                    .AsNoTracking()
                    .Skip(skip)
                    .Take(take)
                    .Select(MangaCatalog.Projection)
                    .ToListAsync();
                return ResponseFactory.CreateInstance().CreateResponseBasedOnCollectionData(mangas);
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedDataResponse<MangaCatalog>(ex);
            }
        }
        public async Task<SingleResponse<Manga>> GetComplete(int ID)
        {
            try
            {
                Manga? Select = _db.Mangas.Include(c => c.Genres).Include(c=> c.Comentaries).ThenInclude(u => u.User).FirstOrDefault(m => m.Id == ID);
                return ResponseFactory.CreateInstance().CreateSuccessSingleResponse<Manga>(Select);
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedSingleResponse<Manga>(ex);
            }
        }
        public async Task<DataResponse<Manga>> GetByCategory(int ID)
        {
            try
            {
                Genre? Select = _db.Genre.Include(c => c.MangasID).FirstOrDefault(m => m.Id == ID);
                return ResponseFactory.CreateInstance().CreateResponseBasedOnCollectionData(Select.MangasID.ToList());
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedDataResponse<Manga>(ex);
            }
        }
        public async Task<DataResponse<MangaCatalog>> GetByRating(int skip, int take)
        {
            try
            {
                List<MangaCatalog> mangas = await _db.Mangas
                    .OrderByDescending(m => m.Rank)
                    .AsNoTracking()
                    .Skip(skip)
                    .Take(take)
                    .Select(MangaCatalog.Projection)
                    .ToListAsync();
                return ResponseFactory.CreateInstance().CreateResponseBasedOnCollectionData(mangas);

            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedDataResponse<MangaCatalog>(ex);
            }
        }
        public async Task<DataResponse<MangaCatalog>> GetByUserCount(int skip, int take)
        {
            try
            {
                List<MangaCatalog> mangas = await _db.Mangas
                    .OrderByDescending(m => m.Members)
                    .AsNoTracking()
                    .Skip(skip)
                    .Take(take)
                    .Select(MangaCatalog.Projection)
                    .ToListAsync();
                return ResponseFactory.CreateInstance().CreateResponseBasedOnCollectionData(mangas);

            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedDataResponse<MangaCatalog>(ex);
            }
        }
        public async Task<DataResponse<MangaCatalog>> GetByFavorites(int skip, int take)
        {
            try
            {
                List<MangaCatalog> mangas = await _db.Mangas
                    .OrderByDescending(m => m.Favorites)
                    .AsNoTracking()
                    .Skip(skip)
                    .Take(take)
                    .Select(MangaCatalog.Projection)
                    .ToListAsync();
                return ResponseFactory.CreateInstance().CreateResponseBasedOnCollectionData(mangas);

            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedDataResponse<MangaCatalog>(ex);
            }
        }

        public async Task<DataResponse<MangaCatalog>> GetHome(int skip, int take)
        {
            throw new NotImplementedException();
        }
    }
}
