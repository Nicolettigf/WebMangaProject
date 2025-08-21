using System.Data.Common;

namespace DataAccessLayer.Interfaces
{
    public interface IHomeDAL
    {
        Task<DbDataReader> GetTopAnimeManga(int skip, int take);
    }
}
