namespace GlassBoard.Request.Add
{
    using SharedLibrary.Enum;

    using System.Text.Json.Serialization;

    public class AddCheckHttpRequest
    {
        public string? Name { get; set; }

        // Forza la serializzazione in stringa per gli Enum
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public CheckQueryTypes? CheckQueryType { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public CheckStatusTypes? DefaultResult { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public CheckCategoryTypes? Category { get; set; }

        // L'API potrebbe volere una stringa "HH:mm:ss" invece dell'oggetto TimeSpan
        public string? ItemsTimeRange { get; set; } 
        
        public int? ItemsCount { get; set; }

        // Se l'API vuole una stringa qui, dobbiamo cambiare il tipo o serializzarlo prima
        public AddDataSampleQueryHttpRequest? DataSampleQuery { get; set; } 

        public List<AddRuleDefinitionHttpRequest>? RuleDefinitions { get; set; } = new();

        public AddRemediationHttpRequest? Remediation { get; set; } = new();

        public Dictionary<string, string> TemplateLocalizedMessages { get; set; } = new();
    }

    public class AddRuleDefinitionHttpRequest
    {
        public string? Name { get; set; }

        public AddRuleConditionHttpRequest? Condition { get; set; } = new();

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public CheckStatusTypes? ResultsOnPass { get; set; }
    }

    public class AddDataSampleQueryHttpRequest
    {
        public List<AddMetricDataSampleQueryHttpRequest>? MetricQueries { get; set; } = new();
    }

    public class AddMetricDataSampleQueryHttpRequest
    {
        public string? MetricName { get; set; }
        public string? ProjectedMetricName { get; set; }
    }

    public class AddRuleConditionHttpRequest
    {
        public string? Expression { get; set; }
    }

    public class AddRemediationHttpRequest
    {
        public string? SuggestedAction { get; set; }
        public string? RiskIfIgnored { get; set; }
        public string? Summary { get; set; }
    }
}