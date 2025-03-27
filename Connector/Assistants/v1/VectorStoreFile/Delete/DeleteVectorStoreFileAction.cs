namespace Connector.Assistants.v1.VectorStoreFile.Delete;

using Json.Schema.Generation;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.Action;

/// <summary>
/// Action to delete a vector store file.
/// </summary>
[Description("Deletes a vector store file from a vector store")]
public class DeleteVectorStoreFileAction : IStandardAction<DeleteVectorStoreFileActionInput, DeleteVectorStoreFileActionOutput>
{
    public DeleteVectorStoreFileActionInput ActionInput { get; set; } = new()
    {
        VectorStoreId = string.Empty,
        FileId = string.Empty
    };
    public DeleteVectorStoreFileActionOutput ActionOutput { get; set; } = new()
    {
        Id = string.Empty,
        Object = "vector_store.file.deleted",
        Deleted = true
    };
    public StandardActionFailure ActionFailure { get; set; } = new();

    public bool CreateRtap => true;
}

public class DeleteVectorStoreFileActionInput
{
    [JsonPropertyName("vector_store_id")]
    [Description("The ID of the vector store that the file belongs to")]
    [Required]
    public required string VectorStoreId { get; init; }

    [JsonPropertyName("file_id")]
    [Description("The ID of the file to delete")]
    [Required]
    public required string FileId { get; init; }
}

public class DeleteVectorStoreFileActionOutput
{
    [JsonPropertyName("id")]
    [Description("The ID of the deleted file")]
    [Required]
    public required string Id { get; init; }

    [JsonPropertyName("object")]
    [Description("The object type, which is always vector_store.file.deleted")]
    [Required]
    public required string Object { get; init; }

    [JsonPropertyName("deleted")]
    [Description("Whether the vector store file was successfully deleted")]
    [Required]
    public required bool Deleted { get; init; }
}
