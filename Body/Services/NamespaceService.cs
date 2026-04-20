using GlassBoard.Abstractions.Config;
using GlassBoard.Abstractions.Service;
using GlassBoard.Request.Add;
using GlassBoard.Response.Get;

using Microsoft.Extensions.Options;

using System.Net.Http.Headers;

using static System.Net.WebRequestMethods;

public class NamespaceService : INamespaceService
{
    private readonly HttpClient _http;
    private readonly IAuthService _auth;
    private readonly NamespaceApiOptions _options;

    public NamespaceService(
        HttpClient http, 
        IAuthService auth, 
        IOptions<NamespaceApiOptions> options) // Utilizzo di IOptions
    {
        _http = http;
        _auth = auth;
        _options = options.Value;
    }

    public async Task<List<NamespaceModel>> GetAllNamespacesAsync()
    {
        var token = await _auth.GetContextAccessTokenAsync();
        
        using var request = new HttpRequestMessage(HttpMethod.Get, _options.NamespacesUrl);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        request.Headers.Add("X-MYDEV-CHANNEL", _options.Channel);

        var response = await _http.SendAsync(request);
        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<GetNamespaceHttpResponse>();
            return result?.Items ?? new List<NamespaceModel>();
        }
        return new List<NamespaceModel>();
    }

    public async Task<HttpResponseMessage?> AddNamespace(AddNamespaceHttpRequest request)
    {
        var url = _options.NamespacesUrl.ToString().Replace("s/","");
        //var token = await AuthService.GetAccessTokenByParamAsync(Config["Tenant:Admin:User"], Config["Tenant:Admin:Password"]);
        var token = await _auth.GetContextAccessTokenAsync();

        using var httpRequest = new HttpRequestMessage(HttpMethod.Post, url);
        httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        httpRequest.Headers.Add("X-MYDEV-CHANNEL", _options.Channel);
        httpRequest.Headers.Add("X-MYDEV-APPNAME", "GlassBoard");
        httpRequest.Content = JsonContent.Create(request);

        return await _http.SendAsync(httpRequest);
    }
}