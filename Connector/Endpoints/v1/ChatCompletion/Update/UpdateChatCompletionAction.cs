namespace Connector.Endpoints.v1.ChatCompletion.Update;

using Json.Schema.Generation;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.Action;

/// <summary>
/// Action object that will represent an action in the Xchange system. This will contain an input object type,
/// an output object type, and a Action failure type (this will default to <see cref="StandardActionFailure"/>
/// but that can be overridden with your own preferred type). These objects will be converted to a JsonSchema, 
/// so add attributes to the properties to provide any descriptions, titles, ranges, max, min, etc... 
/// These types will be used for validation at runtime to make sure the objects being passed through the system 
/// are properly formed. The schema also helps provide integrators more information for what the values 
/// are intended to be.
/// </summary>
[Description("Updates metadata for a stored chat completion using OpenAI's API")]
public class UpdateChatCompletionAction : IStandardAction<UpdateChatCompletionActionInput, UpdateChatCompletionActionOutput>
{
    public UpdateChatCompletionActionInput ActionInput { get; set; } = new() 
    { 
        CompletionId = string.Empty,
        Metadata = new Dictionary<string, string>()
    };
    public UpdateChatCompletionActionOutput ActionOutput { get; set; } = new()
    {
        Id = string.Empty,
        Object = "chat.completion",
        Created = 0,
        Model = string.Empty,
        Choices = Array.Empty<ChatCompletionChoice>(),
        Usage = new ChatCompletionUsage
        {
            PromptTokens = 0,
            CompletionTokens = 0,
            TotalTokens = 0
        }
    };
    public StandardActionFailure ActionFailure { get; set; } = new();

    public bool CreateRtap => true;
}

public class UpdateChatCompletionActionInput
{
    [JsonPropertyName("completion_id")]
    [Description("The ID of the chat completion to update")]
    [Required]
    public required string CompletionId { get; init; }

    [JsonPropertyName("metadata")]
    [Description("Set of key-value pairs that can be attached to an object")]
    [Required]
    public required Dictionary<string, string> Metadata { get; init; }
}

public class UpdateChatCompletionActionOutput
{
    [JsonPropertyName("id")]
    [Description("A unique identifier for the chat completion")]
    [Required]
    public required string Id { get; init; }

    [JsonPropertyName("object")]
    [Description("The object type, which is always chat.completion")]
    [Required]
    public required string Object { get; init; }

    [JsonPropertyName("created")]
    [Description("The Unix timestamp (in seconds) of when the chat completion was created")]
    [Required]
    public required int Created { get; init; }

    [JsonPropertyName("model")]
    [Description("The model used for the chat completion")]
    [Required]
    public required string Model { get; init; }

    [JsonPropertyName("choices")]
    [Description("A list of chat completion choices")]
    [Required]
    public required ChatCompletionChoice[] Choices { get; init; }

    [JsonPropertyName("usage")]
    [Description("Usage statistics for the completion request")]
    [Required]
    public required ChatCompletionUsage Usage { get; init; }

    [JsonPropertyName("metadata")]
    [Description("Set of key-value pairs attached to the object")]
    public Dictionary<string, string>? Metadata { get; init; }

    [JsonPropertyName("service_tier")]
    [Description("The service tier used for processing the request")]
    public string? ServiceTier { get; init; }

    [JsonPropertyName("system_fingerprint")]
    [Description("This fingerprint represents the backend configuration that the model runs with")]
    public string? SystemFingerprint { get; init; }
}

public class ChatCompletionChoice
{
    [JsonPropertyName("index")]
    [Required]
    public required int Index { get; init; }

    [JsonPropertyName("message")]
    [Required]
    public required ChatCompletionMessage Message { get; init; }

    [JsonPropertyName("finish_reason")]
    public string? FinishReason { get; init; }

    [JsonPropertyName("logprobs")]
    public object? LogProbs { get; init; }
}

public class ChatCompletionMessage
{
    [JsonPropertyName("role")]
    [Required]
    public required string Role { get; init; }

    [JsonPropertyName("content")]
    [Required]
    public required string Content { get; init; }

    [JsonPropertyName("tool_calls")]
    public object? ToolCalls { get; init; }

    [JsonPropertyName("function_call")]
    public object? FunctionCall { get; init; }
}

public class ChatCompletionUsage
{
    [JsonPropertyName("prompt_tokens")]
    [Required]
    public required int PromptTokens { get; init; }

    [JsonPropertyName("completion_tokens")]
    [Required]
    public required int CompletionTokens { get; init; }

    [JsonPropertyName("total_tokens")]
    [Required]
    public required int TotalTokens { get; init; }
}
