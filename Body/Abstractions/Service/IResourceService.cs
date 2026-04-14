using GlassBoard.Response.Get;

using SharedLibrary.Models;

using static SharedLibrary.Enum.Enums;

namespace GlassBoard.Abstractions.Service
{
    public interface IResourceService
    {
        Task<List<ResourceApiDto>> GetRootResources();
        Task<ResourceItem> GetResourceWithChildren(string id);

        Task<bool> RunDiscoveryAsync(string resourceId, string instrumentType, bool isPreDiscovery, string? organizationId);

        Task<bool> UpdateMonitoringStatusAsync(string resourceId, bool isObserved);
    }
}