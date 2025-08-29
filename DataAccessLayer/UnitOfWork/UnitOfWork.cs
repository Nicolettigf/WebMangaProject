using DataAccessLayer.ErrorHandling;
using DataAccessLayer.Implementations;
using DataAccessLayer.Implementations.UserComentaryDAL;
using DataAccessLayer.Implementations.UserItemDAL;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Interfaces.IAnimeInterfaces;
using DataAccessLayer.Interfaces.IMangaInterfaces;
using DataAccessLayer.Interfaces.IUserComentary;
using DataAccessLayer.Interfaces.IUSerInterfaces;
using DataAccessLayer.Interfaces.IUserItem;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shared;
using Shared.Responses;

namespace DataAccessLayer.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly MangaProjectDbContext _dbContext;
        private readonly IServiceProvider _serviceProvider; // precisa disso
        private IHomeDAL homeRepository = null;
        private IApiConsumeDAL apiConsumeRepository = null;
        private IUserDAL userRepository = null;
        private IMangaDAL mangaRepository = null;
        private IAnimeDAL animeRepository = null;
        private IUserMangaItemDAL userMangaItemRepository = null;
        private IUserAnimeItemDAL userAnimeItemRepository = null;
        private IMangaComentaryDAL mangaComentaryRepository = null;
        private IAnimeComentaryDAL animeComentaryRepository = null;
        private IApiReInsertStatsDAL apiReInsertStatsRepository = null;

        public UnitOfWork(MangaProjectDbContext dbContext, IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _serviceProvider = serviceProvider;
        }

        public IQueryable<T> Query<T>() where T : class
        {
            return _dbContext.Set<T>();
        }

        public ICRUD<T> GetDAL<T>() where T : MediaBase
        {
            return _serviceProvider.GetRequiredService<ICRUD<T>>();
        }

        public async Task<Response> Commit()
        {
            try
            {
                await _dbContext.SaveChangesAsync();
                return ResponseFactory.CreateInstance().CreateSuccessResponse();
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedResponse(ex);
            }
        }

        public async Task<Response> CommitForUser()
        {
            try
            {
                await _dbContext.SaveChangesAsync();
                return ResponseFactory.CreateInstance().CreateSuccessResponse();
            }
            catch (Exception ex)
            {
                return UserDbFailed.Handle(ex);
            }
        }

        private bool disposed = false;

        public IUserDAL UserRepository
        {
            get
            {
                if (userRepository == null)
                {
                    userRepository = new UserDAL(_dbContext);
                }
                return userRepository;
            }
        }

        public IMangaDAL MangaRepository
        {
            get
            {
                if (mangaRepository == null)
                {
                    mangaRepository = new MangaDAL(_dbContext);
                }
                return mangaRepository;
            }
        }

        public IAnimeDAL AnimeRepository
        {
            get
            {
                if (animeRepository == null)
                {
                    animeRepository = new AnimeDAL(_dbContext);
                }
                return animeRepository;
            }
        }

        public IUserMangaItemDAL UserMangaItemRepository
        {
            get
            {
                if (userMangaItemRepository == null)
                {
                    userMangaItemRepository = new UserMangaItemDAL(_dbContext);
                }
                return userMangaItemRepository;
            }
        }

        public IUserAnimeItemDAL UserAnimeItemRepository
        {
            get
            {
                if (userAnimeItemRepository == null)
                {
                    userAnimeItemRepository = new UserAnimeItemDAL(_dbContext);
                }
                return userAnimeItemRepository;
            }
        }

        public IMangaComentaryDAL MangaComentaryRepository
        {
            get
            {
                if (mangaComentaryRepository == null)
                {
                    mangaComentaryRepository = new MangaComentaryDAL(_dbContext);
                }
                return mangaComentaryRepository;
            }
        }

        public IAnimeComentaryDAL AnimeComentaryRepository
        {
            get
            {
                if (animeComentaryRepository == null)
                {
                    animeComentaryRepository = new AnimeComentaryDAL(_dbContext);
                }
                return animeComentaryRepository;
            }
        }

        public IHomeDAL HomeRepository
        {
            get
            {
                if (homeRepository == null)
                {
                    homeRepository = new HomeDal(_dbContext);
                }
                return homeRepository;
            }
        }

        public IApiConsumeDAL ApiConsumeRepository
        {
            get
            {
                if (apiConsumeRepository == null)
                {
                    apiConsumeRepository = new ApiConsumeDal(_dbContext);
                }
                return apiConsumeRepository;
            }
        }
        public IApiReInsertStatsDAL ApiReInsertStatRepository
        {
            get
            {
                if (apiReInsertStatsRepository == null)
                {
                    apiReInsertStatsRepository = new ApiReInsertStatsDAL(_dbContext);
                }
                return apiReInsertStatsRepository;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Desfaz todas as alterações pendentes no DbContext.
        /// </summary>
        public async Task Rollback()
        {
            // Desfaz alterações pendentes no DbContext
            foreach (var entry in _dbContext.ChangeTracker.Entries()
                                             .Where(e => e.State != EntityState.Unchanged))
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        entry.CurrentValues.SetValues(entry.OriginalValues);
                        entry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Unchanged;
                        break;
                }
            }
            await Task.CompletedTask;
        }

        /// <summary>
        /// Retorna um IQueryable sem rastreamento de alterações.
        /// Ideal para leitura apenas.
        /// </summary>
        public IQueryable<T> QueryNoTracking<T>() where T : class
        {
            return _dbContext.Set<T>().AsNoTracking();
        }

        /// <summary>
        /// Desanexa uma entidade específica do DbContext, deixando de rastrear alterações.
        /// </summary>
        public void Detach<T>(T entity) where T : class
        {
            _dbContext.Entry(entity).State = EntityState.Detached;
        }

        /// <summary>
        /// Busca uma entidade pelo ID.
        /// </summary>
        public async Task<T> FindByIdAsync<T>(int id) where T : class
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        /// <summary>
        /// Executa uma query SQL bruta diretamente no banco de dados.
        /// </summary>
        public async Task<int> ExecuteSqlAsync(string sql, params object[] parameters)
        {
            return await _dbContext.Database.ExecuteSqlRawAsync(sql, parameters);
        }

        /// <summary>
        /// Reseta o ChangeTracker do DbContext, removendo o rastreamento de todas as entidades carregadas.
        /// </summary>
        public void ResetChangeTracker()
        {
            _dbContext.ChangeTracker.Clear();
        }
    }
}
