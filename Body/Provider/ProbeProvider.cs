using GlassBoard.Abstractions.Provider;
using GlassBoard.Abstractions.Service;
using GlassBoard.Response.Get;

namespace GlassBoard.Provider
{
    public class ProbeProvider : IProbeProvider
    {
        private readonly IProbeService _service;
        private List<ProbeItemResponse> _probes = new();

        public ProbeProvider(IProbeService service)
        {
            _service = service;
        }

        public bool IsLoaded => _probes.Any();
        public List<ProbeItemResponse> Probes => _probes;

        public async Task InitializeAsync(bool forceRefresh = false)
        {
            if (_probes.Any() && !forceRefresh) return;

            var data = await _service.GetAllProbesAsync();
            _probes = data;
            
            Console.WriteLine($"[ProbeProvider] Cache aggiornata: {_probes.Count} sonde pronte.");
        }

        public string GetProbeName(string probeId)
        {
            if (string.IsNullOrEmpty(probeId)) return "Unknown Probe";
            return _probes.FirstOrDefault(p => p.Id == probeId)?.Name ?? "Unknown Probe";
        }

        public List<ProbeItemResponse> GetAllProbes()
        {
            // Restituiamo una copia della lista per sicurezza (o la lista stessa se read-only)
            return _probes.ToList();
        }
    }
}