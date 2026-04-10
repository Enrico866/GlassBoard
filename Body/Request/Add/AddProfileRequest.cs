using System.Text.Json.Serialization;

namespace GlassBoard.Request.Add
{
public class AddProfileRequest
{
    [JsonPropertyName("id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("isDefault")]
    public bool IsDefault { get; set; }

    [JsonPropertyName("schedulingPolicyId")]
    public string SchedulingPolicyId { get; set; }

    [JsonPropertyName("resourceTypes")]
    public string ResourceTypes { get; set; } // Cambiato in stringa per coincidere col JSON

    [JsonPropertyName("observableConfigurationIds")]
    public List<string> ObservableConfigurationIds { get; set; } = new();

    [JsonPropertyName("tags")]
    public List<string> Tags { get; set; } = new();
}
}
