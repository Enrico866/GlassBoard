using System;
using System.Collections.Generic;

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
        public string Id { get; set; } = string.Empty;
        public string Path { get; set; } = string.Empty;
        public string ProbeId { get; set; } = string.Empty;
        public string OrganizationId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;

        // Nuovi campi Hardware / OS richiesti dalle API
        public string SerialNumber { get; set; } = string.Empty;
        public string PartNumber { get; set; } = string.Empty;
        public string OsVersion { get; set; } = string.Empty;

        public List<string> Endpoints { get; set; } = new();
        public bool IsMonitored { get; set; }
        public List<string> Tags { get; set; } = new();
        public List<string> ResourceTypes { get; set; } = new();
        
        public List<string> CollectionProfileIds { get; set; } = new();
        public List<string> AutoAssignedCollectionProfileIds { get; set; } = new();
        public List<string> SecurityIds { get; set; } = new();
        public List<string>? CheckProfileIds { get; set; } = new();
        public List<string> AlertProfileIds { get; set; } = new(); // Aggiunto per coerenza con API

        public List<ResourceItem> Children { get; set; } = new();
        public List<ResourceAccess> ResourceAccesses { get; set; } = new();
        public List<ResourceAttribute>? ResourceAttributes { get; set; } = new();
        public List<string> AvailableMetricNames { get; set; } = new();

        public string SeverityType { get; set; } = string.Empty;
        public string LastMetricsTimestamp { get; set; } = string.Empty;

        // Campi per tracciare l'ultima modifica
        public DateTime ModifiedOn { get; set; }
        public string LastModifiedBy { get; set; } = string.Empty;
    }

    public class ResourceAttribute
    {
        public string Key { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
    }

    public class ResourceAccess
    {
        public List<ResourceInstrument> Instruments { get; set; } = new();
        public List<string> AccessScopes { get; set; } = new();
        public string Address { get; set; } = string.Empty;
    }

    public class ResourceInstrument
    {
        public string InstrumentType { get; set; } = string.Empty;
        public string? SnmpVersion { get; set; }
        public int? TimeoutSeconds { get; set; }
        public string? SecurityId { get; set; }
    }
}