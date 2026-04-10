using GlassBoard.Response.Get;

namespace GlassBoard.Abstractions.Provider
{
    public interface INamespaceProvider
    {
        List<NamespaceModel> namespaces { get; }
        bool IsLoaded { get; }
        Task InitializeAsync(bool forceRefresh = false);
        NamespaceModel GetNamespaceByName(string name);
    }
}