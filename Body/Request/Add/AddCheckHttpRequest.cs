namespace GlassBoard.Request.Add
{
    using SharedLibrary.Enum;
    using System;
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    public class AddCheckHttpRequest
    {
        public string? Name { get; set; }

        public Dictionary<string, string> TemplateLocalizedMessages { get; set; } = new();

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public CheckStatusTypes? DefaultResult { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public CheckCategoryTypes? Category { get; set; }

        public AddDataSampleQueryHttpRequest? DataSampleQuery { get; set; }

        // L'API si aspetta un array di regole
        public AddRuleDefinitionHttpRequest[]? RuleDefinitions { get; set; }

        public AddRemediationHttpRequest? Remediation { get; set; } = new();
    }

    public class AddRuleDefinitionHttpRequest
    {
        public string? Name { get; set; }

        public Dictionary<string, string>? TemplateLocalizedMessages { get; set; }

        public AddRuleConditionHttpRequest? Condition { get; set; } = new();

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public CheckStatusTypes? ResultsOnPass { get; set; }
    }

    public class AddDataSampleQueryHttpRequest
    {
        // Sostituito MetricQueries con InputQueries come da specifica API
        public AddInputDataSampleQueryHttpRequest[]? InputQueries { get; set; }
    }

    public class AddInputDataSampleQueryHttpRequest
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public CheckQueryTypes? CheckQueryType { get; set; }

        // Usiamo TimeSpan? direttamente, System.Text.Json lo serializza come stringa ISO 8601
        public TimeSpan? ItemsTimeRange { get; set; }

        public int? ItemsCount { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public InputTypes InputType { get; set; }

        public string? InputName { get; set; }

        public string? ProjectedInputName { get; set; }
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