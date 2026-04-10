using SharedLibrary.Models;

using System.Text.Json.Serialization;

namespace GlassBoard.Response.Get
{
    public class GetSchedulingPoliciesResponse
    {
        [JsonPropertyName("items")]
        public List<SchedulingPolicy> Items { get; set; } = new();
    }
}
