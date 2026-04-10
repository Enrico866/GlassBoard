using static GlassBoard.Response.Get.GetTenantHttpResponse;

namespace GlassBoard.Abstractions.Provider
{
    public interface ITenantProvider
    {
        List<TenantItemResponse> Tenants { get; }
        bool IsLoaded { get; }
        Task InitializeAsync(bool forceRefresh = false);
        
        // Metodi sincroni: restituiscono direttamente i dati
        string GetTenantName(string tenantId);
        (string TenantName, string OrgName) GetTenantAndOrgNames(string organizationId);
    }
}