using SharedLibrary.Models;

namespace GlassBoard.Abstractions.Provider
{
public interface ISchedulingProvider
{
    List<SchedulingPolicy> Policies { get; }
    bool IsLoaded { get; }
    Task InitializeAsync(bool forceRefresh = false);
    SchedulingPolicy GetPolicyById(string id);
    Task<bool> CreatePolicyAsync(SchedulingPolicy policy);
    Task<bool> UpdatePolicyAsync(SchedulingPolicy policy);
    Task<bool> DeletePolicyAsync(string id);
}
}
