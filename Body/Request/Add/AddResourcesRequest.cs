using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GlassBoard.Request.Add
{
    public class AddResourceHttpRequest
    {
        [JsonPropertyName("name")] public string Name { get; set; } = string.Empty;
        [JsonPropertyName("description")] public string Description { get; set; } = string.Empty;
        [JsonPropertyName("probeId")] public string ProbeId { get; set; } = string.Empty;
        [JsonPropertyName("parentId")] public string? ParentId { get; set; }
        [JsonPropertyName("notes")] public string? Notes { get; set; }
        [JsonPropertyName("isObserved")] public bool IsObserved { get; set; }

        [JsonPropertyName("resourceTypes")] public List<string> ResourceTypes { get; set; } = new();
        [JsonPropertyName("checkProfileIds")] public List<string> CheckProfileIds { get; set; } = new();
        [JsonPropertyName("collectionProfileIds")] public List<string> CollectionProfileIds { get; set; } = new();
        [JsonPropertyName("resourceAccesses")] public List<ResourceAccessItem> ResourceAccesses { get; set; } = new();
        [JsonPropertyName("instrumentConfigurations")] public List<InstrumentConfigItem> InstrumentConfigurations { get; set; } = new();
        [JsonPropertyName("resourceAttributes")] public List<ResourceAttributeItem> ResourceAttributes { get; set; } = new();
    }

    public class ResourceAccessItem
    {
        [JsonPropertyName("address")] public string Address { get; set; } = string.Empty;
        [JsonPropertyName("accessScopes")] public List<string> AccessScopes { get; set; } = new();

        // ALLINEAMENTO GET: Lista di oggetti complessi
        [JsonPropertyName("instruments")] public List<ResourceInstrumentItem> Instruments { get; set; } = new();
    }

    public class ResourceInstrumentItem
    {
        [JsonPropertyName("instrumentType")] public string InstrumentType { get; set; } = string.Empty;
        [JsonPropertyName("snmpVersion")] public string? SnmpVersion { get; set; }
        [JsonPropertyName("timeoutSeconds")] public int? TimeoutSeconds { get; set; }
        [JsonPropertyName("shouldLock")] public bool ShouldLock { get; set; }
    }

    public class InstrumentConfigItem
    {
        [JsonPropertyName("instrument")] public string Instrument { get; set; } = string.Empty;
        [JsonPropertyName("snmpVersion")] public string SnmpVersion { get; set; } = "V2c";
        [JsonPropertyName("port")] public int Port { get; set; }
        [JsonPropertyName("connTimeoutSeconds")] public int ConnTimeoutSeconds { get; set; } = 5;
        [JsonPropertyName("shouldLock")] public bool ShouldLock { get; set; }
    }

    public class ResourceAttributeItem
    {
        [JsonPropertyName("key")] public string Key { get; set; } = string.Empty;
        [JsonPropertyName("value")] public string Value { get; set; } = string.Empty;
    }
}