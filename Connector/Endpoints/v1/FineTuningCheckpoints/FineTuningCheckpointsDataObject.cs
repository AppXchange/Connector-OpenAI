namespace Connector.Endpoints.v1.FineTuningCheckpoints;

using System.Collections.Generic;
using Json.Schema.Generation;
using System;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.CacheWriter;

/// <summary>
/// Represents a fine-tuning checkpoint from OpenAI's API
/// </summary>
[PrimaryKey("id", nameof(Id))]
[Description("Represents a checkpoint from a fine-tuning job")]
public class FineTuningCheckpointsDataObject
{
    [JsonPropertyName("id")]
    [Description("The unique identifier for the checkpoint")]
    [Required]
    public required string Id { get; init; }

    [JsonPropertyName("object")]
    [Description("The object type, which is always 'fine_tuning.job.checkpoint'")]
    [Required]
    public required string Object { get; init; }

    [JsonPropertyName("created_at")]
    [Description("The Unix timestamp (in seconds) for when the checkpoint was created")]
    [Required]
    public required long CreatedAt { get; init; }

    [JsonPropertyName("fine_tuned_model_checkpoint")]
    [Description("The identifier of the fine-tuned model checkpoint")]
    [Required]
    public required string FineTunedModelCheckpoint { get; init; }

    [JsonPropertyName("metrics")]
    [Description("The metrics for the checkpoint")]
    [Required]
    public required CheckpointMetrics Metrics { get; init; }

    [JsonPropertyName("fine_tuning_job_id")]
    [Description("The ID of the fine-tuning job this checkpoint belongs to")]
    [Required]
    public required string FineTuningJobId { get; init; }

    [JsonPropertyName("step_number")]
    [Description("The step number of the checkpoint")]
    [Required]
    public required int StepNumber { get; init; }
}

public class CheckpointMetrics
{
    [JsonPropertyName("full_valid_loss")]
    [Description("The validation loss for the checkpoint")]
    [Required]
    public required double FullValidLoss { get; init; }

    [JsonPropertyName("full_valid_mean_token_accuracy")]
    [Description("The mean token accuracy for the checkpoint")]
    [Required]
    public required double FullValidMeanTokenAccuracy { get; init; }
}

public class FineTuningCheckpointsResponse
{
    [JsonPropertyName("object")]
    public string Object { get; set; } = string.Empty;

    [JsonPropertyName("data")]
    public List<FineTuningCheckpointsDataObject> Data { get; set; } = new();

    [JsonPropertyName("first_id")]
    public string FirstId { get; set; } = string.Empty;

    [JsonPropertyName("last_id")]
    public string LastId { get; set; } = string.Empty;

    [JsonPropertyName("has_more")]
    public bool HasMore { get; set; }
}