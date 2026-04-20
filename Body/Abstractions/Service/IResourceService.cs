using GlassBoard.Request.Add;
using GlassBoard.Request.Update;
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

        Task<HttpResponseMessage?> AddResource(AddResourceHttpRequest request);

        Task<HttpResponseMessage?> UpdateResourceAttributes(UpdateResourceAttributesRequest request);

        Task<HttpResponseMessage?> UpdateCollectionProfileAsync(UpdateCollectionProfileRequest request);
    }
}