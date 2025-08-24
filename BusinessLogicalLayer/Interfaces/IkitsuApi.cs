using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicalLayer.Interfaces
{
    public interface IKitsuApi
    {
        Task<RootKitsu> BuscarPagina<T>(string endpoint, int offset = 0, int limit = 20) where T : class;

        Task BuscarECompararAnimePorIds(int maxId);
    }
}
