using GlassBoard.Abstractions.Config;
using GlassBoard.Abstractions.Service;
using GlassBoard.Response.Get;

using Microsoft.Extensions.Options;

using System.Net.Http.Headers;
using System.Net.Http.Json;

public class CheckPolicyService : ICheckPolicyService
{
    private readonly HttpClient _http;
    private readonly IAuthService _auth;
    private readonly CheckPolicyApiOptions _options;

    public CheckPolicyService(
        HttpClient http, 
        IAuthService auth, 
        IOptions<CheckPolicyApiOptions> options)
    {
        _http = http;
        _auth = auth;
        _options = options.Value;
    }

    public async Task<List<CheckPolicyModels>> GetAllPoliciesAsync()
    {
        try
        {
            var token = await _auth.GetContextAccessTokenAsync();
            using var request = new HttpRequestMessage(HttpMethod.Get, _options.CheckPoliciesUrl);
            
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            request.Headers.Add("X-MYDEV-CHANNEL", _options.Channel);

            var response = await _http.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<GetCheckPolicyHttpResponse>();
                return result?.Items ?? new List<CheckPolicyModels>();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[CheckPolicyService] Errore durante il recupero delle policy: {ex.Message}");
        }
        return new List<CheckPolicyModels>();
    }
}