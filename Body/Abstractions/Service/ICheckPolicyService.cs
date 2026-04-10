using GlassBoard.Response.Get;

namespace GlassBoard.Abstractions.Service
{
    public interface ICheckPolicyService
    {
        Task<List<CheckPolicyModels>> GetAllPoliciesAsync();
    }
}