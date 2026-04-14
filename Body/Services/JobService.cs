using GlassBoard.Abstractions.Config;
using GlassBoard.Abstractions.Service;
using GlassBoard.Response.Get;

using Microsoft.Extensions.Options;

using System.Net.Http.Headers;

public class JobService : IJobService
{
    private readonly HttpClient _http;
    private readonly IAuthService _auth;
    private readonly JobApiOptions _options;

    public JobService(HttpClient http, IAuthService auth, IOptions<JobApiOptions> options)
    {
        _http = http;
        _auth = auth;
        _options = options.Value;
    }

    public async Task<(bool Success, List<JobItem> Jobs)> GenerateAndNotifyAsync(string probeId, string tenantId, string orgId, string dbName)
    {
        var token = await _auth.GetAccessTokenByParamAsync(_options.AdminUser, _options.AdminPassword);
        var jobs = new List<JobItem>();

        // 1. Generate and Persist
        using var genRequest = CreateRequest(HttpMethod.Post, _options.JobGenerateAndPersist, token, tenantId, orgId, dbName, probeId);
        var genResponse = await _http.SendAsync(genRequest);
        
        if (!genResponse.IsSuccessStatusCode) return (false, jobs);

        var result = await genResponse.Content.ReadFromJsonAsync<GetJobPersistResponse>();
        jobs = result?.Items ?? new List<JobItem>();

        // 2. Notify Refresh
        using var notifyRequest = CreateRequest(HttpMethod.Post, _options.JobNotifyRefresh, token, tenantId, orgId, dbName, probeId);
        var notifyResponse = await _http.SendAsync(notifyRequest);

        return (notifyResponse.IsSuccessStatusCode, jobs);
    }

    private HttpRequestMessage CreateRequest(HttpMethod method, string url, string token, string tenantId, string orgId, string dbName, string probeId)
    {
        var request = new HttpRequestMessage(method, url);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        request.Headers.Add("x-database-name", dbName);
        request.Headers.Add("x-tenant-id", tenantId);
        request.Headers.Add("x-organization-id", orgId);
        request.Headers.Add("X-MYDEV-CHANNEL", "admin");
        request.Headers.Add("X-MYDEV-APPNAME", "GlassBoard");
        request.Content = JsonContent.Create(new { probeId });
        return request;
    }
}