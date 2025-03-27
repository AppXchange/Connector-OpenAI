namespace Connector.Endpoints.v1.ChatCompletion;

using Json.Schema.Generation;
using System;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.CacheWriter;

/// <summary>
/// Data object that will represent an object in the Xchange system. This will be converted to a JsonSchema, 
/// so add attributes to the properties to provide any descriptions, titles, ranges, max, min, etc... 
/// These types will be used for validation at runtime to make sure the objects being passed through the system 
/// are properly formed. The schema also helps provide integrators more information for what the values 
/// are intended to be.
/// </summary>
[PrimaryKey("id", nameof(Id))]
//[AlternateKey("alt-key-id", nameof(CompanyId), nameof(EquipmentNumber))]
[Description("Represents a chat completion response from OpenAI's API")]
public class ChatCompletionDataObject
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

    [JsonPropertyName("prompt_tokens_details")]
    public TokenDetails? PromptTokensDetails { get; init; }

    [JsonPropertyName("completion_tokens_details")]
    public TokenDetails? CompletionTokensDetails { get; init; }
}

public class TokenDetails
{
    [JsonPropertyName("cached_tokens")]
    public int? CachedTokens { get; init; }

    [JsonPropertyName("audio_tokens")]
    public int? AudioTokens { get; init; }

    [JsonPropertyName("reasoning_tokens")]
    public int? ReasoningTokens { get; init; }

    [JsonPropertyName("accepted_prediction_tokens")]
    public int? AcceptedPredictionTokens { get; init; }

    [JsonPropertyName("rejected_prediction_tokens")]
    public int? RejectedPredictionTokens { get; init; }
}