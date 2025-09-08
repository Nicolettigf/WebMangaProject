using Shared.Models;
using Shared.Responses;
using System.Data.Common;

namespace DataAccessLayer.Interfaces
{
    public interface IHomeDAL
    {
        Task<DbDataReader> GetTopAnimeManga(int skip, int take);

        Task<DbDataReader> GetHomeMedia(int skip, int take,string table);

        Task<SingleResponse<SearchData>> GetByName(string name);

    }
}
