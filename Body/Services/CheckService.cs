using GlassBoard.Abstractions.Service;
using GlassBoard.Request.Add;
using GlassBoard.Response.Add;
using GlassBoard.Response.Get;

using MongoDB.Bson;

using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

public class CheckService : ICheckService
{
    private readonly HttpClient _http;
    private readonly IConfiguration _config;
    private readonly IAuthService _authService;

    public CheckService(HttpClient http, IConfiguration config, IAuthService authService)
    {
        _http = http;
        _config = config;
        _authService = authService;
    }

    // Metodo privato di utilità per evitare la duplicazione del codice e gestire il crash del NavigationManager
    private async Task<string?> GetAccessTokenSafelyAsync()
    {
        try
        {
            return await _authService.GetContextAccessTokenAsync();
        }
        catch (Exception ex) when (ex.Message.Contains("RemoteNavigationManager") || ex is InvalidOperationException)
        {
            // Se il manager non è pronto, logghiamo e ritorniamo null
            Console.WriteLine("[CheckService] RemoteNavigationManager non ancora inizializzato.");
            return null;
        }
    }

    public async Task<List<CheckModel>> GetAllChecksAsync()
    {
        var url = _config["ResourceApi:ChecksUrl"];
        var token = await GetAccessTokenSafelyAsync();

        if (string.IsNullOrEmpty(token)) return new List<CheckModel>();

        using var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await _http.SendAsync(request);
        var jsonString = await response.Content.ReadAsStringAsync();
        if (string.IsNullOrWhiteSpace(jsonString))
        {
            return new List<CheckModel>();
        }
        var apiResponse = JsonSerializer.Deserialize<GetCheckHttpResponse>(jsonString, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
        return apiResponse?.Items ?? new List<CheckModel>();
    }

    public async Task<string?> AddCheckAsync(AddCheckHttpRequest request)
    {
        var url = _config["ResourceApi:ChecksUrl"];
        var channel = _config["ResourceApi:Channel"] ?? "enduser";

        // --- DEBUG PER TEST API ---
        var options = new JsonSerializerOptions 
        { 
            WriteIndented = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase // Spesso le API scalari vogliono camelCase
        };
        string jsonDebug = JsonSerializer.Serialize(request, options);
        Console.WriteLine("--- COPIA QUESTO JSON PER TESTARE L'API ---");
        Console.WriteLine(jsonDebug);
        // ---------------------------

        var token = await GetAccessTokenSafelyAsync();
        if (string.IsNullOrEmpty(token)) return null;

        try
        {
            using var httpRequest = new HttpRequestMessage(HttpMethod.Post, url);
            httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            httpRequest.Headers.Add("X-MYDEV-CHANNEL", channel);
            
            // Usiamo il contenuto serializzato con le opzioni corrette
            httpRequest.Content = JsonContent.Create(request, options: options);

            var response = await _http.SendAsync(httpRequest);
            
            if (response.IsSuccessStatusCode)
            {
                var res = await response.Content.ReadFromJsonAsync<AddCheckResponse>();
                return res?.Id;
            }
            else
            {
                var errorBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"[CheckService] Errore API ({response.StatusCode}): {errorBody}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[CheckService Error]: {ex.Message}");
        }
        return null;
    }
}