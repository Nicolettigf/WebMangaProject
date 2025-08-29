using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicalLayer.Interfaces
{
    public interface IKitsuApi
    {
        Task BuscarECompararPorIds<T>() where T : MediaBase;
    }
}
