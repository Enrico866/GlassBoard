using GlassBoard.Abstractions.Service;
using GlassBoard.Response.Get;
using GlassBoard.Services;

public class AuthService : IAuthService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _config;
    private readonly AppContextService _context;
    private GetAuthTokenResponse _tokenData;

    public AuthService(HttpClient httpClient, IConfiguration config, AppContextService context)
    {
        _httpClient = httpClient;
        _config = config;
        _context = context;

        // Quando il contesto cambia (DB/Utente), svuotiamo il token memorizzato
        _context.OnContextChanged += () => { _tokenData = null; };
    }

    /// <summary>
    /// Recupera il token per l'utente corretto nel contesto (con gestione cache e refresh)
    /// </summary>
    public async Task<string> GetContextAccessTokenAsync()
    {
        if (_tokenData != null && DateTime.UtcNow < _tokenData.ExpiryTime.AddSeconds(-30))
        {
            return _tokenData.AccessToken;
        }

        if (_tokenData != null && !string.IsNullOrEmpty(_tokenData.RefreshToken))
        {
            try { return await RefreshTokenAsync(); }
            catch { return await InitialLoginAsync(); }
        }

        return await InitialLoginAsync();
    }

    /// <summary>
    /// Metodo speciale per richiedere un token con credenziali specifiche (bypassando il contesto)
    /// </summary>
    public async Task<string> GetAccessTokenByParamAsync(string email, string password)
    {
        var dict = new Dictionary<string, string>
        {
            { "grant_type", "password" },
            { "client_id", _config["IdentityApi:ClientId"] },
            { "username", email },
            { "password", password },
            { "scope", _config["IdentityApi:Scope"] }
        };

        // Nota: questo metodo non sovrascrive _tokenData per non "rompere" la sessione dell'utente standard
        return await SendTokenRequest(dict, updateCache: false, _config["ResourceApi:ChannelAdmin"]);
    }

    private async Task<string> InitialLoginAsync()
    {
        var dict = new Dictionary<string, string>
        {
            { "grant_type", "password" },
            { "client_id", _config["IdentityApi:ClientId"] },
            { "username", _context.CurrentUsername },
            { "password", _context.CurrentPassword },
            { "scope", _config["IdentityApi:Scope"] }
        };
        return await SendTokenRequest(dict, updateCache: true);
    }

    private async Task<string> RefreshTokenAsync()
    {
        var dict = new Dictionary<string, string>
        {
            { "grant_type", "refresh_token" },
            { "refresh_token", _tokenData.RefreshToken },
            { "client_id", _config["IdentityApi:ClientId"] },
        };
        return await SendTokenRequest(dict, updateCache: true);
    }

    private async Task<string> SendTokenRequest(Dictionary<string, string> parameters, bool updateCache, string channel="enduser")
    {
        var tokenUrl = _config["IdentityApi:TokenUrl"];
        var request = new HttpRequestMessage(HttpMethod.Post, tokenUrl)
        {
            Content = new FormUrlEncodedContent(parameters)
        };

        request.Headers.Add("X-MYDEV-CLIENT", "myvemweb");
        request.Headers.Add("X-MYDEV-CHANNEL", channel);

        var response = await _httpClient.SendAsync(request);
        
        if (response.IsSuccessStatusCode)
        {
            var data = await response.Content.ReadFromJsonAsync<GetAuthTokenResponse>();
            if (data != null)
            {
                data.ExpiryTime = DateTime.UtcNow.AddSeconds(data.ExpiresIn);
                
                if (updateCache)
                {
                    _tokenData = data;
                }
                
                return data.AccessToken;
            }
        }

        var errorUser = parameters.ContainsKey("username") ? parameters["username"] : "Refresh Token";
        throw new Exception($"Errore autenticazione per {errorUser}: {response.ReasonPhrase}");
    }
}