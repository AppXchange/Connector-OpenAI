namespace Connector.Assistants.v1.Thread.Modify;

using Json.Schema.Generation;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.Action;

/// <summary>
/// Action to modify an OpenAI Thread.
/// </summary>
[Description("Modifies an existing OpenAI Thread's metadata and tool resources")]
public class ModifyThreadAction : IStandardAction<ModifyThreadActionInput, ModifyThreadActionOutput>
{
    public ModifyThreadActionInput ActionInput { get; set; } = new() { ThreadId = string.Empty };
    public ModifyThreadActionOutput ActionOutput { get; set; } = new() 
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

public class ModifyThreadActionInput
{
    [JsonPropertyName("thread_id")]
    [Description("The ID of the thread to modify")]
    [Required]
    public required string ThreadId { get; set; } = string.Empty;

    [JsonPropertyName("metadata")]
    [Description("Set of 16 key-value pairs that can be attached to an object")]
    public Dictionary<string, string>? Metadata { get; set; }

    [JsonPropertyName("tool_resources")]
    [Description("A set of resources that are made available to the assistant's tools in this thread")]
    public ToolResources? ToolResources { get; set; }
}

public class ModifyThreadActionOutput
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
