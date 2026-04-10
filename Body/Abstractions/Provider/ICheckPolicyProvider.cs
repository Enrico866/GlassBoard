using GlassBoard.Response.Get;

namespace GlassBoard.Abstractions.Provider
{
    public interface ICheckPolicyProvider
    {
        List<CheckPolicyModels> Policies { get; }
        bool IsLoaded { get; }
        Task InitializeAsync(bool forceRefresh = false);
        List<CheckPolicyModels> GetPoliciesByIds(List<string> ids);
    }
}