namespace Connector.Assistants.v1.Thread.Create;

using Json.Schema.Generation;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.Action;

/// <summary>
/// Action to create a new OpenAI Thread.
/// </summary>
[Description("Creates a new OpenAI Thread for conversation")]
public class CreateThreadAction : IStandardAction<CreateThreadActionInput, CreateThreadActionOutput>
{
    public CreateThreadActionInput ActionInput { get; set; } = new();
    public CreateThreadActionOutput ActionOutput { get; set; } = new() 
    { 
        Id = string.Empty,
        Object = "thread",
        CreatedAt = 0,
        Metadata = new Dictionary<string, string>(),
        ToolResources = new ToolResources()
    };
    public StandardActionFailure ActionFailure { get; set; } = new();

    public bool CreateRtap => true;
}

public class CreateThreadActionInput
{
    [JsonPropertyName("messages")]
    [Description("A list of messages to start the thread with")]
    public Message[]? Messages { get; set; }

    [JsonPropertyName("metadata")]
    [Description("Set of 16 key-value pairs that can be attached to an object")]
    public Dictionary<string, string>? Metadata { get; set; }

    [JsonPropertyName("tool_resources")]
    [Description("A set of resources that are made available to the assistant's tools in this thread")]
    public ToolResources? ToolResources { get; set; }
}

public class CreateThreadActionOutput
{
    [JsonPropertyName("id")]
    [Description("The identifier, which can be referenced in API endpoints")]
    [Required]
    public required string Id { get; set; }

    [JsonPropertyName("object")]
    [Description("The object type, which is always 'thread'")]
    [Required]
    public required string Object { get; set; }

    [JsonPropertyName("created_at")]
    [Description("The Unix timestamp (in seconds) for when the thread was created")]
    [Required]
    public required long CreatedAt { get; set; }

    [JsonPropertyName("metadata")]
    [Description("Set of 16 key-value pairs that can be attached to an object")]
    [Required]
    public required Dictionary<string, string> Metadata { get; set; }

    [JsonPropertyName("tool_resources")]
    [Description("A set of resources that are made available to the assistant's tools in this thread")]
    [Required]
    public required ToolResources ToolResources { get; set; }
}

public class Message
{
    [JsonPropertyName("role")]
    [Description("The role of the message author")]
    [Required]
    public required string Role { get; set; }

    [JsonPropertyName("content")]
    [Description("The content of the message")]
    [Required]
    public required string Content { get; set; }

    [JsonPropertyName("file_ids")]
    [Description("A list of file IDs that the message is attached to")]
    public string[]? FileIds { get; set; }

    [JsonPropertyName("metadata")]
    [Description("Set of 16 key-value pairs that can be attached to an object")]
    public Dictionary<string, string>? Metadata { get; set; }
}

public class ToolResources
{
    [JsonPropertyName("code_interpreter")]
    [Description("Resources for the code_interpreter tool")]
    public CodeInterpreterResources? CodeInterpreter { get; set; }
}

public class CodeInterpreterResources
{
    [JsonPropertyName("file_ids")]
    [Description("List of file IDs for code_interpreter tool")]
    public string[]? FileIds { get; set; }
}
