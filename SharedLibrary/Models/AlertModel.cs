using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SharedLibrary.Models
{
        /// <summary>
    /// Wrapper per la risposta dell'API degli Alerts
    /// </summary>
    public class GetAlertsHttpResponse
    {
        [JsonPropertyName("items")]
        public List<AlertModel> Items { get; set; } = new();
    }

    /// <summary>
    /// Modello che rappresenta il singolo Alert
    /// </summary>
    public class AlertModel
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("organizationId")]
        public string OrganizationId { get; set; } = string.Empty;

        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;

        [JsonPropertyName("category")]
        public string Category { get; set; } = string.Empty;

        [JsonPropertyName("domain")]
        public string Domain { get; set; } = string.Empty;

        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;

        [JsonPropertyName("gravity")]
        public double Gravity { get; set; }

        [JsonPropertyName("isVisible")]
        public bool IsVisible { get; set; }

        [JsonPropertyName("createdOn")]
        public DateTime CreatedOn { get; set; }

        [JsonPropertyName("modifiedOn")]
        public DateTime ModifiedOn { get; set; }

        [JsonPropertyName("modifiedBy")]
        public string? ModifiedBy { get; set; }

        [JsonPropertyName("metadata")]
        public Dictionary<string, string> Metadata { get; set; } = new();

        [JsonPropertyName("resource")]
        public ResourceDetail Resource { get; set; } = new();

        [JsonPropertyName("rootResource")]
        public ResourceDetail RootResource { get; set; } = new();
    }

    /// <summary>
    /// Dettaglio della risorsa (usato sia per resource che per rootResource)
    /// </summary>
    public class ResourceDetail
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;

        [JsonPropertyName("path")]
        public string Path { get; set; } = string.Empty;

        [JsonPropertyName("tags")]
        public List<string> Tags { get; set; } = new();

        [JsonPropertyName("addresses")]
        public List<string> Addresses { get; set; } = new();

        [JsonPropertyName("resourceTypes")]
        public List<string> ResourceTypes { get; set; } = new();

        [JsonPropertyName("attributes")]
        public Dictionary<string, string> Attributes { get; set; } = new();
    }
}
