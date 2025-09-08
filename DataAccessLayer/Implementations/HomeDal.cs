using DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using Shared.Models;
using Shared.Responses;
using System.Data;
using System.Data.Common;

namespace DataAccessLayer.Implementations
{
    public class HomeDal : IHomeDAL
    {
        private readonly MangaProjectDbContext _db;
        public HomeDal(MangaProjectDbContext db)
        {
            this._db = db;
        }

        public async Task<SingleResponse<SearchData>> GetByName(string name)
        {
            // Instancia o objeto de retorno
            var searchData = new SearchData();

            // Valida o nome
            if (string.IsNullOrWhiteSpace(name))
                return ResponseFactory.CreateInstance().CreateSuccessSingleResponse(searchData);

            // Filtra Mangás (até 50)
            searchData.Mangas = await _db.Mangas
                .Where(m => m.Title.Contains(name) || (m.TitleEnglish != null && m.TitleEnglish.Contains(name)))
                .OrderBy(m => m.Title) // opcional, ordena alfabeticamente
                .Take(12)
                .Select(MediaCatalog.Projection)
                .ToListAsync();

            // Filtra Animes (até 50)
            searchData.Animes = await _db.Animes
                .Where(m => m.Title.Contains(name) || (m.TitleEnglish != null && m.TitleEnglish.Contains(name)))
                .OrderBy(a => a.Title)
                .Take(12)
                .Select(MediaCatalog.Projection)
                .ToListAsync();

            // Retorna encapsulado no SingleResponse
            return ResponseFactory.CreateInstance().CreateSuccessSingleResponse(searchData);
        }

        public async Task<DbDataReader> GetHomeMedia(int skip, int take, string tableName)
        {
            var connection = _db.Database.GetDbConnection();
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = "sp_GetHomeMedia";
            command.CommandType = CommandType.StoredProcedure;

            var skipParam = command.CreateParameter();
            skipParam.ParameterName = "@Skip";
            skipParam.Value = skip;
            command.Parameters.Add(skipParam);

            var takeParam = command.CreateParameter();
            takeParam.ParameterName = "@Take";
            takeParam.Value = take;
            command.Parameters.Add(takeParam);

            var tableParam = command.CreateParameter();
            tableParam.ParameterName = "@TableName";
            tableParam.Value = tableName;
            command.Parameters.Add(tableParam);

            return await command.ExecuteReaderAsync(CommandBehavior.CloseConnection);
        }

        public async Task<DbDataReader> GetTopAnimeManga(int skip, int take)
        {
            var connection = _db.Database.GetDbConnection();
            await connection.OpenAsync();
            var command = connection.CreateCommand();
            command.CommandText = "sp_GetTopAnimeManga";
            command.CommandType = CommandType.StoredProcedure;

            var skipParam = command.CreateParameter();
            skipParam.ParameterName = "@Skip";
            skipParam.Value = skip;
            command.Parameters.Add(skipParam);

            var takeParam = command.CreateParameter();
            takeParam.ParameterName = "@Take";
            takeParam.Value = take;
            command.Parameters.Add(takeParam);

            return await command.ExecuteReaderAsync(/*CommandBehavior.CloseConnection*/);
        }
    }
}
