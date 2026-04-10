using System.Text.Json.Serialization;

namespace GlassBoard.Response.Get
{
public class GetNamespaceHttpResponse
{
    [JsonPropertyName("items")]
    public List<NamespaceModel> Items { get; set; } = new();
}

public class NamespaceModel
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    public List<ObservableConfig> ObservableConfigurations { get; set; } = new();

    [JsonPropertyName("observableConfigurationIds")]
    public List<string> observableConfigurationIds { get; set; } = new();
}

public class ObservableConfig
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }
}
}
