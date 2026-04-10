using System.Text.Json.Serialization;

namespace GlassBoard.Response.Get
{
public class GetObservableConfigurationResponse
{
    [JsonPropertyName("items")]
    public List<ObservableConfigurationDetailed> Items { get; set; } = new();
}

public class ObservableConfigurationDetailed
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    public string? ResourceId { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("instrument")]
    public string? Instrument { get; set; }

    [JsonPropertyName("scalarOidTemplate")]
    public string? ScalarOidTemplate { get; set; }

    [JsonPropertyName("tableOidTemplate")] // Aggiungi questo campo
    public string? TableOidTemplate { get; set; }

    [JsonPropertyName("details")]
    public List<ObservableDetail> Details { get; set; } = new();

    [JsonPropertyName("collector")]
    public CollectorInfo? Collector { get; set; }
}

public class ObservableDetail
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("resultType")]
    public string? ResultType { get; set; }
            
    // Campi per Metric
    [JsonPropertyName("metricKind")]
    public string MetricKind { get; set; } = "gauge"; // Valore predefinito "gauge"

    [JsonPropertyName("metricUnit")]
    public string? MetricUnit { get; set; }

    // Campo per StructuredData
    [JsonPropertyName("columnOrdinal")]
    public string? ColumnOrdinal { get; set; }

    [JsonPropertyName("conversion")]
    public string? Conversion { get; set; }

    [JsonPropertyName("datapointAttributes")]
    public List<string>? DatapointAttributes { get; set; }
}

public class CollectorInfo
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }
    [JsonPropertyName("description")]
    public string? Description { get; set; }
}
}
