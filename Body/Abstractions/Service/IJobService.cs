using GlassBoard.Response.Get;

namespace GlassBoard.Abstractions.Service
{
    public interface IJobService
    {
        Task<(bool Success, List<JobItem> Jobs)> GenerateAndNotifyAsync(string probeId, string tenantId, string orgId, string dbName);
    }
}