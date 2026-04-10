using GlassBoard.Response;
using GlassBoard.Response.Get;

using SharedLibrary.Models;

namespace GlassBoard.Abstractions.Provider
{
    public interface IResourceProvider
    {
        IReadOnlyList<ResourceApiDto> Resources { get; }
        bool IsLoaded { get; }
        Task InitializeAsync(bool forceRefresh = false);
    }
}