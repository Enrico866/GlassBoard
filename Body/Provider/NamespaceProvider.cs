using GlassBoard.Abstractions.Provider;
using GlassBoard.Abstractions.Service;
using GlassBoard.Response.Get;

namespace GlassBoard.Provider
{
    public class NamespaceProvider : INamespaceProvider
    {
        private readonly INamespaceService _service;
        private readonly IObservableProvider _observableProvider;
        
        private List<NamespaceModel> _namespaces = new();

        public List<NamespaceModel> namespaces => _namespaces;
        public bool IsLoaded => _namespaces.Any();

        public NamespaceProvider(INamespaceService service, IObservableProvider observableProvider)
        {
            _service = service;
            _observableProvider = observableProvider;
        }

        public async Task InitializeAsync(bool forceRefresh = false)
        {
            if (IsLoaded && !forceRefresh) return;

            try
            {
                // 1. Carichiamo (o refreshiamo) la cache delle configurazioni
                await _observableProvider.InitializeAsync(forceRefresh);

                // 2. Chiamiamo il service per i namespace
                var rawNamespaces = await _service.GetAllNamespacesAsync();

                // 3. Arricchimento dati: colleghiamo i namespace alle OC reali nel provider
                foreach (var ns in rawNamespaces)
                {
                    // Puliamo la lista attuale prima di ripopolarla (fondamentale in caso di refresh)
                    ns.ObservableConfigurations.Clear();

                    if (ns.observableConfigurationIds != null)
                    {
                        foreach (var ocId in ns.observableConfigurationIds)
                        {
                            // Usiamo il metodo del provider che abbiamo sistemato prima
                            var foundOc = _observableProvider.GetConfigById(ocId);
                            
                            ns.ObservableConfigurations.Add(new ObservableConfig
                            {
                                Id = ocId,
                                Name = string.IsNullOrEmpty(foundOc.Name) ? "Oc Not Found" : foundOc.Name,
                                Description = string.IsNullOrEmpty(foundOc.Description) ? "Oc Not Found" : foundOc.Description
                            });
                        }
                    }
                }

                _namespaces = rawNamespaces;
                Console.WriteLine($"[NamespaceProvider] Caricati {_namespaces.Count} namespace.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[NamespaceProvider] Errore critico: {ex.Message}");
            }
        }

        public NamespaceModel GetNamespaceByName(string name)
        {
            if (string.IsNullOrEmpty(name)) return null!;
            return _namespaces.FirstOrDefault(n => string.Equals(n.Name, name, StringComparison.OrdinalIgnoreCase))!;
        }
    }
}