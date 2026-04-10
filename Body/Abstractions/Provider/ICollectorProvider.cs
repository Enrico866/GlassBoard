using GlassBoard.Response.Get;

namespace GlassBoard.Abstractions.Provider
{
    public interface ICollectorProvider
    {
        bool IsLoaded { get; }
        Task InitializeAsync(bool forceRefresh = false);
        List<CollectorItem> GetCollectors();
    }
}
