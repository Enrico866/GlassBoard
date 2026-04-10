using GlassBoard.Abstractions.Provider;
using GlassBoard.Abstractions.Service;
using GlassBoard.Response.Get;

namespace GlassBoard.Provider
{
    public class SecurityProvider : ISecurityProvider
    {
        private readonly ISecurityService _service;
        private List<SecurityItemResponse> _securities = new();
        private bool _isInitialized = false;

        public SecurityProvider(ISecurityService service) => _service = service;

        public List<SecurityItemResponse> Securities => _securities;
        public bool IsLoaded => _isInitialized;

        public async Task InitializeAsync(bool forceRefresh = false)
        {
            if (_isInitialized && !forceRefresh) return;
            _securities = await _service.GetAllSecuritiesAsync();
            _isInitialized = true;
        }

        public string GetSecurityName(string securityId)
        {
            return _securities.FirstOrDefault(s => s.Id == securityId)?.Name ?? "Unknown Security";
        }
    }
}