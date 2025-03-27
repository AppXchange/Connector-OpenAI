namespace Connector.Assistants.v1.VectorStore.Delete;

using Json.Schema.Generation;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.Action;

/// <summary>
/// Action to delete a vector store.
/// </summary>
[Description("Deletes a vector store")]
public class DeleteVectorStoreAction : IStandardAction<DeleteVectorStoreActionInput, DeleteVectorStoreActionOutput>
{
    public DeleteVectorStoreActionInput ActionInput { get; set; } = new() 
    { 
        VectorStoreId = string.Empty 
    };
    public DeleteVectorStoreActionOutput ActionOutput { get; set; } = new() 
    { 
        Id = string.Empty,
        Object = "vector_store.deleted",
        Deleted = true
    };
    public StandardActionFailure ActionFailure { get; set; } = new();

    public bool CreateRtap => true;
}

public class DeleteVectorStoreActionInput
{
    [JsonPropertyName("vector_store_id")]
    [Description("The ID of the vector store to delete")]
    [Required]
    public required string VectorStoreId { get; init; }
}

public class DeleteVectorStoreActionOutput
{
    [JsonPropertyName("id")]
    [Description("The ID of the deleted vector store")]
    [Required]
    public required string Id { get; init; }

    [JsonPropertyName("object")]
    [Description("The object type, which is always vector_store.deleted")]
    [Required]
    public required string Object { get; init; }

    [JsonPropertyName("deleted")]
    [Description("Whether the vector store was successfully deleted")]
    [Required]
    public required bool Deleted { get; init; }
}
