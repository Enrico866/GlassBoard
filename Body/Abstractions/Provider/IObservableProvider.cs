using GlassBoard.Request.Add;
using GlassBoard.Response.Get;
using SharedLibrary.Models;
using System.Net.Http.Headers;

namespace GlassBoard.Abstractions.Provider
{
    public interface IObservableProvider
    {
        bool IsLoaded { get; }
        Dictionary<string, ObservableConfigurationDetailed> configCache { get; }

        Task InitializeAsync(bool forceRefresh = false);

        // Metodi per i Profili (Sincroni e Async)
        List<CollectionProfileDetailed> GetAllAvailableProfiles();
        List<CollectionProfileDetailed> GetProfiles(List<string>? ids = null); // <--- AGGIUNTO
        Task<List<CollectionProfileDetailed>> GetProfilesAsync();

        // Metodi per le Configurazioni
        Dictionary<string, ObservableConfigurationDetailed> GetConfigs(List<string>? ids = null);
        ObservableConfigurationDetailed GetConfigById(string id);
        CollectionProfileDetailed GetProfileForOc(string ocId);

        // Scheduling
        List<SchedulingPolicy> GetSchedulingPolicies();
        Task<List<SchedulingPolicy>> GetSchedulingPoliciesAsync(AuthenticationHeaderValue auth, string channel);

        // Operazioni CRUD
        Task<string?> CreateConfigurationAsync(List<AddConfigurationRequest> config);
        Task<string?> UpdateConfigurationAsync(string id, AddConfigurationRequest config);
        Task<bool> DeleteConfigurationAsync(string id);
        Task<string?> CreateProfileAsync(AddProfileRequest profile);
        Task<string?> UpdateProfileAsync(string profileId, AddProfileRequest profile);
        Task<string?> DeleteProfileAsync(string profileId);
    }
}