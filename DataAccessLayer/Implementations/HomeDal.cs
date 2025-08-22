using DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
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
