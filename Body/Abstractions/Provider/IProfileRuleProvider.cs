using GlassBoard.Request.Add;

public interface IProfileRuleProvider
{
    Task<bool> AddProfileRuleAsync(AddProfileRuleHttpRequest request);
    // Aggiungiamo questo:
    Task<ProfileRuleResponse> GetProfileRulesAsync(); 

    Task<bool> UpdateProfileRuleAsync(string id, AddProfileRuleHttpRequest request);

    Task<bool> DeleteProfileRuleAsync(string id);
}