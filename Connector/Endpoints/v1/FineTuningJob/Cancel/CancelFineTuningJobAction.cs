namespace Connector.Endpoints.v1.FineTuningJob.Cancel;

using Json.Schema.Generation;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.Action;

/// <summary>
/// Action for canceling a fine-tuning job using OpenAI's API
/// </summary>
[Description("Cancels a fine-tuning job that is currently running")]
public class CancelFineTuningJobAction : IStandardAction<CancelFineTuningJobActionInput, CancelFineTuningJobActionOutput>
{
    public CancelFineTuningJobActionInput ActionInput { get; set; } = new() 
    { 
        FineTuningJobId = string.Empty
    };
    public CancelFineTuningJobActionOutput ActionOutput { get; set; } = new()
    {
        Id = string.Empty,
        Object = "fine_tuning.job",
        Model = string.Empty,
        CreatedAt = 0,
        OrganizationId = string.Empty,
        ResultFiles = Array.Empty<string>(),
        Status = string.Empty,
        TrainingFile = string.Empty
    };
    public StandardActionFailure ActionFailure { get; set; } = new();

    public bool CreateRtap => true;
}

public class CancelFineTuningJobActionInput
{
    [JsonPropertyName("fine_tuning_job_id")]
    [Description("The ID of the fine-tuning job to cancel")]
    [Required]
    public required string FineTuningJobId { get; init; }
}

public class CancelFineTuningJobActionOutput
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
}
