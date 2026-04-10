using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SharedLibrary.Models
{
public class SchedulingPolicy
{
        [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;
        [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
    [JsonPropertyName("intervalInSeconds")]
    public int? IntervalInSeconds { get; set; }
        [JsonPropertyName("cron")]
    public string? Cron { get; set; }
}
}
