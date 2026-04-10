using System.Text.Json.Serialization;

namespace GlassBoard.Request.Update
{
    public class UpdateResourceIsObserved
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("isObserved")]
        public bool IsObserved { get; set; } = new();
    }
}
