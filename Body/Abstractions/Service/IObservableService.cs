using GlassBoard.Request.Add;
using GlassBoard.Response.Get;

namespace GlassBoard.Abstractions.Service
{
    public interface IObservableService
    {
        // Configurazioni
        Task<List<ObservableConfigurationDetailed>> GetConfigurationsAsync();
        Task<bool> CreateConfigurationsAsync(List<AddConfigurationRequest> configs);
        Task<bool> UpdateConfigurationAsync(AddConfigurationRequest config);
        Task<bool> DeleteConfigurationAsync(string id);

        // Profili
        Task<List<CollectionProfileDetailed>> GetProfilesAsync();
        Task<string?> CreateProfileAsync(AddProfileRequest profile);
        Task<bool> UpdateProfileAsync(AddProfileRequest profile);
        Task<bool> DeleteProfileAsync(string profileId);
    }
}