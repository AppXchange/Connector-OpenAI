namespace Connector.Endpoints.v1.File.Delete;

using Json.Schema.Generation;
using System;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.Action;

/// <summary>
/// Action for deleting a file from OpenAI's API
/// </summary>
[Description("Deletes a file from OpenAI's API")]
public class DeleteFileAction : IStandardAction<DeleteFileActionInput, DeleteFileActionOutput>
{
    public DeleteFileActionInput ActionInput { get; set; } = new() { FileId = string.Empty };
    public DeleteFileActionOutput ActionOutput { get; set; } = new()
    {
        Id = string.Empty,
        Object = "file",
        Deleted = false
    };
    public StandardActionFailure ActionFailure { get; set; } = new();

    public bool CreateRtap => true;
}

public class DeleteFileActionInput
{
    [JsonPropertyName("file_id")]
    [Description("The ID of the file to delete")]
    [Required]
    public required string FileId { get; set; }
}

public class DeleteFileActionOutput
{
    [JsonPropertyName("id")]
    [Description("The ID of the deleted file")]
    [Required]
    public required string Id { get; set; }

    [JsonPropertyName("object")]
    [Description("The object type, which is always 'file'")]
    [Required]
    public required string Object { get; set; }

    [JsonPropertyName("deleted")]
    [Description("Whether the file was successfully deleted")]
    [Required]
    public required bool Deleted { get; set; }
}
