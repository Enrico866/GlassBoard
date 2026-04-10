using GlassBoard.Abstractions.Provider;
using GlassBoard.Abstractions.Service;
using SharedLibrary.Models;

namespace GlassBoard.Provider
{
    public class SchedulingProvider : ISchedulingProvider
    {
        private readonly ISchedulingService _service;
        private List<SchedulingPolicy> _policies = new();
        private bool _isInitialized = false;

        public SchedulingProvider(ISchedulingService service)
        {
            _service = service;
        }

        public List<SchedulingPolicy> Policies => _policies;
        public bool IsLoaded => _isInitialized;

        public async Task InitializeAsync(bool forceRefresh = false)
        {
            if (_isInitialized && !forceRefresh) return;

            _policies = await _service.GetAllPoliciesAsync();
            _isInitialized = true;
        }

        public async Task<bool> CreatePolicyAsync(SchedulingPolicy policy)
        {
            var success = await _service.CreatePolicyAsync(policy);
            if (success) await InitializeAsync(true);
            return success;
        }

        public async Task<bool> UpdatePolicyAsync(SchedulingPolicy policy)
        {
            var success = await _service.UpdatePolicyAsync(policy);
            if (success) await InitializeAsync(true);
            return success;
        }

        public async Task<bool> DeletePolicyAsync(string id)
        {
            var success = await _service.DeletePolicyAsync(id);
            if (success) await InitializeAsync(true);
            return success;
        }

        public SchedulingPolicy GetPolicyById(string id)
        {
            // Metodo sincrono per lookup rapido in UI
            return _policies.FirstOrDefault(p => p.Id == id) ?? new SchedulingPolicy();
        }
    }
}