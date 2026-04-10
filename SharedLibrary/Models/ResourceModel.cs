using System.Text.Json.Serialization;

namespace SharedLibrary.Models
{
    public class ResourceModel
    {
        public int Offset { get; set; }
        public int Limit { get; set; }
        public int TotalCount { get; set; }
        public List<ResourceItem> Items { get; set; } = new();
    }

    public class ResourceItem
{
    public string Id { get; set; }

    public string Path { get; set; }

    public string ProbeId { get; set; }

    public string OrganizationId { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public string Notes { get; set; }

    public List<string> Endpoints { get; set; } = new();

    public bool IsMonitored { get; set; }

    public List<string> Tags { get; set; } = new();

    public List<string> ResourceTypes { get; set; } = new();

    public List<string> CollectionProfileIds { get; set; } = new();

    public List<string> AutoAssignedCollectionProfileIds { get; set; } = new();

    public List<string> SecurityIds { get; set; } = new();

    public List<ResourceItem> Children { get; set; } = new();

    public List<string>? CheckProfileIds { get; set; } = new();

    public List<ResourceAccess> ResourceAccesses { get; set; } = new();

    public List<ResourceAttribute>? ResourceAttributes { get; set; } = new();

    public List<string> AvailableMetricNames { get; set; } = new();

    public string SeverityType { get; set; }

    public string LastMetricsTimestamp { get; set; }
}

    public class ResourceAttribute
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }

    public class ResourceAccess
    {
        public List<string> Instruments { get; set; } = new();
        public List<string> AccessScopes { get; set; } = new();
        public string Address { get; set; }
    }
}