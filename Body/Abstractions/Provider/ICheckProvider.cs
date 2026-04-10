using GlassBoard.Response.Get;

namespace GlassBoard.Abstractions.Provider
{
    public interface ICheckProvider
    {
        List<CheckModel> Checks { get; }
        bool IsLoaded { get; }
        Task InitializeAsync(bool forceRefresh = false);
        CheckModel GetCheckById(string id);
        List<CheckModel> GetChecksByMetric(string metricName);
    }
}