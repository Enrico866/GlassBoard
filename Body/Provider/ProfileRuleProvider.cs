using GlassBoard.Request.Add;

public class ProfileRuleProvider : IProfileRuleProvider
{
    private readonly IProfileRuleService _service;

    public ProfileRuleProvider(IProfileRuleService service)
    {
        _service = service;
    }

    public async Task<ProfileRuleResponse> GetProfileRulesAsync()
    {
        try
        {
            return await _service.GetAllProfileRulesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ProfileRuleProvider] Errore: {ex.Message}");
            return new ProfileRuleResponse { Items = new() };
        }
    }

    public async Task<bool> AddProfileRuleAsync(AddProfileRuleHttpRequest request)
    {
        return await _service.AddProfileRuleAsync(request);
    }

    public async Task<bool> UpdateProfileRuleAsync(string id, AddProfileRuleHttpRequest request)
    {
        // Chiama il service che a sua volta farà la PUT
        return await _service.UpdateProfileRuleAsync(id, request);
    }

    public async Task<bool> DeleteProfileRuleAsync(string id)
    {
        return await _service.DeleteProfileRuleAsync(id);
    }
}