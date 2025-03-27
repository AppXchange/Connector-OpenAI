namespace Connector.Assistants.v1.Thread.Delete;

using Json.Schema.Generation;
using System;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.Action;

/// <summary>
/// Action to delete an OpenAI Thread.
/// </summary>
[Description("Deletes an OpenAI Thread by its ID")]
public class DeleteThreadAction : IStandardAction<DeleteThreadActionInput, DeleteThreadActionOutput>
{
    public DeleteThreadActionInput ActionInput { get; set; } = new() { ThreadId = string.Empty };
    public DeleteThreadActionOutput ActionOutput { get; set; } = new() 
    { 
        Id = string.Empty,
        Object = "thread.deleted",
        Deleted = false
    };
    public StandardActionFailure ActionFailure { get; set; } = new();

    public bool CreateRtap => true;
}

public class DeleteThreadActionInput
{
    [JsonPropertyName("thread_id")]
    [Description("The ID of the thread to delete")]
    [Required]
    public required string ThreadId { get; set; }
}

public class DeleteThreadActionOutput
{
    [JsonPropertyName("id")]
    [Description("The ID of the deleted thread")]
    [Required]
    public required string Id { get; set; }

    [JsonPropertyName("object")]
    [Description("The object type, which is always 'thread.deleted'")]
    [Required]
    public required string Object { get; set; }

    [JsonPropertyName("deleted")]
    [Description("Whether the thread was successfully deleted")]
    [Required]
    public required bool Deleted { get; set; }
}
