using GlassBoard.Abstractions.Config;
using GlassBoard.Abstractions.Service;
using GlassBoard.Response.Get;

using Microsoft.Extensions.Options;

using System.Net.Http.Headers;
using System.Net.Http.Json;

public class CheckService : ICheckService
{
    private readonly HttpClient _http;
    private readonly IAuthService _auth;
    private readonly CheckApiOptions _options;

    public CheckService(
        HttpClient http, 
        IAuthService auth, 
        IOptions<CheckApiOptions> options)
    {
        _http = http;
        _auth = auth;
        _options = options.Value;
    }

    public async Task<List<CheckModel>> GetAllChecksAsync()
    {
        try
        {
            var token = await _auth.GetContextAccessTokenAsync();
            using var request = new HttpRequestMessage(HttpMethod.Get, _options.ChecksUrl);
            
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            request.Headers.Add("X-MYDEV-CHANNEL", _options.Channel);

            var response = await _http.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<GetCheckHttpResponse>();
                return result?.Items ?? new List<CheckModel>();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[CheckService] Errore API: {ex.Message}");
        }
        return new List<CheckModel>();
    }
}