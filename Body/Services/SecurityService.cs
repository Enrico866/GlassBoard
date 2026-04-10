using GlassBoard.Abstractions.Config;
using GlassBoard.Abstractions.Service;
using GlassBoard.Request.Add;
using GlassBoard.Response.Get;

using Microsoft.Extensions.Options;

using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GlassBoard.Services
{
    public class SecurityService : ISecurityService
    {
        private readonly HttpClient _http;
        private readonly IAuthService _auth;
        private readonly SecurityApiOptions _options;
        private readonly JsonSerializerOptions _jsonOptions;

        public SecurityService(HttpClient http, IAuthService auth, IOptions<SecurityApiOptions> options)
        {
            _http = http;
            _auth = auth;
            _options = options.Value;
            
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter() }
            };
        }

        public async Task<bool> CreateSecurityConfigAsync(AddSecurityRequest securityRequest)
        {
            try
            {
                var token = await _auth.GetContextAccessTokenAsync();
                using var request = new HttpRequestMessage(HttpMethod.Post, _options.SecurityUrl);
                
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                request.Headers.Add("X-MYDEV-CHANNEL", _options.Channel);
                
                request.Content = JsonContent.Create(securityRequest, options: _jsonOptions);

                var response = await _http.SendAsync(request);
                
                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"[SecurityService] API Error: {error}");
                }

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[SecurityService] Exception: {ex.Message}");
                return false;
            }
        }

        public async Task<List<SecurityItemResponse>> GetAllSecuritiesAsync()
        {
            try
            {
                var token = await _auth.GetContextAccessTokenAsync();
                using var request = new HttpRequestMessage(HttpMethod.Get, _options.SecurityUrl);
        
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                request.Headers.Add("X-MYDEV-CHANNEL", _options.Channel);

                var response = await _http.SendAsync(request);
        
                if (response.IsSuccessStatusCode)
                {
                    // Assicurati che il JSON dell'API corrisponda (Lista diretta o oggetto con 'Items')
                    var result = await response.Content.ReadFromJsonAsync<SecurityListResponse>(_jsonOptions);
                    return result?.Items ?? new List<SecurityItemResponse>();
                }
                return new List<SecurityItemResponse>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[SecurityService] Errore in GetAllSecuritiesAsync: {ex.Message}");
                return new List<SecurityItemResponse>();
            }
        }
    }
}