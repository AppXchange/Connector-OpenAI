namespace Connector.Assistants.v1.Assistant.Modify;

using Json.Schema.Generation;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.Action;

/// <summary>
/// Action to modify an OpenAI Assistant.
/// </summary>
[Description("Modifies an existing OpenAI Assistant with updated configuration")]
public class ModifyAssistantAction : IStandardAction<ModifyAssistantActionInput, ModifyAssistantActionOutput>
{
    public ModifyAssistantActionInput ActionInput { get; set; } = new() { AssistantId = string.Empty };
    public ModifyAssistantActionOutput ActionOutput { get; set; } = new() 
    { 
        Id = string.Empty,
        Object = "assistant",
        CreatedAt = 0,
        Model = string.Empty,
        Tools = Array.Empty<AssistantTool>(),
        Metadata = new Dictionary<string, string>()
    };
    public StandardActionFailure ActionFailure { get; set; } = new();

    public bool CreateRtap => true;
}

public class ModifyAssistantActionInput
{
    [JsonPropertyName("assistant_id")]
    [Description("The ID of the assistant to modify")]
    [Required]
    public required string AssistantId { get; set; }

    [JsonPropertyName("model")]
    [Description("ID of the model to use")]
    public string? Model { get; set; }

    [JsonPropertyName("name")]
    [Description("The name of the assistant. Maximum length is 256 characters")]
    public string? Name { get; set; }

    [JsonPropertyName("description")]
    [Description("The description of the assistant. Maximum length is 512 characters")]
    public string? Description { get; set; }

    [JsonPropertyName("instructions")]
    [Description("The system instructions that the assistant uses. Maximum length is 256,000 characters")]
    public string? Instructions { get; set; }

    [JsonPropertyName("tools")]
    [Description("A list of tools enabled on the assistant. Maximum of 128 tools per assistant")]
    public AssistantTool[]? Tools { get; set; }

    [JsonPropertyName("metadata")]
    [Description("Set of 16 key-value pairs that can be attached to an object")]
    public Dictionary<string, string>? Metadata { get; set; }

    [JsonPropertyName("file_ids")]
    [Description("List of file IDs for code_interpreter tool")]
    public string[]? FileIds { get; set; }

    [JsonPropertyName("temperature")]
    [Description("What sampling temperature to use, between 0 and 2")]
    public double? Temperature { get; set; }

    [JsonPropertyName("top_p")]
    [Description("An alternative to sampling with temperature, called nucleus sampling")]
    public double? TopP { get; set; }

    [JsonPropertyName("response_format")]
    [Description("Specifies the format that the model must output")]
    public ResponseFormat? ResponseFormat { get; set; }

    [JsonPropertyName("tool_resources")]
    [Description("A set of resources that are used by the assistant's tools")]
    public ToolResources? ToolResources { get; set; }

    [JsonPropertyName("reasoning_effort")]
    [Description("Constrains effort on reasoning for reasoning models")]
    public string? ReasoningEffort { get; set; }
}

public class ModifyAssistantActionOutput
{
    [JsonPropertyName("id")]
    [Description("The identifier of the assistant")]
    [Required]
    public required string Id { get; set; }

    [JsonPropertyName("object")]
    [Description("The object type, which is always 'assistant'")]
    [Required]
    public required string Object { get; set; }

    [JsonPropertyName("created_at")]
    [Description("The Unix timestamp (in seconds) for when the assistant was created")]
    [Required]
    public required long CreatedAt { get; set; }

    [JsonPropertyName("name")]
    [Description("The name of the assistant")]
    public string? Name { get; set; }

    [JsonPropertyName("description")]
    [Description("The description of the assistant")]
    public string? Description { get; set; }

    [JsonPropertyName("model")]
    [Description("ID of the model to use")]
    [Required]
    public required string Model { get; set; }

    [JsonPropertyName("instructions")]
    [Description("The system instructions that the assistant uses")]
    public string? Instructions { get; set; }

    [JsonPropertyName("tools")]
    [Description("A list of tools enabled on the assistant")]
    [Required]
    public required AssistantTool[] Tools { get; set; }

    [JsonPropertyName("metadata")]
    [Description("Set of key-value pairs attached to the object")]
    [Required]
    public required Dictionary<string, string> Metadata { get; set; }

    [JsonPropertyName("top_p")]
    [Description("An alternative to sampling with temperature")]
    public double? TopP { get; set; }

    [JsonPropertyName("temperature")]
    [Description("What sampling temperature to use")]
    public double? Temperature { get; set; }

    [JsonPropertyName("response_format")]
    [Description("Specifies the format that the model must output")]
    public ResponseFormat? ResponseFormat { get; set; }

    [JsonPropertyName("tool_resources")]
    [Description("A set of resources that are used by the assistant's tools")]
    public ToolResources? ToolResources { get; set; }
}
