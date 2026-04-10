using System.Text.Json.Serialization;

namespace GlassBoard.Request.Update
{
    public class UpdateCollectionProfileRequest
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } // ID della Risorsa

        [JsonPropertyName("collectionProfileIds")]
        public List<string> CollectionProfileIds { get; set; } = new();
    }
}
