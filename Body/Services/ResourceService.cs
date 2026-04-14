using GlassBoard.Abstractions.Config;
using GlassBoard.Abstractions.Service;
using GlassBoard.Mappers;
using GlassBoard.Response.Get;

using Microsoft.Extensions.Options;

using SharedLibrary.Models;

using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace GlassBoard.Services
{
    public class ResourceService : IResourceService
    {
        private readonly HttpClient _http;
        private readonly IAuthService _authService;
        private readonly ResourceApiOptions _options;

        public ResourceService(
            HttpClient http, 
            IAuthService authService, 
            IOptions<ResourceApiOptions> options)
        {
            _http = http;
            _authService = authService;
            _options = options.Value;
        }

        public async Task<bool> RunDiscoveryAsync(string resourceId, string instrumentType, bool isPreDiscovery, string? organizationId)
        {
            var token = await _authService.GetContextAccessTokenAsync();
        
            var requestBody = new
            {
                ResourceId = resourceId,
                IsPreDiscovery = isPreDiscovery,
                InstrumentType = instrumentType,
                OrganizationId = organizationId
            };

            using var request = new HttpRequestMessage(HttpMethod.Post, _options.DiscoveryUrl);
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            request.Headers.Add("X-MYDEV-CHANNEL", _options.Channel);
            request.Content = JsonContent.Create(requestBody);

            var response = await _http.SendAsync(request);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateMonitoringStatusAsync(string resourceId, bool isObserved)
        {
            var token = await _authService.GetContextAccessTokenAsync();
            var url = $"{_options.EndpointAddResource.TrimEnd('/')}/{resourceId}";

            using var request = new HttpRequestMessage(HttpMethod.Put, url);
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            request.Headers.Add("X-MYDEV-CHANNEL", _options.Channel);
            request.Content = JsonContent.Create(new { isObserved });

            var response = await _http.SendAsync(request);
            return response.IsSuccessStatusCode;
        }

        private async Task<HttpRequestMessage> CreateRequestAsync(HttpMethod method, string url)
        {
            var token = await _authService.GetContextAccessTokenAsync();
            var request = new HttpRequestMessage(method, url);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            request.Headers.Add("X-MYDEV-CHANNEL", _options.Channel);
            return request;
        }

        public async Task<ResourceItem> GetResourceWithChildren(string id)
        {
            var baseUrl = _options.EndpointAddResource.TrimEnd('/');
            using var request = await CreateRequestAsync(HttpMethod.Get, $"{baseUrl}s/{id}");
            var response = await _http.SendAsync(request);
            
            if (!response.IsSuccessStatusCode) return new ResourceItem();

            var dto = await response.Content.ReadFromJsonAsync<ResourceApiDto>();
            if (dto == null) return new ResourceItem();

            var finalResource = dto.ToDomain();

            var childrenUrl = $"{baseUrl}s/{id}/children?offset=0&limit=2000";
            using var requestChild = await CreateRequestAsync(HttpMethod.Get, childrenUrl);
            var childrenResponse = await _http.SendAsync(requestChild);

            if (childrenResponse.IsSuccessStatusCode)
            {
                var childrenData = await childrenResponse.Content.ReadFromJsonAsync<GetResourceHttpResponse>();
                if (childrenData?.Items != null)
                {
                    finalResource.Children = childrenData.Items.Select(c => c.ToDomain()).ToList();
                }
            }
            return finalResource;
        }

        public async Task<List<ResourceApiDto>> GetRootResources()
        {
            var url = $"{_options.EndpointAddResource.TrimEnd('/')}s?rootsOnly=true";
            using var request = await CreateRequestAsync(HttpMethod.Get, url);
            var response = await _http.SendAsync(request);
            
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<GetResourceHttpResponse>();
                return result?.Items ?? new List<ResourceApiDto>();
            }
            return new List<ResourceApiDto>();
        }
    }
}