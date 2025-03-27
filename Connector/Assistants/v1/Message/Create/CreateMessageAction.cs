namespace Connector.Assistants.v1.Message.Create;

using Json.Schema.Generation;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.Action;

/// <summary>
/// Action to create a new message in a thread.
/// </summary>
[Description("Creates a new message in a thread")]
public class CreateMessageAction : IStandardAction<CreateMessageActionInput, CreateMessageActionOutput>
{
    public CreateMessageActionInput ActionInput { get; set; } = new() 
    { 
        ThreadId = string.Empty,
        Role = string.Empty,
        Content = string.Empty
    };
    public CreateMessageActionOutput ActionOutput { get; set; } = new() 
    { 
        Id = string.Empty,
        Object = "thread.message",
        CreatedAt = 0,
        ThreadId = string.Empty,
        Role = string.Empty,
        Content = new[] { new MessageContent { Type = "text", Text = new TextContent { Value = string.Empty, Annotations = Array.Empty<object>() } } },
        Metadata = new Dictionary<string, string>()
    };
    public StandardActionFailure ActionFailure { get; set; } = new();

    public bool CreateRtap => true;
}

public class CreateMessageActionInput
{
    [JsonPropertyName("thread_id")]
    [Description("The ID of the thread to create a message for")]
    [Required]
    public required string ThreadId { get; set; }

    [JsonPropertyName("role")]
    [Description("The role of the entity that is creating the message")]
    [Required]
    public required string Role { get; set; }

    [JsonPropertyName("content")]
    [Description("The content of the message")]
    [Required]
    public required string Content { get; set; }

    [JsonPropertyName("attachments")]
    [Description("A list of files attached to the message, and the tools they should be added to")]
    public MessageAttachment[]? Attachments { get; set; }

    [JsonPropertyName("metadata")]
    [Description("Set of 16 key-value pairs that can be attached to an object")]
    public Dictionary<string, string>? Metadata { get; set; }
}

public class CreateMessageActionOutput
{
    [JsonPropertyName("id")]
    [Description("The identifier, which can be referenced in API endpoints")]
    [Required]
    public required string Id { get; set; }

    [JsonPropertyName("object")]
    [Description("The object type, which is always 'thread.message'")]
    [Required]
    public required string Object { get; set; }

    [JsonPropertyName("created_at")]
    [Description("The Unix timestamp (in seconds) for when the message was created")]
    [Required]
    public required long CreatedAt { get; set; }

    [JsonPropertyName("thread_id")]
    [Description("The thread ID that this message belongs to")]
    [Required]
    public required string ThreadId { get; set; }

    [JsonPropertyName("role")]
    [Description("The entity that produced the message")]
    [Required]
    public required string Role { get; set; }

    [JsonPropertyName("content")]
    [Description("The content of the message in array of text and/or images")]
    [Required]
    public required MessageContent[] Content { get; set; }

    [JsonPropertyName("assistant_id")]
    [Description("If applicable, the ID of the assistant that authored this message")]
    public string? AssistantId { get; set; }

    [JsonPropertyName("run_id")]
    [Description("The ID of the run associated with the creation of this message")]
    public string? RunId { get; set; }

    [JsonPropertyName("attachments")]
    [Description("A list of files attached to the message, and the tools they were added to")]
    public MessageAttachment[]? Attachments { get; set; }

    [JsonPropertyName("metadata")]
    [Description("Set of 16 key-value pairs that can be attached to an object")]
    [Required]
    public required Dictionary<string, string> Metadata { get; set; }
}

public class MessageContent
{
    [JsonPropertyName("type")]
    [Description("The type of content")]
    [Required]
    public required string Type { get; set; }

    [JsonPropertyName("text")]
    [Description("The text content")]
    public TextContent? Text { get; set; }
}

public class TextContent
{
    [JsonPropertyName("value")]
    [Description("The text value")]
    [Required]
    public required string Value { get; set; }

    [JsonPropertyName("annotations")]
    [Description("Any annotations on the text")]
    [Required]
    public required object[] Annotations { get; set; }
}

public class MessageAttachment
{
    [JsonPropertyName("file_id")]
    [Description("The ID of the file")]
    [Required]
    public required string FileId { get; set; }

    [JsonPropertyName("tools")]
    [Description("The tools this file was added to")]
    [Required]
    public required MessageTool[] Tools { get; set; }
}

public class MessageTool
{
    [JsonPropertyName("type")]
    [Description("The type of tool")]
    [Required]
    public required string Type { get; set; }
}
