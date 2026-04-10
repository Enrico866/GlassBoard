using SharedLibrary.Models;

using System.Security.AccessControl;
using System.Text.Json.Serialization;

using static SharedLibrary.Enum.Enums;

namespace GlassBoard.Response.Get
{
    public class GetCollectionProfileHttpResponse
{
    [JsonPropertyName("offset")]
    public int Offset { get; set; }

    [JsonPropertyName("limit")]
    public int Limit { get; set; }

    [JsonPropertyName("totalCount")]
    public int TotalCount { get; set; }

    [JsonPropertyName("items")]
    public List<CollectionProfileDetailed> Items { get; set; } = new();
}

public class CollectionProfileDetailed
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("isDefault")]
    public bool IsDefault { get; set; }

    [JsonPropertyName("schedulingPolicyId")]
    public string SchedulingPolicyId { get; set; }

    /// <summary>
    /// Nel JSON arriva come stringa singola. 
    /// Il JsonStringEnumConverter gestirà la conversione verso l'Enum ResourceTypes.
    /// </summary>
    [JsonPropertyName("resourceTypes")]
    public ResourceTypes ResourceTypes { get; set; }

    /// <summary>
    /// ATTENZIONE: Nel JSON arrivano solo stringhe (ID), non oggetti complessi.
    /// </summary>
    [JsonPropertyName("observableConfigurationIds")]
    public List<string> ObservableConfigurationIds { get; set; } = new();

    [JsonPropertyName("tags")]
    public List<string> Tags { get; set; } = new();

    // Queste proprietà non sono presenti nel JSON fornito, 
    // le manteniamo come opzionali per non rompere la logica esistente
    [JsonPropertyName("schedulingPolicy")]
    public SchedulingPolicy Info { get; set; }
}

}