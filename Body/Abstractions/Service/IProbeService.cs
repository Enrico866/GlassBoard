using GlassBoard.Response.Get;

namespace GlassBoard.Abstractions.Service
{
    public interface IProbeService
    {
        Task<List<ProbeItemResponse>> GetAllProbesAsync();
    }
}