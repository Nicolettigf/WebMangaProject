using DataAccessLayer.Interfaces;
using DataAccessLayer.Interfaces.IAnimeInterfaces;
using DataAccessLayer.Interfaces.IMangaInterfaces;
using DataAccessLayer.Interfaces.IUserComentary;
using DataAccessLayer.Interfaces.IUSerInterfaces;
using DataAccessLayer.Interfaces.IUserItem;
using Entities.Common;
using Microsoft.EntityFrameworkCore;
using Shared.Interfaces;
using Shared.Responses;

namespace DataAccessLayer.UnitOfWork
{
    public interface IUnitOfWork
    {
        IUserDAL UserRepository { get; }
        IMangaDAL MangaRepository { get; }
        IAnimeDAL AnimeRepository { get; }
        IHomeDAL HomeRepository { get; }
        IApiConsumeDAL ApiConsumeRepository { get; }
        IApiReInsertStatsDAL ApiReInsertStatRepository { get; }
        IUserMangaItemDAL UserMangaItemRepository { get; }
        IUserAnimeItemDAL UserAnimeItemRepository { get; }
        IMangaComentaryDAL MangaComentaryRepository { get; }
        IAnimeComentaryDAL AnimeComentaryRepository { get; }


        /// <summary>
        /// Persiste todas as alterações pendentes no DbContext no banco de dados.
        /// </summary>
        /// <returns>Um Response indicando sucesso ou falha da operação.</returns>
        Task<Response> Commit();

        /// <summary>
        /// Persiste alterações específicas do usuário no DbContext no banco de dados.
        /// </summary>
        /// <returns>Um Response indicando sucesso ou falha da operação.</returns>
        Task<Response> CommitForUser();

        /// <summary>
        /// Retorna o repositório (DAL) específico para o tipo de entidade T.
        /// </summary>
        /// <typeparam name="T">Tipo da entidade, derivada de MediaBase.</typeparam>
        /// <returns>Um ICRUD da entidade.</returns>
        ICRUD<T> GetDAL<T>() where T : MediaBase;

        /// <summary>
        /// Retorna um IQueryable para a entidade T, permitindo consultas LINQ.
        /// As entidades serão rastreadas pelo DbContext.
        /// </summary>
        /// <typeparam name="T">Tipo da entidade.</typeparam>
        /// <returns>IQueryable da entidade.</returns>
        IQueryable<T> Query<T>() where T : class;

        /// <summary>
        /// Desfaz todas as alterações pendentes no contexto antes de um commit,
        /// útil para desfazer transações em caso de erro.
        /// </summary>
        /// <returns>Uma Task que representa a operação assíncrona de rollback.</returns>
        Task Rollback();

        /// <summary>
        /// Retorna um IQueryable sem rastrear alterações pelo EF Core,
        /// ideal para consultas de leitura que não precisam ser monitoradas.
        /// </summary>
        /// <typeparam name="T">Tipo da entidade.</typeparam>
        /// <returns>Uma queryable da entidade sem tracking.</returns>
        IQueryable<T> QueryNoTracking<T>() where T : class;

        /// <summary>
        /// Desanexa a entidade do DbContext para que não seja mais rastreada,
        /// liberando memória ou evitando alterações indesejadas.
        /// </summary>
        /// <typeparam name="T">Tipo da entidade.</typeparam>
        /// <param name="entity">A entidade a ser desanexada.</param>
        void Detach<T>(T entity) where T : class;

        /// <summary>
        /// Busca uma entidade pelo seu identificador.
        /// </summary>
        /// <typeparam name="T">Tipo da entidade.</typeparam>
        /// <param name="id">ID da entidade.</param>
        /// <returns>A entidade encontrada ou null caso não exista.</returns>
        Task<T> FindByIdAsync<T>(int id) where T : class;

        /// <summary>
        /// Executa uma query SQL bruta diretamente no banco de dados.
        /// Útil para queries complexas, performance ou procedures.
        /// </summary>
        /// <param name="sql">Comando SQL a ser executado.</param>
        /// <param name="parameters">Parâmetros opcionais da query.</param>
        /// <returns>O número de linhas afetadas.</returns>
        Task<int> ExecuteSqlAsync(string sql, params object[] parameters);

        /// <summary>
        /// Reseta o ChangeTracker do DbContext, removendo o rastreamento de todas
        /// as entidades carregadas atualmente. Útil em cenários de processamento em lote.
        /// </summary>
        void ResetChangeTracker();
    }
}
