using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GlassBoard.Response.Get
{
    // --- WRAPPER PER LE POLICY ---
    public class GetCheckPolicyHttpResponse
    {
        [JsonPropertyName("items")]
        public List<CheckPolicyModels> Items { get; set; } = new();
    }

    public class CheckPolicyModels
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("namespace")]
        public string Namespace { get; set; }

        [JsonPropertyName("checkIds")]
        public List<string> CheckIds { get; set; } = new();

        // Questa è la proprietà che causava l'errore: ora è tipizzata correttamente
        public List<CheckModel> RuleDetails { get; set; } = new();

        public string Category => Namespace ?? "Generale";
    }
}