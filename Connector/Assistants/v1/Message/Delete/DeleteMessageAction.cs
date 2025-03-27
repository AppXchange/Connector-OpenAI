namespace Connector.Assistants.v1.Message.Delete;

using Json.Schema.Generation;
using System;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.Action;

/// <summary>
/// Action to delete a message from a thread.
/// </summary>
[Description("Deletes a message from a thread")]
public class DeleteMessageAction : IStandardAction<DeleteMessageActionInput, DeleteMessageActionOutput>
{
    public DeleteMessageActionInput ActionInput { get; set; } = new() 
    { 
        ThreadId = string.Empty,
        MessageId = string.Empty
    };
    public DeleteMessageActionOutput ActionOutput { get; set; } = new() 
    { 
        Id = string.Empty,
        Object = "thread.message.deleted",
        Deleted = false
    };
    public StandardActionFailure ActionFailure { get; set; } = new();

    public bool CreateRtap => true;
}

public class DeleteMessageActionInput
{
    [JsonPropertyName("thread_id")]
    [Description("The ID of the thread containing the message")]
    [Required]
    public required string ThreadId { get; set; }

    [JsonPropertyName("message_id")]
    [Description("The ID of the message to delete")]
    [Required]
    public required string MessageId { get; set; }
}

public class DeleteMessageActionOutput
{
    [JsonPropertyName("id")]
    [Description("The ID of the deleted message")]
    [Required]
    public required string Id { get; set; }

    [JsonPropertyName("object")]
    [Description("The object type, which is always 'thread.message.deleted'")]
    [Required]
    public required string Object { get; set; }

    [JsonPropertyName("deleted")]
    [Description("Whether the message was successfully deleted")]
    [Required]
    public required bool Deleted { get; set; }
}
