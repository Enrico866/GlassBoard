using System.Text.Json.Serialization;

namespace GlassBoard.Response.Add
{
    public class AddCheckPolicyResponse
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
    }
}