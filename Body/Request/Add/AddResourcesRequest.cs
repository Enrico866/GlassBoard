using System.Collections.Generic;
using System.Text.Json.Serialization;

using static SharedLibrary.Enum.Enums;

namespace GlassBoard.Request.Add
{
    public class AddResourceHttpRequest
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("probeId")]
        public string ProbeId { get; set; }

        [JsonPropertyName("parentId")]
        public string? ParentId { get; set; } = null;

        [JsonPropertyName("notes")]
        public string? Notes { get; set; } = null;

        [JsonPropertyName("isObserved")]
        public bool IsObserved { get; set; } = false;

        [JsonPropertyName("resourceTypes")]
        public List<string> ResourceTypes { get; set; } = new();

        // 1. Aggiunto per gestire i Check Profiles selezionati
        [JsonPropertyName("checkProfileIds")]
        public List<string> CheckProfileIds { get; set; } = new();

        [JsonPropertyName("collectionProfileIds")]
        public List<string> CollectionProfileIds { get; set; } = new();

        [JsonPropertyName("resourceAccesses")]
        public List<ResourceAccessItem> ResourceAccesses { get; set; } = new();

        [JsonPropertyName("instrumentConfigurations")]
        public List<InstrumentConfigItem> InstrumentConfigurations { get; set; } = new();

        // 2. Aggiunto per gestire gli attributi (come observable-namespace)
        [JsonPropertyName("resourceAttributes")]
        public List<ResourceAttributeItem> ResourceAttributes { get; set; } = new();
    }

    public class ResourceAccessItem
    {
        [JsonPropertyName("address")]
        public string Address { get; set; }

        [JsonPropertyName("accessScopes")]
        public List<string> AccessScopes { get; set; } = new();

        [JsonPropertyName("instruments")]
        public List<string> Instruments { get; set; } = new();
    }

    public class InstrumentConfigItem
    {
        [JsonPropertyName("instrumentId")]
        public int InstrumentId { get; set; }

        [JsonPropertyName("instrument")]
        public string Instrument { get; set; }

        [JsonPropertyName("snmpVersion")]
        public string SnmpVersion { get; set; } = "V2c";

        [JsonPropertyName("port")]
        public int Port { get; set; }

        [JsonPropertyName("connTimeoutSeconds")]
        public int ConnTimeoutSeconds { get; set; } = 5;

        [JsonPropertyName("shouldLock")]
        public bool ShouldLock { get; set; }
    }

    // 3. Classe di supporto per gli attributi chiave-valore
    public class ResourceAttributeItem
    {
        [JsonPropertyName("key")]
        public string Key { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }
    }
}