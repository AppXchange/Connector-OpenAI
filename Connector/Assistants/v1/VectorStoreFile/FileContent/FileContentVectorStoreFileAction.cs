namespace Connector.Assistants.v1.VectorStoreFile.FileContent;

using Json.Schema.Generation;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.Action;

/// <summary>
/// Action to retrieve the parsed contents of a vector store file.
/// </summary>
[Description("Retrieves the parsed contents of a vector store file")]
public class FileContentVectorStoreFileAction : IStandardAction<FileContentVectorStoreFileActionInput, FileContentVectorStoreFileActionOutput>
{
    public FileContentVectorStoreFileActionInput ActionInput { get; set; } = new()
    {
        VectorStoreId = string.Empty,
        FileId = string.Empty
    };
    public FileContentVectorStoreFileActionOutput ActionOutput { get; set; } = new()
    {
        FileId = string.Empty,
        Filename = string.Empty,
        Content = new List<ContentItem>()
    };
    public StandardActionFailure ActionFailure { get; set; } = new();

    public bool CreateRtap => true;
}

public class FileContentVectorStoreFileActionInput
{
    [JsonPropertyName("vector_store_id")]
    [Description("The ID of the vector store")]
    [Required]
    public required string VectorStoreId { get; init; }

    [JsonPropertyName("file_id")]
    [Description("The ID of the file within the vector store")]
    [Required]
    public required string FileId { get; init; }
}

public class FileContentVectorStoreFileActionOutput
{
    [JsonPropertyName("file_id")]
    [Description("The ID of the file")]
    [Required]
    public required string FileId { get; init; }

    [JsonPropertyName("filename")]
    [Description("The name of the file")]
    [Required]
    public required string Filename { get; init; }

    [JsonPropertyName("attributes")]
    [Description("Set of key-value pairs that can be attached to an object")]
    public Dictionary<string, object>? Attributes { get; init; }

    [JsonPropertyName("content")]
    [Description("The parsed contents of the file")]
    [Required]
    public required List<ContentItem> Content { get; init; }
}

public class ContentItem
{
    [JsonPropertyName("type")]
    [Description("The type of content")]
    [Required]
    public required string Type { get; init; }

    [JsonPropertyName("text")]
    [Description("The text content")]
    [Required]
    public required string Text { get; init; }
}
