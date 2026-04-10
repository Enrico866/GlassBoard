using GlassBoard.Response.Get;

using SharedLibrary.Models;

namespace GlassBoard.Abstractions.Service
{
    public interface IResourceService
    {
        Task<List<ResourceApiDto>> GetRootResources();
        Task<ResourceItem> GetResourceWithChildren(string id);
    }
}