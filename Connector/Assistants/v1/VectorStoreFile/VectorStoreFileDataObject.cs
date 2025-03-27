namespace Connector.Assistants.v1.VectorStoreFile;

using Json.Schema.Generation;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.CacheWriter;

/// <summary>
/// Data object that represents a vector store file in the Xchange system.
/// </summary>
[PrimaryKey("id", nameof(Id))]
[Description("Represents a file attached to a vector store")]
public class VectorStoreFileDataObject
{
    [JsonPropertyName("id")]
    [Description("The identifier, which can be referenced in API endpoints")]
    [Required]
    public required string Id { get; init; }

    [JsonPropertyName("object")]
    [Description("The object type, which is always vector_store.file")]
    [Required]
    public required string Object { get; init; }

    [JsonPropertyName("usage_bytes")]
    [Description("The total vector store usage in bytes")]
    [Required]
    public required long UsageBytes { get; init; }

    [JsonPropertyName("created_at")]
    [Description("The Unix timestamp (in seconds) for when the vector store file was created")]
    [Required]
    public required long CreatedAt { get; init; }

    [JsonPropertyName("vector_store_id")]
    [Description("The ID of the vector store that the File is attached to")]
    [Required]
    public required string VectorStoreId { get; init; }

    [JsonPropertyName("status")]
    [Description("The status of the vector store file, which can be either in_progress, completed, cancelled, or failed")]
    [Required]
    public required string Status { get; init; }

    [JsonPropertyName("last_error")]
    [Description("The last error associated with this vector store file")]
    public LastError? LastError { get; init; }

    [JsonPropertyName("chunking_strategy")]
    [Description("The strategy used to chunk the file")]
    [Required]
    public required ChunkingStrategy ChunkingStrategy { get; init; }

    [JsonPropertyName("attributes")]
    [Description("Set of 16 key-value pairs that can be attached to an object")]
    public Dictionary<string, object>? Attributes { get; init; }
}

public class LastError
{
    [JsonPropertyName("code")]
    [Description("The error code")]
    [Required]
    public required string Code { get; init; }

    [JsonPropertyName("message")]
    [Description("The error message")]
    [Required]
    public required string Message { get; init; }
}

public class ChunkingStrategy
{
    [JsonPropertyName("type")]
    [Description("The type of chunking strategy")]
    [Required]
    public required string Type { get; init; }

    [JsonPropertyName("static")]
    [Description("The static chunking strategy configuration")]
    public StaticChunkingConfig? Static { get; init; }
}

public class StaticChunkingConfig
{
    [JsonPropertyName("max_chunk_size_tokens")]
    [Description("Maximum size of each chunk in tokens")]
    [Required]
    public required int MaxChunkSizeTokens { get; init; }

    [JsonPropertyName("chunk_overlap_tokens")]
    [Description("Number of tokens to overlap between chunks")]
    [Required]
    public required int ChunkOverlapTokens { get; init; }
}