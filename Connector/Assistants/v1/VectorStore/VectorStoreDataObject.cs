namespace Connector.Assistants.v1.VectorStore;

using Json.Schema.Generation;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.CacheWriter;

/// <summary>
/// Data object that represents a vector store in the Xchange system.
/// </summary>
[PrimaryKey("id", nameof(Id))]
[Description("Represents a vector store that contains processed files for use by the file_search tool")]
public class VectorStoreDataObject
{
    [JsonPropertyName("id")]
    [Description("The identifier, which can be referenced in API endpoints")]
    [Required]
    public required string Id { get; init; }

    [JsonPropertyName("object")]
    [Description("The object type, which is always vector_store")]
    [Required]
    public required string Object { get; init; }

    [JsonPropertyName("created_at")]
    [Description("The Unix timestamp (in seconds) for when the vector store was created")]
    [Required]
    public required long CreatedAt { get; init; }

    [JsonPropertyName("usage_bytes")]
    [Description("The total number of bytes used by the files in the vector store")]
    [Required]
    public required long UsageBytes { get; init; }

    [JsonPropertyName("last_active_at")]
    [Description("The Unix timestamp (in seconds) for when the vector store was last active")]
    public long? LastActiveAt { get; init; }

    [JsonPropertyName("name")]
    [Description("The name of the vector store")]
    [Required]
    public required string Name { get; init; }

    [JsonPropertyName("status")]
    [Description("The status of the vector store, which can be either expired, in_progress, or completed")]
    [Required]
    public required string Status { get; init; }

    [JsonPropertyName("file_counts")]
    [Description("Counts of files in different states")]
    [Required]
    public required FileCounts FileCounts { get; init; }

    [JsonPropertyName("expires_at")]
    [Description("The Unix timestamp (in seconds) for when the vector store will expire")]
    public long? ExpiresAt { get; init; }

    [JsonPropertyName("expires_after")]
    [Description("The expiration policy for a vector store")]
    public ExpiresAfter? ExpiresAfter { get; init; }

    [JsonPropertyName("metadata")]
    [Description("Set of 16 key-value pairs that can be attached to an object")]
    public Dictionary<string, string>? Metadata { get; init; }
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

    [JsonPropertyName("cancelled")]
    [Description("Number of files that were cancelled during processing")]
    [Required]
    public required int Cancelled { get; init; }

    [JsonPropertyName("failed")]
    [Description("Number of files that failed during processing")]
    [Required]
    public required int Failed { get; init; }

    [JsonPropertyName("total")]
    [Description("Total number of files in the vector store")]
    [Required]
    public required int Total { get; init; }
}

public class ExpiresAfter
{
    [JsonPropertyName("days")]
    [Description("Number of days after which the vector store will expire")]
    public int? Days { get; init; }

    [JsonPropertyName("months")]
    [Description("Number of months after which the vector store will expire")]
    public int? Months { get; init; }
}