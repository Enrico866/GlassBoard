using GlassBoard.Abstractions.Provider;
using GlassBoard.Abstractions.Service;
using GlassBoard.Request.Add;
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

            try 
            {
                var items = await _service.GetAllChecksAsync();
                _checks = items ?? new List<CheckModel>();
                _isInitialized = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[CheckProvider] Errore durante l'inizializzazione: {ex.Message}");
                _checks = new List<CheckModel>();
            }
        }

        public CheckModel GetCheckById(string id)
        {
            return _checks.FirstOrDefault(c => c.Id == id) ?? new CheckModel();
        }

        public async Task<string?> AddCheckAsync(AddCheckHttpRequest request)
        {
            var newId = await _service.AddCheckAsync(request);
            if (!string.IsNullOrEmpty(newId))
            {
                // Opzionale: rinfresca la cache dopo l'aggiunta
                await InitializeAsync(forceRefresh: true);
            }
            return newId;
        }

        public List<CheckModel> GetChecksByMetric(string metricName)
        {
            if (string.IsNullOrEmpty(metricName)) return new List<CheckModel>();

            // Logica di navigazione della gerarchia mantenuta e pulita
            return _checks.Where(c => 
                c.DataSampleQuery?.InputQueries != null && 
                c.DataSampleQuery.InputQueries.Any(m => 
                    string.Equals(m.InputName, metricName, StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(m.ProjectedInputName, metricName, StringComparison.OrdinalIgnoreCase)
                )
            ).ToList();
        }
    }
}