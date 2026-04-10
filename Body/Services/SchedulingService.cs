using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.Extensions.Options;
using GlassBoard.Abstractions.Config;
using GlassBoard.Abstractions.Service;
using GlassBoard.Response.Get;
using SharedLibrary.Models;

namespace GlassBoard.Services
{
    public class SchedulingService : ISchedulingService
    {
        private readonly HttpClient _http;
        private readonly IAuthService _auth;
        private readonly SchedulingApiOptions _options;

        public SchedulingService(
            HttpClient http, 
            IAuthService auth, 
            IOptions<SchedulingApiOptions> options)
        {
            _http = http;
            _auth = auth;
            _options = options.Value;
        }

        private async Task<HttpRequestMessage> CreateRequestAsync(HttpMethod method, string url)
        {
            var token = await _auth.GetContextAccessTokenAsync();
            var request = new HttpRequestMessage(method, url);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            if (!string.IsNullOrEmpty(_options.Channel))
            {
                request.Headers.Add("X-MYDEV-CHANNEL", _options.Channel);
            }
            return request;
        }

        public async Task<List<SchedulingPolicy>> GetAllPoliciesAsync()
        {
            try
            {
                using var request = await CreateRequestAsync(HttpMethod.Get, _options.SchedulingUrl);
                var response = await _http.SendAsync(request);
                
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<GetSchedulingHttpResponse>();
                    return result?.Items ?? new List<SchedulingPolicy>();
                }
                return new List<SchedulingPolicy>();
            }
            catch { return new List<SchedulingPolicy>(); }
        }

        public async Task<bool> CreatePolicyAsync(SchedulingPolicy policy)
        {
            using var request = await CreateRequestAsync(HttpMethod.Post, _options.SchedulingUrl);
            request.Content = JsonContent.Create(policy);
            var response = await _http.SendAsync(request);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdatePolicyAsync(SchedulingPolicy policy)
        {
            var url = $"{_options.SchedulingUrl.TrimEnd('/')}/{policy.Id}";
            using var request = await CreateRequestAsync(HttpMethod.Put, url);
            request.Content = JsonContent.Create(policy);
            var response = await _http.SendAsync(request);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeletePolicyAsync(string id)
        {
            var url = $"{_options.SchedulingUrl.TrimEnd('/')}/{id}";
            using var request = await CreateRequestAsync(HttpMethod.Delete, url);
            var response = await _http.SendAsync(request);
            return response.IsSuccessStatusCode;
        }
    }
}