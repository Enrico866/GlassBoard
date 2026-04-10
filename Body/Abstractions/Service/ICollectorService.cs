using GlassBoard.Response.Get;

namespace GlassBoard.Abstractions.Service
{
    public interface ICollectorService
    {
        Task<List<CollectorItem>> GetAllCollectorsAsync();
    }
}