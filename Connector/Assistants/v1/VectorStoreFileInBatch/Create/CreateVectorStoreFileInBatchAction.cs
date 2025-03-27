namespace Connector.Assistants.v1.VectorStoreFileInBatch.Create;

using Json.Schema.Generation;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.Action;

/// <summary>
/// Action to create a vector store file batch.
/// </summary>
[Description("Creates a vector store file batch for processing multiple files")]
public class CreateVectorStoreFileInBatchAction : IStandardAction<CreateVectorStoreFileInBatchActionInput, CreateVectorStoreFileInBatchActionOutput>
{
    public CreateVectorStoreFileInBatchActionInput ActionInput { get; set; } = new()
    {
        VectorStoreId = string.Empty,
        FileIds = new string[] { },
        Attributes = null,
        ChunkingStrategy = null
    };
    public CreateVectorStoreFileInBatchActionOutput ActionOutput { get; set; } = new()
    {
        Id = string.Empty,
        Object = "vector_store.file_batch",
        CreatedAt = 0,
        VectorStoreId = string.Empty,
        Status = "in_progress",
        FileCounts = new FileCounts
        {
            InProgress = 0,
            Completed = 0,
            Failed = 0,
            Cancelled = 0,
            Total = 0
        }
    };
    public StandardActionFailure ActionFailure { get; set; } = new();

    public bool CreateRtap => true;
}

public class CreateVectorStoreFileInBatchActionInput
{
    [JsonPropertyName("vector_store_id")]
    [Description("The ID of the vector store for which to create a File Batch")]
    [Required]
    public required string VectorStoreId { get; init; }

    [JsonPropertyName("file_ids")]
    [Description("A list of File IDs that the vector store should use")]
    [Required]
    public required string[] FileIds { get; init; }

    [JsonPropertyName("attributes")]
    [Description("Set of key-value pairs that can be attached to an object")]
    public Dictionary<string, object>? Attributes { get; init; }

    [JsonPropertyName("chunking_strategy")]
    [Description("The chunking strategy used to chunk the files")]
    public ChunkingStrategy? ChunkingStrategy { get; init; }
}

public class CreateVectorStoreFileInBatchActionOutput
{
    [JsonPropertyName("id")]
    [Description("The identifier of the vector store file batch")]
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
    [Description("The status of the vector store files batch")]
    [Required]
    public required string Status { get; init; }

    [JsonPropertyName("file_counts")]
    [Description("Counts of files in different states")]
    [Required]
    public required FileCounts FileCounts { get; init; }
}

public class ChunkingStrategy
{
    [JsonPropertyName("type")]
    [Description("The type of chunking strategy")]
    [Required]
    public required string Type { get; init; }

    [JsonPropertyName("size")]
    [Description("The size of each chunk in tokens")]
    public int? Size { get; init; }

    [JsonPropertyName("overlap")]
    [Description("The number of tokens to overlap between chunks")]
    public int? Overlap { get; init; }
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
