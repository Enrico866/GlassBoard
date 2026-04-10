using GlassBoard.Abstractions.Provider;
using GlassBoard.Abstractions.Service;
using static GlassBoard.Response.Get.GetTenantHttpResponse;

namespace GlassBoard.Provider
{
    public class TenantProvider : ITenantProvider
    {
        private readonly ITenantService _service;
        private List<TenantItemResponse> _tenants = new();

        public TenantProvider(ITenantService service)
        {
            _service = service;
        }

        public bool IsLoaded => _tenants.Any();
        public List<TenantItemResponse> Tenants => _tenants;

        public async Task InitializeAsync(bool forceRefresh = false)
        {
            if (_tenants.Any() && !forceRefresh) return;
            _tenants = await _service.GetAllTenantsAsync();
        }

        // RIMOSSO Task: è un'operazione di memoria rapida
        public string GetTenantName(string tenantId)
        {
            if (string.IsNullOrEmpty(tenantId)) return "Unknown Tenant";
            return _tenants.FirstOrDefault(t => t.Id == tenantId)?.Name ?? "Unknown Tenant";
        }

        // RIMOSSO Task: restituisce direttamente la tupla
        public (string TenantName, string OrgName) GetTenantAndOrgNames(string organizationId)
        {
            if (string.IsNullOrEmpty(organizationId)) return ("Non trovato", "Non trovato");

            foreach (var tenant in _tenants)
            {
                var org = tenant.Organizations?.FirstOrDefault(o => o.Id == organizationId);
                if (org != null)
                {
                    return (tenant.Name, org.Name);
                }
            }
            return ("Non trovato", "Non trovato");
        }
    }
}