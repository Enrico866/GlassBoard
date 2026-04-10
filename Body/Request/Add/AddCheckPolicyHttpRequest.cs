using System.Text.Json.Serialization;

namespace GlassBoard.Request.Add
{
    public class AddCheckPolicyHttpRequest
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = null; // Di solito null in creazione

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("namespace")]
        public string Namespace { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("checkIds")]
        public List<string> CheckIds { get; set; } = new();
    }
}