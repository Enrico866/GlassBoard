using System.Text.Json.Serialization;

namespace GlassBoard.Response.Get;

public class GetResourceHttpResponse
{
    [JsonPropertyName("offset")]
    public int Offset { get; set; }
    [JsonPropertyName("limit")]
    public int Limit { get; set; }
    [JsonPropertyName("totalCount")]
    public int TotalCount { get; set; }
    [JsonPropertyName("items")]
    public List<ResourceApiDto> Items { get; set; } = new();
}

public class ResourceApiDto
{
    [JsonPropertyName("id")] public string Id { get; set; }
    [JsonPropertyName("path")] public string Path { get; set; }
    [JsonPropertyName("name")] public string Name { get; set; }
    [JsonPropertyName("probeId")] public string ProbeId { get; set; }
    [JsonPropertyName("organizationId")] public string OrganizationId { get; set; }
    [JsonPropertyName("description")] public string Description { get; set; }
    [JsonPropertyName("isObserved")] public bool IsMonitored { get; set; }
    [JsonPropertyName("severityType")] public string SeverityType { get; set; }
    
    // Mappatura nomi divergenti
    [JsonPropertyName("attributes")] 
    public List<ResourceAttributeDto> Attributes { get; set; } = new();
    
    [JsonPropertyName("accesses")] 
    public List<ResourceAccessDto> Accesses { get; set; } = new();

    [JsonPropertyName("tags")] 
    public List<string> Tags { get; set; } = new();

    [JsonPropertyName("resourceTypes")] 
    public List<string> ResourceTypes { get; set; } = new();

    [JsonPropertyName("checkProfileIds")]
    public List<string>? CheckProfileIds { get; set; } = new();

    [JsonPropertyName("collectionProfileIds")]
    public List<string> CollectionProfileIds { get; set; } = new();

    [JsonPropertyName("autoAssignedCollectionProfileIds")]
    public List<string> AutoAssignedCollectionProfileIds { get; set; } = new();

    [JsonPropertyName("securityIds")]
    public List<string> SecurityIds { get; set; } = new();

    [JsonPropertyName("lastMetricsTimestamp")]
    public string LastMetricsTimestamp { get; set; }

    [JsonPropertyName("availableMetricNames")]
    public List<string> AvailableMetricNames { get; set; } = new();
}

public class ResourceAttributeDto
{
    [JsonPropertyName("key")] public string Key { get; set; }
    [JsonPropertyName("value")] public string Value { get; set; }
}

public class ResourceAccessDto
{
    [JsonPropertyName("instruments")] public List<string> Instruments { get; set; } = new();
    [JsonPropertyName("accessScopes")] public List<string> AccessScopes { get; set; } = new();
    [JsonPropertyName("address")] public string Address { get; set; }
}