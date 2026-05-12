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

        [JsonPropertyName("dataSampleQuery")]
        public DataSampleQuery DataSampleQuery { get; set; }

        [JsonPropertyName("ruleDefinitions")]
        public List<RuleDefinition> RuleDefinitions { get; set; } = new();

        [JsonPropertyName("remediation")]
        public RemediationInfo Remediation { get; set; }

        // Mappatura per i risultati storici (se inclusi in altre chiamate)
        [JsonPropertyName("lastCheckResultsByResource")]
        public Dictionary<string, LastCheckResult> LastCheckResultsByResource { get; set; } = new();

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
        // Corretto: l'API restituisce "inputQueries", non "metricQueries"
        [JsonPropertyName("inputQueries")]
        public List<InputQuery> InputQueries { get; set; } = new();
    }

    public class InputQuery
    {
        [JsonPropertyName("checkQueryType")]
        public string CheckQueryType { get; set; }

        [JsonPropertyName("itemsTimeRange")]
        public string? ItemsTimeRange { get; set; }

        [JsonPropertyName("itemsCount")]
        public int? ItemsCount { get; set; }

        [JsonPropertyName("inputName")]
        public string InputName { get; set; }

        [JsonPropertyName("projectedInputName")]
        public string ProjectedInputName { get; set; }
    }

    public class RuleDefinition
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        // Aggiunto perché presente nel JSON per alcune regole
        [JsonPropertyName("templateLocalizedMessages")]
        public Dictionary<string, string>? TemplateLocalizedMessages { get; set; }

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

        [JsonPropertyName("inputs")]
        public Dictionary<string, object> Inputs { get; set; }
    }

    public class RemediationInfo
    {
        [JsonPropertyName("summary")]
        public string? Summary { get; set; }

        [JsonPropertyName("suggestedAction")]
        public string? SuggestedAction { get; set; }

        [JsonPropertyName("riskIfIgnored")]
        public string? RiskIfIgnored { get; set; }
    }
}