using System.Text.Json.Serialization;

namespace GlassBoard.Request.Update
{
    public class UpdateResourceAttributesRequest
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("resourceAttributes")]
        public List<AttributeItem> ResourceAttributes { get; set; } = new();
    }

    public class AttributeItem 
    { 
        [JsonPropertyName("key")]
        public string Key { get; set; } 
    
        [JsonPropertyName("value")]
        public string Value { get; set; } 
    }   
}
