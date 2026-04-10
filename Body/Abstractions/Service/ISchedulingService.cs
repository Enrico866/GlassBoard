using SharedLibrary.Models;

namespace GlassBoard.Abstractions.Service
{
    public interface ISchedulingService
    {
        Task<List<SchedulingPolicy>> GetAllPoliciesAsync();
        Task<bool> CreatePolicyAsync(SchedulingPolicy policy);
        Task<bool> UpdatePolicyAsync(SchedulingPolicy policy);
        Task<bool> DeletePolicyAsync(string id);
    }
}