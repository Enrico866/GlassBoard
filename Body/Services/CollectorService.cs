using GlassBoard.Abstractions.Config;
using GlassBoard.Abstractions.Service;
using GlassBoard.Response.Get;

using Microsoft.Extensions.Options;

using System.Net.Http.Headers;
using System.Net.Http.Json;

public class CollectorService : ICollectorService
{
    private readonly HttpClient _http;
    private readonly IAuthService _auth;
    private readonly CollectorApiOptions _options;

    public CollectorService(
        HttpClient http, 
        IAuthService auth, 
        IOptions<CollectorApiOptions> options)
    {
        _http = http;
        _auth = auth;
        _options = options.Value;
    }

    public async Task<List<CollectorItem>> GetAllCollectorsAsync()
    {
        try
        {
            // Recupero token tramite credenziali admin configurate
            var token = await _auth.GetAccessTokenByParamAsync(_options.AdminUser, _options.AdminPassword);
            
            using var request = new HttpRequestMessage(HttpMethod.Get, _options.CollectorsUrl);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            request.Headers.Add("X-MYDEV-CHANNEL", _options.ChannelAdmin);

            var response = await _http.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadFromJsonAsync<GetCollectorResponse>();
                return data?.Items ?? new List<CollectorItem>();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[CollectorService] Errore: {ex.Message}");
        }
        
        return new List<CollectorItem>();
    }
}