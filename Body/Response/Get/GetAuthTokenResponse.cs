using System.Text.Json.Serialization;

namespace GlassBoard.Response.Get
{
    public class GetAuthTokenResponse
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }

        [JsonPropertyName("token_type")]
        public string TokenType { get; set; }

        [JsonPropertyName("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonPropertyName("refresh_token")]
        public string RefreshToken { get; set; }

        // Impostata manualmente al momento della ricezione
        public DateTime ExpiryTime { get; set; }
    }
}
