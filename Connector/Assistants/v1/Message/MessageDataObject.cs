namespace Connector.Assistants.v1.Message;

using Json.Schema.Generation;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.CacheWriter;

/// <summary>
/// Data object that represents an OpenAI Message in the Xchange system.
/// </summary>
[PrimaryKey("id", nameof(Id))]
[Description("Represents a message within a thread")]
public class MessageDataObject
{
    [JsonPropertyName("id")]
    [Description("The identifier, which can be referenced in API endpoints")]
    [Required]
    public required string Id { get; init; }

    [JsonPropertyName("object")]
    [Description("The object type, which is always 'thread.message'")]
    [Required]
    public required string Object { get; init; }

    [JsonPropertyName("created_at")]
    [Description("The Unix timestamp (in seconds) for when the message was created")]
    [Required]
    public required long CreatedAt { get; init; }

    [JsonPropertyName("thread_id")]
    [Description("The thread ID that this message belongs to")]
    [Required]
    public required string ThreadId { get; init; }

    [JsonPropertyName("role")]
    [Description("The entity that produced the message. One of user or assistant")]
    [Required]
    public required string Role { get; init; }

    [JsonPropertyName("content")]
    [Description("The content of the message in array of text and/or images")]
    [Required]
    public required MessageContent[] Content { get; init; }

    [JsonPropertyName("assistant_id")]
    [Description("If applicable, the ID of the assistant that authored this message")]
    public string? AssistantId { get; init; }

    [JsonPropertyName("run_id")]
    [Description("The ID of the run associated with the creation of this message")]
    public string? RunId { get; init; }

    [JsonPropertyName("attachments")]
    [Description("A list of files attached to the message, and the tools they were added to")]
    public MessageAttachment[]? Attachments { get; init; }

    [JsonPropertyName("metadata")]
    [Description("Set of 16 key-value pairs that can be attached to an object")]
    [Required]
    public required Dictionary<string, string> Metadata { get; init; }

    [JsonPropertyName("completed_at")]
    [Description("The Unix timestamp (in seconds) for when the message was completed")]
    public long? CompletedAt { get; init; }

    [JsonPropertyName("incomplete_at")]
    [Description("The Unix timestamp (in seconds) for when the message was marked as incomplete")]
    public long? IncompleteAt { get; init; }

    [JsonPropertyName("incomplete_details")]
    [Description("On an incomplete message, details about why the message is incomplete")]
    public IncompleteDetails? IncompleteDetails { get; init; }

    [JsonPropertyName("status")]
    [Description("The status of the message, which can be either in_progress, incomplete, or completed")]
    [Required]
    public required string Status { get; init; }
}

public class MessageContent
{
    [JsonPropertyName("type")]
    [Description("The type of content")]
    [Required]
    public required string Type { get; init; }

    [JsonPropertyName("text")]
    [Description("The text content")]
    public TextContent? Text { get; init; }
}

public class TextContent
{
    [JsonPropertyName("value")]
    [Description("The text value")]
    [Required]
    public required string Value { get; init; }

    [JsonPropertyName("annotations")]
    [Description("Any annotations on the text")]
    [Required]
    public required object[] Annotations { get; init; }
}

public class MessageAttachment
{
    [JsonPropertyName("file_id")]
    [Description("The ID of the file")]
    [Required]
    public required string FileId { get; init; }

    [JsonPropertyName("tools")]
    [Description("The tools this file was added to")]
    [Required]
    public required MessageTool[] Tools { get; init; }
}

public class MessageTool
{
    [JsonPropertyName("type")]
    [Description("The type of tool")]
    [Required]
    public required string Type { get; init; }
}

public class IncompleteDetails
{
    [JsonPropertyName("reason")]
    [Description("The reason why the message is incomplete")]
    [Required]
    public required string Reason { get; init; }
}