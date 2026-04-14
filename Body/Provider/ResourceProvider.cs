using GlassBoard.Abstractions.Provider;
using GlassBoard.Abstractions.Service;
using GlassBoard.Response;
using GlassBoard.Response.Get;

using Microsoft.AspNetCore.Components;

using MudBlazor;

using SharedLibrary.Models;

using static SharedLibrary.Enum.Enums;

namespace GlassBoard.Provider
{
    public class ResourceProvider : IResourceProvider
    {
        private readonly IResourceService _service;
        private readonly ITenantProvider _tenantProvider;
        private readonly IProbeProvider _probeProvider;
        private readonly ISnackbar _snackbar;
        private List<ResourceApiDto> _resources = new();

        public ResourceProvider(
            IResourceService service, 
            ITenantProvider tenantProvider, 
            IProbeProvider probeProvider,
            ISnackbar snackbar)
        {
            _service = service;
            _tenantProvider = tenantProvider;
            _probeProvider = probeProvider;
            _snackbar = snackbar;
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

        public async Task ToggleMonitoringAsync(ResourceItem resource, EventCallback onUpdated)
        {
            var success = await _service.UpdateMonitoringStatusAsync(resource.Id, !resource.IsMonitored);
            if (success)
            {
                await onUpdated.InvokeAsync();
            }
        }

        public async Task RunMultipleDiscoveriesAsync(string resourceId, List<InstrumentTypes> selectedInstruments, string orgIdStr)
        {
       
            var tasks = selectedInstruments.Select(inst => 
                _service.RunDiscoveryAsync(resourceId, inst.ToString(), inst == InstrumentTypes.Snmp, orgIdStr));

            var results = await Task.WhenAll(tasks);
            int successCount = results.Count(r => r);

            if (successCount > 0) _snackbar.Add($"{successCount} Discovery inviate", Severity.Success);
            if (successCount < selectedInstruments.Count) _snackbar.Add("Alcune richieste sono fallite", Severity.Error);
        }
    }
}