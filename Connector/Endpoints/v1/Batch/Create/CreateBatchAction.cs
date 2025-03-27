namespace Connector.Endpoints.v1.Batch.Create;

using Json.Schema.Generation;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.Action;

/// <summary>
/// Action for creating a batch operation in OpenAI's API
/// </summary>
[Description("Creates and executes a batch from an uploaded file of requests")]
public class CreateBatchAction : IStandardAction<CreateBatchActionInput, CreateBatchActionOutput>
{
    public CreateBatchActionInput ActionInput { get; set; } = new()
    {
        InputFileId = string.Empty,
        Endpoint = string.Empty,
        CompletionWindow = "24h"
    };
    public CreateBatchActionOutput ActionOutput { get; set; } = new()
    {
        Id = string.Empty,
        Object = "batch",
        Endpoint = string.Empty,
        InputFileId = string.Empty,
        CompletionWindow = "24h",
        Status = string.Empty,
        CreatedAt = 0,
        RequestCounts = new RequestCounts
        {
            Total = 0,
            Completed = 0,
            Failed = 0
        }
    };
    public StandardActionFailure ActionFailure { get; set; } = new();

    public bool CreateRtap => true;
}

public class CreateBatchActionInput
{
    [JsonPropertyName("input_file_id")]
    [Description("The ID of an uploaded file that contains requests for the new batch")]
    [Required]
    public required string InputFileId { get; set; }

    [JsonPropertyName("endpoint")]
    [Description("The endpoint to be used for all requests in the batch")]
    [Required]
    public required string Endpoint { get; set; }

    [JsonPropertyName("completion_window")]
    [Description("The time frame within which the batch should be processed")]
    [Required]
    public required string CompletionWindow { get; set; }

    [JsonPropertyName("metadata")]
    [Description("Set of key-value pairs that can be attached to an object")]
    public Dictionary<string, string>? Metadata { get; set; }
}

public class CreateBatchActionOutput
{
    [JsonPropertyName("id")]
    [Description("The unique identifier for the batch")]
    [Required]
    public required string Id { get; set; }

    [JsonPropertyName("object")]
    [Description("The type of this object, which is always 'batch'")]
    [Required]
    public required string Object { get; set; }

    [JsonPropertyName("endpoint")]
    [Description("The OpenAI API endpoint used by the batch")]
    [Required]
    public required string Endpoint { get; set; }

    [JsonPropertyName("errors")]
    [Description("Object containing any errors that occurred during batch processing")]
    public object? Errors { get; set; }

    [JsonPropertyName("input_file_id")]
    [Description("The ID of the input file for the batch")]
    [Required]
    public required string InputFileId { get; set; }

    [JsonPropertyName("completion_window")]
    [Description("The time frame within which the batch should be processed")]
    [Required]
    public required string CompletionWindow { get; set; }

    [JsonPropertyName("status")]
    [Description("The current status of the batch")]
    [Required]
    public required string Status { get; set; }

    [JsonPropertyName("output_file_id")]
    [Description("The ID of the file containing the outputs of successfully executed requests")]
    public string? OutputFileId { get; set; }

    [JsonPropertyName("error_file_id")]
    [Description("The ID of the file containing the outputs of requests with errors")]
    public string? ErrorFileId { get; set; }

    [JsonPropertyName("created_at")]
    [Description("The Unix timestamp (in seconds) for when the batch was created")]
    [Required]
    public required long CreatedAt { get; set; }

    [JsonPropertyName("in_progress_at")]
    [Description("The Unix timestamp (in seconds) for when the batch started processing")]
    public long? InProgressAt { get; set; }

    [JsonPropertyName("expires_at")]
    [Description("The Unix timestamp (in seconds) for when the batch will expire")]
    public long? ExpiresAt { get; set; }

    [JsonPropertyName("finalizing_at")]
    [Description("The Unix timestamp (in seconds) for when the batch started finalizing")]
    public long? FinalizingAt { get; set; }

    [JsonPropertyName("completed_at")]
    [Description("The Unix timestamp (in seconds) for when the batch was completed")]
    public long? CompletedAt { get; set; }

    [JsonPropertyName("failed_at")]
    [Description("The Unix timestamp (in seconds) for when the batch failed")]
    public long? FailedAt { get; set; }

    [JsonPropertyName("expired_at")]
    [Description("The Unix timestamp (in seconds) for when the batch expired")]
    public long? ExpiredAt { get; set; }

    [JsonPropertyName("cancelling_at")]
    [Description("The Unix timestamp (in seconds) for when the batch started cancelling")]
    public long? CancellingAt { get; set; }

    [JsonPropertyName("cancelled_at")]
    [Description("The Unix timestamp (in seconds) for when the batch was cancelled")]
    public long? CancelledAt { get; set; }

    [JsonPropertyName("request_counts")]
    [Description("The request counts for different statuses within the batch")]
    [Required]
    public required RequestCounts RequestCounts { get; set; }

    [JsonPropertyName("metadata")]
    [Description("Set of key-value pairs that can be attached to an object")]
    public Dictionary<string, string>? Metadata { get; set; }
}

public class RequestCounts
{
    [JsonPropertyName("total")]
    [Description("Total number of requests in the batch")]
    [Required]
    public required int Total { get; set; }

    [JsonPropertyName("completed")]
    [Description("Number of completed requests")]
    [Required]
    public required int Completed { get; set; }

    [JsonPropertyName("failed")]
    [Description("Number of failed requests")]
    [Required]
    public required int Failed { get; set; }
}
