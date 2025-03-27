namespace Connector.Assistants.v1.VectorStoreFileInBatch.Cancel;

using Json.Schema.Generation;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.Action;

/// <summary>
/// Action to cancel a vector store file batch.
/// </summary>
[Description("Cancels a vector store file batch, attempting to stop file processing as soon as possible")]
public class CancelVectorStoreFileInBatchAction : IStandardAction<CancelVectorStoreFileInBatchActionInput, CancelVectorStoreFileInBatchActionOutput>
{
    public CancelVectorStoreFileInBatchActionInput ActionInput { get; set; } = new()
    {
        VectorStoreId = string.Empty,
        BatchId = string.Empty
    };
    public CancelVectorStoreFileInBatchActionOutput ActionOutput { get; set; } = new()
    {
        Id = string.Empty,
        Object = "vector_store.file_batch",
        CreatedAt = 0,
        VectorStoreId = string.Empty,
        Status = string.Empty,
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

public class CancelVectorStoreFileInBatchActionInput
{
    [JsonPropertyName("vector_store_id")]
    [Description("The ID of the vector store that the file batch belongs to")]
    [Required]
    public required string VectorStoreId { get; init; }

    [JsonPropertyName("batch_id")]
    [Description("The ID of the file batch to cancel")]
    [Required]
    public required string BatchId { get; init; }
}

public class CancelVectorStoreFileInBatchActionOutput
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
