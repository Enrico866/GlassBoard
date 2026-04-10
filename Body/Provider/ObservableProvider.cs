using GlassBoard.Abstractions.Provider;
using GlassBoard.Abstractions.Service;
using GlassBoard.Request.Add;
using GlassBoard.Response.Get;
using SharedLibrary.Models;
using System.Net.Http.Headers;

namespace GlassBoard.Provider
{
    public class ObservableProvider : IObservableProvider
    {
        private readonly IObservableService _service;
        private readonly ISchedulingProvider _schedulingProvider;

        // Cache interne
        private Dictionary<string, ObservableConfigurationDetailed> _configCache = new();
        private List<CollectionProfileDetailed> _profileCache = new();
        private bool _isInitialized = false;

        public ObservableProvider(
            IObservableService service, 
            ISchedulingProvider schedulingProvider)
        {
            _service = service;
            _schedulingProvider = schedulingProvider;
        }

        // Proprietà richieste dall'interfaccia
        public bool IsLoaded => _isInitialized;
        public Dictionary<string, ObservableConfigurationDetailed> configCache => _configCache;

        /// <summary>
        /// Carica tutti i dati necessari (Profili, Configurazioni e Scheduling) in parallelo.
        /// </summary>
        public async Task InitializeAsync(bool forceRefresh = false)
        {
            if (_isInitialized && !forceRefresh) return;

            try
            {
                // Lanciamo i task in parallelo per velocità
                var configsTask = _service.GetConfigurationsAsync();
                var profilesTask = _service.GetProfilesAsync();
                var schedulingTask = _schedulingProvider.InitializeAsync(forceRefresh);

                await Task.WhenAll(configsTask, profilesTask, schedulingTask);

                // Popoliamo le cache con i risultati
                var configs = await configsTask;
                _configCache = configs.ToDictionary(x => x.Id, x => x);
                _profileCache = await profilesTask;
                
                _isInitialized = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ObservableProvider] Errore critico durante l'inizializzazione: {ex.Message}");
                _isInitialized = false;
            }
        }

        #region Metodi di Lettura (Cache-First)

        public List<CollectionProfileDetailed> GetAllAvailableProfiles() => _profileCache;

        // Implementazione per risolvere l'errore GetProfilesAsync se richiesto dall'interfaccia
        public async Task<List<CollectionProfileDetailed>> GetProfilesAsync()
        {
            await InitializeAsync();
            return _profileCache;
        }

        public Dictionary<string, ObservableConfigurationDetailed> GetConfigs(List<string>? ids = null)
        {
            if (ids == null || !ids.Any()) return _configCache;
            return _configCache.Where(x => ids.Contains(x.Key)).ToDictionary(x => x.Key, x => x.Value);
        }

        public List<CollectionProfileDetailed> GetProfiles(List<string>? ids = null)
        {
            if (ids == null || !ids.Any()) 
                return _profileCache.ToList();

            return _profileCache.Where(p => ids.Contains(p.Id)).ToList();
        }

        public ObservableConfigurationDetailed GetConfigById(string id)
        {
            return !string.IsNullOrEmpty(id) && _configCache.TryGetValue(id, out var config) 
                ? config 
                : new ObservableConfigurationDetailed();
        }

        public CollectionProfileDetailed GetProfileForOc(string ocId)
        {
            return _profileCache.FirstOrDefault(p => 
                p.ObservableConfigurationIds != null && 
                p.ObservableConfigurationIds.Any(id => id == ocId)) ?? new CollectionProfileDetailed();
        }

        // Risolve l'errore del metodo legacy che chiedeva parametri HTTP
        // Ora restituisce semplicemente la lista dalla cache dello SchedulingProvider
        public List<SchedulingPolicy> GetSchedulingPolicies() => _schedulingProvider.Policies;

        // Se l'interfaccia richiede ancora la versione Async vecchia, la implementiamo come wrapper
        public Task<List<SchedulingPolicy>> GetSchedulingPoliciesAsync(AuthenticationHeaderValue auth, string channel)
            => Task.FromResult(_schedulingProvider.Policies);

        #endregion

        #region Metodi di Scrittura (Service + Refresh)

        public async Task<string?> CreateConfigurationAsync(List<AddConfigurationRequest> config)
        {
            var success = await _service.CreateConfigurationsAsync(config);
            if (success) { await InitializeAsync(true); return "SUCCESS"; }
            return null;
        }

        public async Task<string?> UpdateConfigurationAsync(string id, AddConfigurationRequest config)
        {
            var success = await _service.UpdateConfigurationAsync(config);
            if (success) { await InitializeAsync(true); return "SUCCESS"; }
            return null;
        }

        public async Task<bool> DeleteConfigurationAsync(string id)
        {
            var success = await _service.DeleteConfigurationAsync(id);
            if (success) { await InitializeAsync(true); return true; }
            return false;
        }

        public async Task<string?> CreateProfileAsync(AddProfileRequest profile)
        {
            var newId = await _service.CreateProfileAsync(profile);
            if (newId != null) { await InitializeAsync(true); return "SUCCESS"; }
            return null;
        }

        public async Task<string?> UpdateProfileAsync(string profileId, AddProfileRequest profile)
        {
            var success = await _service.UpdateProfileAsync(profile);
            if (success) { await InitializeAsync(true); return "SUCCESS"; }
            return null;
        }

        public async Task<string?> DeleteProfileAsync(string profileId)
        {
            var success = await _service.DeleteProfileAsync(profileId);
            if (success) { await InitializeAsync(true); return "SUCCESS"; }
            return null;
        }

        #endregion
    }
}