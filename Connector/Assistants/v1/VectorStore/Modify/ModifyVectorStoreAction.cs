namespace Connector.Assistants.v1.VectorStore.Modify;

using Json.Schema.Generation;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.Action;

/// <summary>
/// Action to modify a vector store.
/// </summary>
[Description("Modifies a vector store")]
public class ModifyVectorStoreAction : IStandardAction<ModifyVectorStoreActionInput, ModifyVectorStoreActionOutput>
{
    public ModifyVectorStoreActionInput ActionInput { get; set; } = new() 
    { 
        VectorStoreId = string.Empty
    };
    public ModifyVectorStoreActionOutput ActionOutput { get; set; } = new() 
    { 
        Id = string.Empty,
        Object = "vector_store",
        CreatedAt = 0,
        Name = string.Empty,
        UsageBytes = 0,
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

public class ModifyVectorStoreActionInput
{
    [JsonPropertyName("vector_store_id")]
    [Description("The ID of the vector store to modify")]
    [Required]
    public required string VectorStoreId { get; init; }

    [JsonPropertyName("name")]
    [Description("The name of the vector store")]
    public string? Name { get; init; }

    [JsonPropertyName("metadata")]
    [Description("Set of 16 key-value pairs that can be attached to an object")]
    public Dictionary<string, string>? Metadata { get; init; }

    [JsonPropertyName("expires_after")]
    [Description("The expiration policy for a vector store")]
    public ExpiresAfter? ExpiresAfter { get; init; }
}

public class ModifyVectorStoreActionOutput
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

    [JsonPropertyName("usage_bytes")]
    [Description("The total number of bytes used by the files in the vector store")]
    [Required]
    public required long UsageBytes { get; init; }

    [JsonPropertyName("file_counts")]
    [Description("Counts of files in different states")]
    [Required]
    public required FileCounts FileCounts { get; init; }
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
