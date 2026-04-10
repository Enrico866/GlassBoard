using System.Text.Json.Serialization;

namespace GlassBoard.Request.Update
{
    public class UpdateResourceCheckPoliciesRequest
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } // ID della Risorsa (es. il server o device)

        [JsonPropertyName("checkProfileIds")]
        public List<string> CheckProfileIds { get; set; } = new();
    }
}
