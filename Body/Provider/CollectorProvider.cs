using GlassBoard.Abstractions.Provider;
using GlassBoard.Abstractions.Service;
using GlassBoard.Response.Get;

namespace GlassBoard.Provider
{
    public class CollectorProvider : ICollectorProvider
    {
        private readonly ICollectorService _service;
        private List<CollectorItem> _cache = new();
        
        public bool IsLoaded { get; private set; }

        public CollectorProvider(ICollectorService service)
        {
            _service = service;
        }

        public async Task InitializeAsync(bool forceRefresh = false)
        {
            if (IsLoaded && !forceRefresh) return;

            var items = await _service.GetAllCollectorsAsync();
            _cache = items;
            
            IsLoaded = true;
            Console.WriteLine($"[CollectorProvider] Cache aggiornata: {_cache.Count} sonde.");
        }

        public List<CollectorItem> GetCollectors() => _cache;
    }
}