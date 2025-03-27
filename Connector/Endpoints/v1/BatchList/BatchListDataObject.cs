namespace Connector.Endpoints.v1.BatchList;

using Json.Schema.Generation;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.CacheWriter;

/// <summary>
/// Represents a batch operation from OpenAI's API
/// </summary>
[PrimaryKey("id", nameof(Id))]
[Description("Represents a batch operation from OpenAI's API")]
public class BatchListDataObject
{
    [JsonPropertyName("id")]
    [Description("The unique identifier for the batch")]
    [Required]
    public required string Id { get; init; }

    [JsonPropertyName("object")]
    [Description("The type of this object, which is always 'batch'")]
    [Required]
    public required string Object { get; init; }

    [JsonPropertyName("endpoint")]
    [Description("The OpenAI API endpoint used by the batch")]
    [Required]
    public required string Endpoint { get; init; }

    [JsonPropertyName("errors")]
    [Description("Object containing any errors that occurred during batch processing")]
    public object? Errors { get; init; }

    [JsonPropertyName("input_file_id")]
    [Description("The ID of the input file for the batch")]
    [Required]
    public required string InputFileId { get; init; }

    [JsonPropertyName("completion_window")]
    [Description("The time frame within which the batch should be processed")]
    [Required]
    public required string CompletionWindow { get; init; }

    [JsonPropertyName("status")]
    [Description("The current status of the batch")]
    [Required]
    public required string Status { get; init; }

    [JsonPropertyName("output_file_id")]
    [Description("The ID of the file containing the outputs of successfully executed requests")]
    public string? OutputFileId { get; init; }

    [JsonPropertyName("error_file_id")]
    [Description("The ID of the file containing the outputs of requests with errors")]
    public string? ErrorFileId { get; init; }

    [JsonPropertyName("created_at")]
    [Description("The Unix timestamp (in seconds) for when the batch was created")]
    [Required]
    public required long CreatedAt { get; init; }

    [JsonPropertyName("in_progress_at")]
    [Description("The Unix timestamp (in seconds) for when the batch started processing")]
    public long? InProgressAt { get; init; }

    [JsonPropertyName("expires_at")]
    [Description("The Unix timestamp (in seconds) for when the batch will expire")]
    [Required]
    public required long ExpiresAt { get; init; }

    [JsonPropertyName("finalizing_at")]
    [Description("The Unix timestamp (in seconds) for when the batch started finalizing")]
    public long? FinalizingAt { get; init; }

    [JsonPropertyName("completed_at")]
    [Description("The Unix timestamp (in seconds) for when the batch was completed")]
    public long? CompletedAt { get; init; }

    [JsonPropertyName("failed_at")]
    [Description("The Unix timestamp (in seconds) for when the batch failed")]
    public long? FailedAt { get; init; }

    [JsonPropertyName("expired_at")]
    [Description("The Unix timestamp (in seconds) for when the batch expired")]
    public long? ExpiredAt { get; init; }

    [JsonPropertyName("cancelling_at")]
    [Description("The Unix timestamp (in seconds) for when the batch started cancelling")]
    public long? CancellingAt { get; init; }

    [JsonPropertyName("cancelled_at")]
    [Description("The Unix timestamp (in seconds) for when the batch was cancelled")]
    public long? CancelledAt { get; init; }

    [JsonPropertyName("request_counts")]
    [Description("The request counts for different statuses within the batch")]
    [Required]
    public required RequestCounts RequestCounts { get; init; }

    [JsonPropertyName("metadata")]
    [Description("Set of key-value pairs that can be attached to an object")]
    public Dictionary<string, string>? Metadata { get; init; }
}

public class RequestCounts
{
    [JsonPropertyName("total")]
    [Description("Total number of requests in the batch")]
    [Required]
    public required int Total { get; init; }

    [JsonPropertyName("completed")]
    [Description("Number of completed requests")]
    [Required]
    public required int Completed { get; init; }

    [JsonPropertyName("failed")]
    [Description("Number of failed requests")]
    [Required]
    public required int Failed { get; init; }
}

/// <summary>
/// Represents the response from the batch list API endpoint
/// </summary>
public class BatchListResponse
{
    [JsonPropertyName("object")]
    public string Object { get; set; } = string.Empty;

    [JsonPropertyName("data")]
    public List<BatchListDataObject> Data { get; set; } = new();

    [JsonPropertyName("first_id")]
    public string FirstId { get; set; } = string.Empty;

    [JsonPropertyName("last_id")]
    public string LastId { get; set; } = string.Empty;

    [JsonPropertyName("has_more")]
    public bool HasMore { get; set; }
}