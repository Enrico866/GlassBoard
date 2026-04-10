using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.Extensions.Options;
using GlassBoard.Abstractions.Config;
using GlassBoard.Abstractions.Service;
using GlassBoard.Response.Get;

namespace GlassBoard.Services
{
    public class ProbeService : IProbeService
    {
        private readonly HttpClient _http;
        private readonly IAuthService _authService;
        private readonly ProbeApiOptions _options;

        public ProbeService(
            HttpClient http, 
            IAuthService authService, 
            IOptions<ProbeApiOptions> options)
        {
            _http = http;
            _authService = authService;
            _options = options.Value;
        }

        public async Task<List<ProbeItemResponse>> GetAllProbesAsync()
        {
            try
            {
                // Utilizzo credenziali admin configurate
                var token = await _authService.GetAccessTokenByParamAsync(_options.AdminEmail, _options.AdminPassword);
                
                using var request = new HttpRequestMessage(HttpMethod.Get, _options.ProbesUrl);
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                request.Headers.Add("X-MYDEV-CHANNEL", _options.ChannelAdmin);

                var response = await _http.SendAsync(request);
                
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadFromJsonAsync<ProbeListResponse>();
                    return data?.Items ?? new List<ProbeItemResponse>();
                }
                
                return new List<ProbeItemResponse>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ProbeService] Errore HTTP: {ex.Message}");
                return new List<ProbeItemResponse>();
            }
        }
    }
}