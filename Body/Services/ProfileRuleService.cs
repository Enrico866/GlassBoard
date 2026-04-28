using GlassBoard.Abstractions.Service;
using GlassBoard.Request.Add;

using Microsoft.AspNetCore.Http;

using System.Net.Http.Headers;

public class ProfileRuleService : IProfileRuleService
{
    private readonly HttpClient _http;
    private readonly IConfiguration _config;
    private readonly IAuthService _authService;

    public ProfileRuleService(HttpClient http, IConfiguration config, IAuthService authService)
    {
        _http = http;
        _config = config;
        _authService = authService;
    }

    public async Task<ProfileRuleResponse> GetAllProfileRulesAsync()
    {
        var url = _config["ResourceApi:ProfileRulesUrl"];
        
        // Se il token fallisce per colpa del NavigationManager, lo intercettiamo
        string token;
        try {
            token = await _authService.GetContextAccessTokenAsync();
        } catch (InvalidOperationException) {
            // Fallback: se il contesto non è pronto, non possiamo fare la chiamata
            return new ProfileRuleResponse(); 
        }

        using var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await _http.SendAsync(request);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<ProfileRuleResponse>() ?? new();
        }
        return new ProfileRuleResponse();
    }

    public async Task<bool> AddProfileRuleAsync(AddProfileRuleHttpRequest request)
    {
        var url = _config["ResourceApi:ProfileRulesUrl"];
        
        try 
        {
            var token = await _authService.GetContextAccessTokenAsync();
            
            // Usiamo PostAsJsonAsync che è più robusto
            _http.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            _http.DefaultRequestHeaders.Add("X-MYDEV-CHANNEL", "enduser");
            var response = await _http.PostAsJsonAsync(url, request);
            
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Service Error]: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> UpdateProfileRuleAsync(string id, AddProfileRuleHttpRequest request)
    {
        var url = $"{_config["ResourceApi:ProfileRulesUrl"]}/{id}"; // Assumendo url/id
        var token = await _authService.GetContextAccessTokenAsync();
    
        _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        _http.DefaultRequestHeaders.Add("X-MYDEV-CHANNEL", "enduser");
        var response = await _http.PutAsJsonAsync(url, request);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteProfileRuleAsync(string id)
    {
        var url = $"{_config["ResourceApi:ProfileRulesUrl"]}/{id}";
        var token = await _authService.GetContextAccessTokenAsync();
    
        using var request = new HttpRequestMessage(HttpMethod.Delete, url);
        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await _http.SendAsync(request);
        return response.IsSuccessStatusCode;
    }
}