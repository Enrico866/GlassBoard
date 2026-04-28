using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GlassBoard.Response.Get
{
    public class GetCheckHttpResponse
    {
        [JsonPropertyName("items")]
        public List<CheckModel> Items { get; set; } = new();
    }

    public class CheckModel
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("organizationId")]
        public string OrganizationId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("templateLocalizedMessages")]
        public Dictionary<string, string> TemplateLocalizedMessages { get; set; }

        [JsonPropertyName("category")]
        public string Category { get; set; }

        [JsonPropertyName("defaultResult")]
        public string DefaultResult { get; set; }

        [JsonPropertyName("checkQueryType")]
        public string CheckQueryType { get; set; }

        [JsonPropertyName("itemsTimeRange")]
        public string ItemsTimeRange { get; set; }

        [JsonPropertyName("itemsCount")]
        public int? ItemsCount { get; set; }

        [JsonPropertyName("dataSampleQuery")]
        public DataSampleQuery DataSampleQuery { get; set; }

        [JsonPropertyName("ruleDefinitions")]
        public List<RuleDefinition> RuleDefinitions { get; set; } = new();

        // Mappatura per i risultati storici per risorsa
        [JsonPropertyName("lastCheckResultsByResource")]
        public Dictionary<string, LastCheckResult> LastCheckResultsByResource { get; set; } = new();

        [JsonPropertyName("remediation")]
        public RemediationInfo Remediation { get; set; }

        [JsonPropertyName("createdOn")]
        public DateTime CreatedOn { get; set; }

        [JsonPropertyName("modifiedOn")]
        public DateTime ModifiedOn { get; set; }

        [JsonPropertyName("createdBy")]
        public string CreatedBy { get; set; }

        [JsonPropertyName("modifiedBy")]
        public string ModifiedBy { get; set; }
    }

    public class DataSampleQuery
    {
        [JsonPropertyName("metricQueries")]
        public List<MetricQuery> MetricQueries { get; set; } = new();
    }

    public class MetricQuery
    {
        [JsonPropertyName("metricName")]
        public string MetricName { get; set; }

        [JsonPropertyName("projectedMetricName")]
        public string ProjectedMetricName { get; set; }
    }

    public class RuleDefinition
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("condition")]
        public RuleCondition Condition { get; set; }

        [JsonPropertyName("resultsOnPass")]
        public string ResultsOnPass { get; set; }
    }

    public class RuleCondition
    {
        [JsonPropertyName("expression")]
        public string Expression { get; set; }
    }

    public class LastCheckResult
    {
        [JsonPropertyName("checkResultId")]
        public string CheckResultId { get; set; }

        [JsonPropertyName("isSuccess")]
        public bool IsSuccess { get; set; }

        [JsonPropertyName("result")]
        public string Result { get; set; }

        [JsonPropertyName("executedOn")]
        public DateTime ExecutedOn { get; set; }

        [JsonPropertyName("resourceId")]
        public string ResourceId { get; set; }

        [JsonPropertyName("metadata")]
        public Dictionary<string, object> Metadata { get; set; }

        // Usiamo object perché nel tuo JSON "inputs" contiene sia valori semplici (1, 2) 
        // che oggetti complessi (avg, min, max...)
        [JsonPropertyName("inputs")]
        public Dictionary<string, object> Inputs { get; set; }
    }

    public class RemediationInfo
    {
        [JsonPropertyName("summary")]
        public string Summary { get; set; }

        [JsonPropertyName("suggestedAction")]
        public string SuggestedAction { get; set; }

        [JsonPropertyName("riskIfIgnored")]
        public string RiskIfIgnored { get; set; }
    }
}