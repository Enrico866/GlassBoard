using GlassBoard.Abstractions.Config;
using GlassBoard.Abstractions.Service;
using GlassBoard.Request.Add;
using GlassBoard.Response.Get;

using Microsoft.Extensions.Options;

using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;

public class ObservableService : IObservableService
{
    private readonly HttpClient _http;
    private readonly IAuthService _auth;
    private readonly ObservableApiOptions _options;
    private readonly JsonSerializerOptions _jsonOptions;

    public ObservableService(HttpClient http, IAuthService auth, IOptions<ObservableApiOptions> options)
    {
        _http = http;
        _auth = auth;
        _options = options.Value;
        _jsonOptions = new JsonSerializerOptions { 
            PropertyNameCaseInsensitive = true, 
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            Converters = { new JsonStringEnumConverter() } 
        };
    }

    private async Task<HttpRequestMessage> CreateRequest(HttpMethod method, string url, object? content = null)
    {
        var token = await _auth.GetContextAccessTokenAsync();
        var request = new HttpRequestMessage(method, url);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        request.Headers.Add("X-MYDEV-CHANNEL", _options.Channel);
        if (content != null) request.Content = JsonContent.Create(content, options: _jsonOptions);
        return request;
    }

    public async Task<List<ObservableConfigurationDetailed>> GetConfigurationsAsync()
    {
        using var req = await CreateRequest(HttpMethod.Get, $"{_options.ObservableConfigurationsUrl}s");
        var resp = await _http.SendAsync(req);
        return (await resp.Content.ReadFromJsonAsync<GetObservableConfigurationResponse>(_jsonOptions))?.Items ?? new();
    }

    public async Task<bool> CreateConfigurationsAsync(List<AddConfigurationRequest> configs)
    {
        using var req = await CreateRequest(HttpMethod.Post, $"{_options.ObservableConfigurationsUrl}s", configs);
        return (await _http.SendAsync(req)).IsSuccessStatusCode;
    }

    public async Task<bool> UpdateConfigurationAsync(AddConfigurationRequest config)
    {
        using var req = await CreateRequest(HttpMethod.Put, _options.ObservableConfigurationsUrl, config);
        return (await _http.SendAsync(req)).IsSuccessStatusCode;
    }

    public async Task<bool> DeleteConfigurationAsync(string id)
    {
        using var req = await CreateRequest(HttpMethod.Delete, $"{_options.ObservableConfigurationsUrl}/{id}");
        return (await _http.SendAsync(req)).IsSuccessStatusCode;
    }

    // --- Implementazione Profili (Simile a sopra) ---
    public async Task<List<CollectionProfileDetailed>> GetProfilesAsync()
    {
        using var req = await CreateRequest(HttpMethod.Get, $"{_options.CollectionProfilesUrl}s");
        var resp = await _http.SendAsync(req);
        return (await resp.Content.ReadFromJsonAsync<GetCollectionProfileHttpResponse>(_jsonOptions))?.Items ?? new();
    }

    public async Task<string?> CreateProfileAsync(AddProfileRequest profile)
    {
        using var req = await CreateRequest(HttpMethod.Post, _options.CollectionProfilesUrl, profile);
        var resp = await _http.SendAsync(req);
        if (!resp.IsSuccessStatusCode) return null;
        return (await resp.Content.ReadFromJsonAsync<CollectionProfileDetailed>(_jsonOptions))?.Id;
    }

    public async Task<bool> UpdateProfileAsync(AddProfileRequest profile)
    {
        using var req = await CreateRequest(HttpMethod.Put, _options.CollectionProfilesUrl, profile);
        return (await _http.SendAsync(req)).IsSuccessStatusCode;
    }

    public async Task<bool> DeleteProfileAsync(string profileId)
    {
        using var req = await CreateRequest(HttpMethod.Delete, $"{_options.CollectionProfilesUrl}/{profileId}");
        return (await _http.SendAsync(req)).IsSuccessStatusCode;
    }
}