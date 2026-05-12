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
    [JsonPropertyName("id")] public string Id { get; set; } = string.Empty;
    [JsonPropertyName("name")] public string Name { get; set; } = string.Empty;
    [JsonPropertyName("path")] public string Path { get; set; } = string.Empty;
    [JsonPropertyName("description")] public string Description { get; set; } = string.Empty;
    
    // Mappato su isObserved nel JSON
    [JsonPropertyName("isObserved")] public bool IsMonitored { get; set; }
    
    [JsonPropertyName("probeId")] public string ProbeId { get; set; } = string.Empty;
    [JsonPropertyName("organizationId")] public string OrganizationId { get; set; } = string.Empty;
    
    [JsonPropertyName("severityType")] public string SeverityType { get; set; } = string.Empty;
    [JsonPropertyName("serialNumber")] public string SerialNumber { get; set; } = string.Empty;
    [JsonPropertyName("partNumber")] public string PartNumber { get; set; } = string.Empty;
    [JsonPropertyName("os")] public string Os { get; set; } = string.Empty;

    [JsonPropertyName("attributes")] 
    public List<ResourceAttributeDto> Attributes { get; set; } = new();
    
    [JsonPropertyName("accesses")] 
    public List<ResourceAccessDto> Accesses { get; set; } = new();

    [JsonPropertyName("tags")] 
    public List<string> Tags { get; set; } = new();

    [JsonPropertyName("resourceTypes")] 
    public List<string> ResourceTypes { get; set; } = new();

    [JsonPropertyName("collectionProfileIds")]
    public List<string> CollectionProfileIds { get; set; } = new();

    // RISOLVE ERRORE: AutoAssignedCollectionProfileIds
    [JsonPropertyName("autoAssignedCollectionProfileIds")]
    public List<string> AutoAssignedCollectionProfileIds { get; set; } = new();

    [JsonPropertyName("checkProfileIds")]
    public List<string>? CheckProfileIds { get; set; } = new();

    [JsonPropertyName("alertProfileIds")]
    public List<string> AlertProfileIds { get; set; } = new();

    // RISOLVE ERRORE: SecurityIds
    [JsonPropertyName("securityIds")]
    public List<string> SecurityIds { get; set; } = new();

    // RISOLVE ERRORE: Conversione DateTime? a string
    [JsonPropertyName("lastMetricsTimestamp")]
    public string LastMetricsTimestamp { get; set; } = string.Empty;

    [JsonPropertyName("availableMetricNames")]
    public List<string> AvailableMetricNames { get; set; } = new();

    [JsonPropertyName("modifiedOn")]
    public DateTime ModifiedOn { get; set; }

    [JsonPropertyName("modifier")]
    public ResourceModifierDto? Modifier { get; set; }
}

public class ResourceAttributeDto
{
    [JsonPropertyName("key")] public string Key { get; set; } = string.Empty;
    [JsonPropertyName("value")] public string Value { get; set; } = string.Empty;
}

public class ResourceAccessDto
{
    [JsonPropertyName("address")] 
    public string Address { get; set; } = string.Empty;

    [JsonPropertyName("accessScopes")] 
    public List<string> AccessScopes { get; set; } = new();

    // MODIFICA CHIAVE: Instruments ora è una lista di oggetti, non di stringhe
    [JsonPropertyName("instruments")] 
    public List<ResourceInstrumentDto> Instruments { get; set; } = new();
}

public class ResourceInstrumentDto
{
    [JsonPropertyName("instrumentType")] 
    public string InstrumentType { get; set; } = string.Empty;

    [JsonPropertyName("snmpVersion")] 
    public string? SnmpVersion { get; set; }

    [JsonPropertyName("securityId")] 
    public string? SecurityId { get; set; }

    [JsonPropertyName("shouldLock")] 
    public bool? ShouldLock { get; set; }

    [JsonPropertyName("timeoutSeconds")] 
    public int? TimeoutSeconds { get; set; }
}

public class ResourceModifierDto
{
    [JsonPropertyName("id")] public string Id { get; set; } = string.Empty;
    [JsonPropertyName("firstName")] public string FirstName { get; set; } = string.Empty;
    [JsonPropertyName("lastName")] public string LastName { get; set; } = string.Empty;
}