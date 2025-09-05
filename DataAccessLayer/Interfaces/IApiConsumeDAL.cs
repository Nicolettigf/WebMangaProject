using Entities.Common;
using System.Data.Common;

namespace DataAccessLayer.Interfaces
{
    public interface IApiConsumeDAL
    {
        Task UpdateConsumeStats<T>(ConsumoTipo tipo, string api, int valor) where T : MediaBase;
        Task<ApiConsumeStats> GetStats(string api);
        Task<List<int>> GetIdsFromApi<T>(string apiName, ConsumoTipo tipo) where T : MediaBase;
    }
}
