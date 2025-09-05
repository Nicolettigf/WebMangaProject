using Entities.Common;

namespace BusinessLogicalLayer.Interfaces
{
    public interface IJikanApi
    {
        Task ConsumeGenre<T>() where T : MediaBase;
        Task ConsumeMissingMedia<T>() where T : MediaBase, new();
        Task ConsumeMedia<T>() where T : MediaBase, new();
    }
}
