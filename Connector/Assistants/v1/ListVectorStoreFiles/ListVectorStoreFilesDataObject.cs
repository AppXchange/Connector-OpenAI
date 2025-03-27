namespace Connector.Assistants.v1.ListVectorStoreFiles;

using Json.Schema.Generation;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.CacheWriter;

/// <summary>
/// Data object that represents a list of vector store files.
/// </summary>
[PrimaryKey("first_id", nameof(FirstId))]
[Description("Represents a list of vector store files")]
public class ListVectorStoreFilesDataObject
{
    [JsonPropertyName("object")]
    [Description("The object type, which is always list")]
    [Required]
    public required string Object { get; init; }

    [JsonPropertyName("data")]
    [Description("The list of vector store files")]
    [Required]
    public required VectorStoreFileData[] Data { get; init; }

    [JsonPropertyName("first_id")]
    [Description("The ID of the first vector store file in the list")]
    [Required]
    public required string FirstId { get; init; }

    [JsonPropertyName("last_id")]
    [Description("The ID of the last vector store file in the list")]
    [Required]
    public required string LastId { get; init; }

    [JsonPropertyName("has_more")]
    [Description("Whether there are more vector store files to retrieve")]
    [Required]
    public required bool HasMore { get; init; }
}

public class VectorStoreFileData
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

    [JsonPropertyName("vector_store_id")]
    [Description("The ID of the vector store that the file belongs to")]
    [Required]
    public required string VectorStoreId { get; init; }
}