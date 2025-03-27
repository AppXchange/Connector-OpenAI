namespace Connector.Assistants.v1.VectorStoreFile.Create;

using Json.Schema.Generation;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.Action;

/// <summary>
/// Action to create a vector store file by attaching a File to a vector store.
/// </summary>
[Description("Creates a vector store file by attaching a File to a vector store")]
public class CreateVectorStoreFileAction : IStandardAction<CreateVectorStoreFileActionInput, CreateVectorStoreFileActionOutput>
{
    public CreateVectorStoreFileActionInput ActionInput { get; set; } = new()
    {
        VectorStoreId = string.Empty,
        FileId = string.Empty
    };
    public CreateVectorStoreFileActionOutput ActionOutput { get; set; } = new()
    {
        Id = string.Empty,
        Object = "vector_store.file",
        CreatedAt = 0,
        UsageBytes = 0,
        VectorStoreId = string.Empty,
        Status = "in_progress"
    };
    public StandardActionFailure ActionFailure { get; set; } = new();

    public bool CreateRtap => true;
}

public class CreateVectorStoreFileActionInput
{
    [JsonPropertyName("vector_store_id")]
    [Description("The ID of the vector store for which to create a File")]
    [Required]
    public required string VectorStoreId { get; init; }

    [JsonPropertyName("file_id")]
    [Description("A File ID that the vector store should use")]
    [Required]
    public required string FileId { get; init; }

    [JsonPropertyName("attributes")]
    [Description("Set of 16 key-value pairs that can be attached to an object")]
    public Dictionary<string, object>? Attributes { get; init; }

    [JsonPropertyName("chunking_strategy")]
    [Description("The chunking strategy used to chunk the file")]
    public ChunkingStrategy? ChunkingStrategy { get; init; }
}

public class CreateVectorStoreFileActionOutput
{
    [JsonPropertyName("id")]
    [Description("The identifier of the vector store file")]
    [Required]
    public required string Id { get; init; }

    [JsonPropertyName("object")]
    [Description("The object type, which is always vector_store.file")]
    [Required]
    public required string Object { get; init; }

    [JsonPropertyName("created_at")]
    [Description("The Unix timestamp (in seconds) for when the vector store file was created")]
    [Required]
    public required long CreatedAt { get; init; }

    [JsonPropertyName("usage_bytes")]
    [Description("The total vector store usage in bytes")]
    [Required]
    public required long UsageBytes { get; init; }

    [JsonPropertyName("vector_store_id")]
    [Description("The ID of the vector store that the File is attached to")]
    [Required]
    public required string VectorStoreId { get; init; }

    [JsonPropertyName("status")]
    [Description("The status of the vector store file")]
    [Required]
    public required string Status { get; init; }

    [JsonPropertyName("last_error")]
    [Description("The last error associated with this vector store file")]
    public LastError? LastError { get; init; }
}
