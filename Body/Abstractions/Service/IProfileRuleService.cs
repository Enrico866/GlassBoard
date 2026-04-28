using GlassBoard.Request.Add;

public interface IProfileRuleService
{
    Task<ProfileRuleResponse> GetAllProfileRulesAsync();

    Task<bool> AddProfileRuleAsync(AddProfileRuleHttpRequest request);

    Task<bool> UpdateProfileRuleAsync(string id, AddProfileRuleHttpRequest request);

    Task<bool> DeleteProfileRuleAsync(string id);
}