using GlassBoard.Abstractions.Provider;
using GlassBoard.Abstractions.Service;
using GlassBoard.Response.Get;
using SharedLibrary.Models;
using GlassBoard.Response;

namespace GlassBoard.Provider
{
    public class ResourceProvider : IResourceProvider
    {
        private readonly IResourceService _service;
        private readonly ITenantProvider _tenantProvider;
        private readonly IProbeProvider _probeProvider;
        private List<ResourceApiDto> _resources = new();

        public ResourceProvider(
            IResourceService service, 
            ITenantProvider tenantProvider, 
            IProbeProvider probeProvider)
        {
            _service = service;
            _tenantProvider = tenantProvider;
            _probeProvider = probeProvider;
        }

        public IReadOnlyList<ResourceApiDto> Resources => _resources;
        
        // Risolve l'errore: 'ResourceProvider' does not implement member 'IsLoaded'
        public bool IsLoaded => _resources.Count != 0;

        public async Task InitializeAsync(bool forceRefresh = false)
        {
            if (_resources.Count != 0 && !forceRefresh) return;
            
            // Carichiamo tutto in parallelo per non bloccare la UI
            var taskResources = _service.GetRootResources();
            var taskTenants = _tenantProvider.InitializeAsync();
            var taskProbes = _probeProvider.InitializeAsync();

            await Task.WhenAll(taskResources, taskTenants, taskProbes);

            _resources = await taskResources;
        }

        // --- Metodi delegati per mantenere l'interfaccia se necessario alla UI ---

        public async Task<string> GetProbeName(string probeId) 
            => _probeProvider.GetProbeName(probeId);

        public async Task<List<ProbeItemResponse>> GetAllProbes() 
            => _probeProvider.GetAllProbes();

        public async Task<string> GetTenantName(string tenantId) 
            => _tenantProvider.GetTenantName(tenantId);

        public async Task<(string TenantName, string OrgName)> GetTenantAndOrgNames(string organizationId) 
            => _tenantProvider.GetTenantAndOrgNames(organizationId);
    }
}