namespace Connector.Assistants.v1.ListVectorStores;

using Json.Schema.Generation;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.CacheWriter;

/// <summary>
/// Data object that represents a list of vector stores.
/// </summary>
[PrimaryKey("first_id", nameof(FirstId))]
[Description("Represents a list of vector stores")]
public class ListVectorStoresDataObject
{
    [JsonPropertyName("object")]
    [Description("The object type, which is always list")]
    [Required]
    public required string Object { get; init; }

    [JsonPropertyName("data")]
    [Description("The list of vector stores")]
    [Required]
    public required VectorStoreData[] Data { get; init; }

    [JsonPropertyName("first_id")]
    [Description("The ID of the first vector store in the list")]
    [Required]
    public required string FirstId { get; init; }

    [JsonPropertyName("last_id")]
    [Description("The ID of the last vector store in the list")]
    [Required]
    public required string LastId { get; init; }

    [JsonPropertyName("has_more")]
    [Description("Whether there are more vector stores to retrieve")]
    [Required]
    public required bool HasMore { get; init; }
}

public class VectorStoreData
{
    [JsonPropertyName("id")]
    [Description("The identifier of the vector store")]
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

    [JsonPropertyName("name")]
    [Description("The name of the vector store")]
    [Required]
    public required string Name { get; init; }

    [JsonPropertyName("bytes")]
    [Description("The total number of bytes used by the files in the vector store")]
    [Required]
    public required long UsageBytes { get; init; }

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