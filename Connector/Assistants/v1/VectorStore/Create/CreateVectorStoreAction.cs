namespace Connector.Assistants.v1.VectorStore.Create;

using Json.Schema.Generation;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.Action;

/// <summary>
/// Action to create a vector store.
/// </summary>
[Description("Creates a vector store that can be used by tools like file_search")]
public class CreateVectorStoreAction : IStandardAction<CreateVectorStoreActionInput, CreateVectorStoreActionOutput>
{
    public CreateVectorStoreActionInput ActionInput { get; set; } = new();
    public CreateVectorStoreActionOutput ActionOutput { get; set; } = new()
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

public class CreateVectorStoreActionInput
{
    [JsonPropertyName("name")]
    [Description("The name of the vector store")]
    public string? Name { get; init; }

    [JsonPropertyName("file_ids")]
    [Description("A list of File IDs that the vector store should use")]
    public string[]? FileIds { get; init; }

    [JsonPropertyName("metadata")]
    [Description("Set of 16 key-value pairs that can be attached to an object")]
    public Dictionary<string, string>? Metadata { get; init; }

    [JsonPropertyName("expires_after")]
    [Description("The expiration policy for a vector store")]
    public ExpiresAfter? ExpiresAfter { get; init; }

    [JsonPropertyName("chunking_strategy")]
    [Description("The chunking strategy used to chunk the files")]
    public ChunkingStrategy? ChunkingStrategy { get; init; }
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

public class CreateVectorStoreActionOutput
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
