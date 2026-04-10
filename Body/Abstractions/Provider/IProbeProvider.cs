using GlassBoard.Response.Get;

namespace GlassBoard.Abstractions.Provider
{
    public interface IProbeProvider
    {
        bool IsLoaded { get; }
        Task InitializeAsync(bool forceRefresh = false);
        string GetProbeName(string probeId);
        List<ProbeItemResponse> GetAllProbes();
        List<ProbeItemResponse> Probes { get; }
    }
}