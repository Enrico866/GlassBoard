using GlassBoard.Response.Get;

namespace GlassBoard.Abstractions.Service
{
    public interface INamespaceService
    {
        Task<List<NamespaceModel>> GetAllNamespacesAsync();
    }
}