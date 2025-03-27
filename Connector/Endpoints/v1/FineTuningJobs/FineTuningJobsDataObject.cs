namespace Connector.Endpoints.v1.FineTuningJobs;

using Json.Schema.Generation;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.CacheWriter;

/// <summary>
/// Represents a fine-tuning job from OpenAI's API
/// </summary>
[PrimaryKey("id", nameof(Id))]
//[AlternateKey("alt-key-id", nameof(CompanyId), nameof(EquipmentNumber))]
[Description("Represents a fine-tuning job from OpenAI's API")]
public class FineTuningJobsDataObject
{
    [JsonPropertyName("id")]
    [Description("The unique identifier for the fine-tuning job")]
    [Required]
    public required string Id { get; init; }

    [JsonPropertyName("object")]
    [Description("The object type, which is always 'fine_tuning.job'")]
    [Required]
    public required string Object { get; init; }

    [JsonPropertyName("model")]
    [Description("The base model that is being fine-tuned")]
    [Required]
    public required string Model { get; init; }

    [JsonPropertyName("created_at")]
    [Description("The Unix timestamp (in seconds) for when the job was created")]
    [Required]
    public required long CreatedAt { get; init; }

    [JsonPropertyName("fine_tuned_model")]
    [Description("The name of the fine-tuned model that is being created")]
    public string? FineTunedModel { get; init; }

    [JsonPropertyName("organization_id")]
    [Description("The organization that owns the fine-tuning job")]
    [Required]
    public required string OrganizationId { get; init; }

    [JsonPropertyName("result_files")]
    [Description("The compiled results files for the fine-tuning job")]
    [Required]
    public required string[] ResultFiles { get; init; }

    [JsonPropertyName("status")]
    [Description("The current status of the fine-tuning job")]
    [Required]
    public required string Status { get; init; }

    [JsonPropertyName("validation_file")]
    [Description("The validation file used for the fine-tuning job")]
    public string? ValidationFile { get; init; }

    [JsonPropertyName("training_file")]
    [Description("The training file used for the fine-tuning job")]
    [Required]
    public required string TrainingFile { get; init; }

    [JsonPropertyName("metadata")]
    [Description("Optional metadata associated with the fine-tuning job")]
    public Dictionary<string, string>? Metadata { get; init; }
}

public class FineTuningJobsResponse
{
    [JsonPropertyName("object")]
    public string Object { get; set; } = string.Empty;

    [JsonPropertyName("data")]
    public List<FineTuningJobsDataObject> Data { get; set; } = new();

    [JsonPropertyName("has_more")]
    public bool HasMore { get; set; }
}