using GlassBoard.Response.Get; // Assumendo che esista un DTO di risposta

namespace GlassBoard.Abstractions.Provider
{
    public interface ISecurityProvider
    {
        List<SecurityItemResponse> Securities { get; }
        bool IsLoaded { get; }
        Task InitializeAsync(bool forceRefresh = false);
        string GetSecurityName(string securityId);
    }
}