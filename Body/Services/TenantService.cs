using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.Extensions.Options;
using GlassBoard.Abstractions.Config;
using GlassBoard.Abstractions.Service;
using static GlassBoard.Response.Get.GetTenantHttpResponse;

namespace GlassBoard.Services
{
    public class TenantService : ITenantService
    {
        private readonly HttpClient _http;
        private readonly IAuthService _authService;
        private readonly TenantApiOptions _options;

        public TenantService(
            HttpClient http, 
            IAuthService authService, 
            IOptions<TenantApiOptions> options)
        {
            _http = http;
            _authService = authService;
            _options = options.Value;
        }

        public async Task<List<TenantItemResponse>> GetAllTenantsAsync()
        {
            var token = await _authService.GetAccessTokenByParamAsync(_options.AdminEmail, _options.AdminPassword);
            
            using var request = new HttpRequestMessage(HttpMethod.Get, _options.TenantUrl);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            request.Headers.Add("X-MYDEV-CHANNEL", _options.Channel);

            var response = await _http.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadFromJsonAsync<TenantListResponse>();
                return data?.Items ?? new List<TenantItemResponse>();
            }
            return new List<TenantItemResponse>();
        }
    }
}