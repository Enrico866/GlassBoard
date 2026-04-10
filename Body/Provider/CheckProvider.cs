using GlassBoard.Abstractions.Provider;
using GlassBoard.Abstractions.Service;
using GlassBoard.Response.Get;

namespace GlassBoard.Provider
{
    public class CheckProvider : ICheckProvider
    {
        private readonly ICheckService _service;
        private List<CheckModel> _checks = new();
        private bool _isInitialized = false;

        public List<CheckModel> Checks => _checks;
        public bool IsLoaded => _isInitialized;

        public CheckProvider(ICheckService service)
        {
            _service = service;
        }

        public async Task InitializeAsync(bool forceRefresh = false)
        {
            if (_isInitialized && !forceRefresh) return;

            var items = await _service.GetAllChecksAsync();
            _checks = items;
            _isInitialized = true;
            
            Console.WriteLine($"[CheckProvider] Cache caricata con {_checks.Count} check.");
        }

        public CheckModel GetCheckById(string id)
        {
            return _checks.FirstOrDefault(c => c.Id == id) ?? new CheckModel();
        }

        public List<CheckModel> GetChecksByMetric(string metricName)
        {
            if (string.IsNullOrEmpty(metricName)) return new List<CheckModel>();

            // Logica di navigazione della gerarchia mantenuta e pulita
            return _checks.Where(c => 
                c.DataSampleQuery?.MetricQueries != null && 
                c.DataSampleQuery.MetricQueries.Any(m => 
                    string.Equals(m.MetricName, metricName, StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(m.ProjectedMetricName, metricName, StringComparison.OrdinalIgnoreCase)
                )
            ).ToList();
        }
    }
}