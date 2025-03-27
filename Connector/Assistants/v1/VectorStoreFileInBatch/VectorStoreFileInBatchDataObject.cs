namespace Connector.Assistants.v1.VectorStoreFileInBatch;

using Json.Schema.Generation;
using System;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.CacheWriter;

/// <summary>
/// Data object that represents a vector store file batch in the Xchange system.
/// </summary>
[PrimaryKey("id", nameof(Id))]
[Description("Represents a batch of files being processed for a vector store")]
public class VectorStoreFileInBatchDataObject
{
    [JsonPropertyName("id")]
    [Description("The identifier, which can be referenced in API endpoints")]
    [Required]
    public required string Id { get; init; }

    [JsonPropertyName("object")]
    [Description("The object type, which is always vector_store.file_batch")]
    [Required]
    public required string Object { get; init; }

    [JsonPropertyName("created_at")]
    [Description("The Unix timestamp (in seconds) for when the vector store files batch was created")]
    [Required]
    public required long CreatedAt { get; init; }

    [JsonPropertyName("vector_store_id")]
    [Description("The ID of the vector store that the files are attached to")]
    [Required]
    public required string VectorStoreId { get; init; }

    [JsonPropertyName("status")]
    [Description("The status of the vector store files batch, which can be either in_progress, completed, cancelled or failed")]
    [Required]
    public required string Status { get; init; }

    [JsonPropertyName("file_counts")]
    [Description("Counts of files in different states")]
    [Required]
    public required FileCounts FileCounts { get; init; }
}

public class FileCounts
{
    [JsonPropertyName("in_progress")]
    [Description("Number of files currently being processed")]
    [Required]
    public required int InProgress { get; init; }

    [JsonPropertyName("completed")]
    [Description("Number of files that have been successfully processed")]
    [Required]
    public required int Completed { get; init; }

    [JsonPropertyName("failed")]
    [Description("Number of files that failed during processing")]
    [Required]
    public required int Failed { get; init; }

    [JsonPropertyName("cancelled")]
    [Description("Number of files that were cancelled during processing")]
    [Required]
    public required int Cancelled { get; init; }

    [JsonPropertyName("total")]
    [Description("Total number of files in the batch")]
    [Required]
    public required int Total { get; init; }
}