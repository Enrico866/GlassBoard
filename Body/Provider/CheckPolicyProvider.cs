using GlassBoard.Abstractions.Provider;
using GlassBoard.Abstractions.Service;
using GlassBoard.Response.Get;

namespace GlassBoard.Provider
{
    public class CheckPolicyProvider : ICheckPolicyProvider
    {
        private readonly ICheckPolicyService _service;
        private List<CheckPolicyModels> _policies = new();
        private bool _isInitialized = false;

        public List<CheckPolicyModels> Policies => _policies;
        public bool IsLoaded => _isInitialized && _policies.Any();

        public CheckPolicyProvider(ICheckPolicyService service)
        {
            _service = service;
        }

        public async Task InitializeAsync(bool forceRefresh = false)
        {
            if (_isInitialized && !forceRefresh) return;

            var items = await _service.GetAllPoliciesAsync();
            _policies = items;
            _isInitialized = true;
            
            Console.WriteLine($"[CheckPolicyProvider] Cache aggiornata: {_policies.Count} policy caricate.");
        }

        public List<CheckPolicyModels> GetPoliciesByIds(List<string> ids)
        {
            if (ids == null || !ids.Any()) return new List<CheckPolicyModels>();
            return _policies.Where(p => ids.Contains(p.Id)).ToList();
        }
    }
}