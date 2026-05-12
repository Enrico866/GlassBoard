using GlassBoard.Abstractions.Config;
using GlassBoard.Abstractions.Service;
using GlassBoard.Models.Alerts;
using GlassBoard.Response.Get;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace GlassBoard.Services
{
    public class AlertService : IAlertService
    {
        private readonly HttpClient _http;
        private readonly IConfiguration _config;
        private readonly IAuthService _auth;
        private readonly AlertApiOptions _options;

        public AlertService(HttpClient httpClient, IConfiguration config, IAuthService auth, IOptions<AlertApiOptions> options)
        {
            _http = httpClient;
            _auth = auth;
            _config = config;
            _options = options.Value;
        }

        public async Task<List<AlertModel>> GetAlertsAsync()
        {
            try
            {
                // 1. Recupero del token di autenticazione
                var token = await _auth.GetContextAccessTokenAsync();
                
                // 2. Preparazione della richiesta usando l'URL dalla configurazione globale
                using var request = new HttpRequestMessage(HttpMethod.Get, _options.AlertsUrl);
            
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                request.Headers.Add("X-MYDEV-CHANNEL", _options.Channel);

                // 3. Invio della richiesta
                var response = await _http.SendAsync(request);
                
                if (response.IsSuccessStatusCode)
                {
                    // MODIFICA CRUCIALE: Usiamo GetAlertsHttpResponse invece di GetCheckPolicyHttpResponse
                    // Questo garantisce che il JSON venga mappato su List<AlertModel>
                    var result = await response.Content.ReadFromJsonAsync<GetAlertsHttpResponse>();
                    
                    return result?.Items ?? new List<AlertModel>();
                }

                return new List<AlertModel>();
            }
            catch (Exception ex)
            {
                // Log dell'errore come richiesto
                Console.WriteLine($"[AlertService] Errore critico durante il recupero alert: {ex.Message}");
                return new List<AlertModel>();
            }
        }
    }
}