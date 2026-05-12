using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GlassBoard.Models.Alerts
{
    public class GetAlertsHttpResponse
    {
        [JsonPropertyName("items")]
        public List<AlertModel> Items { get; set; } = new();
    }

    public class AlertModel
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("organizationId")]
        public string OrganizationId { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("category")]
        public string Category { get; set; }

        [JsonPropertyName("domain")]
        public string Domain { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("gravity")]
        public double Gravity { get; set; }

        [JsonPropertyName("isVisible")]
        public bool IsVisible { get; set; }

        [JsonPropertyName("createdOn")]
        public DateTime CreatedOn { get; set; }

        [JsonPropertyName("modifiedOn")]
        public DateTime ModifiedOn { get; set; }

        [JsonPropertyName("resource")]
        public ResourceInfo Resource { get; set; }

        [JsonPropertyName("rootResource")]
        public ResourceInfo RootResource { get; set; }

        [JsonPropertyName("metadata")]
        public Dictionary<string, string> Metadata { get; set; } = new();
    }

    public class ResourceInfo
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("resourceTypes")]
        public List<string> ResourceTypes { get; set; } = new();

        [JsonPropertyName("attributes")]
        public Dictionary<string, string> Attributes { get; set; } = new();
    }
}