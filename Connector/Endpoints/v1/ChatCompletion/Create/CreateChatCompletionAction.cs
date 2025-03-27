namespace Connector.Endpoints.v1.ChatCompletion.Create;

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
[Description("Creates a chat completion using OpenAI's API")]
public class CreateChatCompletionAction : IStandardAction<CreateChatCompletionActionInput, CreateChatCompletionActionOutput>
{
    public CreateChatCompletionActionInput ActionInput { get; set; } = new() 
    { 
        Messages = Array.Empty<ChatMessage>(),
        Model = "gpt-4o"
    };
    public CreateChatCompletionActionOutput ActionOutput { get; set; } = new();
    public StandardActionFailure ActionFailure { get; set; } = new();

    public bool CreateRtap => true;
}

public class CreateChatCompletionActionInput
{
    [JsonPropertyName("messages")]
    [Description("A list of messages comprising the conversation so far")]
    [Required]
    public required ChatMessage[] Messages { get; init; }

    [JsonPropertyName("model")]
    [Description("ID of the model to use (e.g., gpt-4o, o1)")]
    [Required]
    public required string Model { get; init; }

    [JsonPropertyName("audio")]
    [Description("Parameters for audio output")]
    public AudioParameters? Audio { get; init; }

    [JsonPropertyName("frequency_penalty")]
    [Description("Number between -2.0 and 2.0. Positive values penalize new tokens based on their existing frequency")]
    public double? FrequencyPenalty { get; init; }

    [JsonPropertyName("logit_bias")]
    [Description("Modify the likelihood of specified tokens appearing in the completion")]
    public Dictionary<string, int>? LogitBias { get; init; }

    [JsonPropertyName("logprobs")]
    [Description("Whether to return log probabilities of the output tokens")]
    public bool? LogProbs { get; init; }

    [JsonPropertyName("max_completion_tokens")]
    [Description("An upper bound for the number of tokens that can be generated for a completion")]
    public int? MaxCompletionTokens { get; init; }

    [JsonPropertyName("metadata")]
    [Description("Set of key-value pairs that can be attached to an object")]
    public Dictionary<string, string>? Metadata { get; init; }

    [JsonPropertyName("modalities")]
    [Description("Output types that you would like the model to generate")]
    public string[]? Modalities { get; init; }

    [JsonPropertyName("n")]
    [Description("How many chat completion choices to generate for each input message")]
    public int? N { get; init; }

    [JsonPropertyName("parallel_tool_calls")]
    [Description("Whether to enable parallel function calling during tool use")]
    public bool? ParallelToolCalls { get; init; }

    [JsonPropertyName("presence_penalty")]
    [Description("Number between -2.0 and 2.0. Positive values penalize new tokens based on whether they appear in the text")]
    public double? PresencePenalty { get; init; }

    [JsonPropertyName("reasoning_effort")]
    [Description("Constrains effort on reasoning for reasoning models")]
    public string? ReasoningEffort { get; init; }

    [JsonPropertyName("response_format")]
    [Description("An object specifying the format that the model must output")]
    public ResponseFormat? ResponseFormat { get; init; }

    [JsonPropertyName("seed")]
    [Description("If specified, makes a best effort to sample deterministically")]
    public int? Seed { get; init; }

    [JsonPropertyName("service_tier")]
    [Description("Specifies the latency tier to use for processing the request")]
    public string? ServiceTier { get; init; }

    [JsonPropertyName("stop")]
    [Description("Up to 4 sequences where the API will stop generating further tokens")]
    public string[]? Stop { get; init; }

    [JsonPropertyName("store")]
    [Description("Whether or not to store the output of this chat completion request")]
    public bool? Store { get; init; }

    [JsonPropertyName("stream")]
    [Description("If set to true, the model response data will be streamed")]
    public bool? Stream { get; init; }

    [JsonPropertyName("stream_options")]
    [Description("Options for streaming response")]
    public StreamOptions? StreamOptions { get; init; }

    [JsonPropertyName("temperature")]
    [Description("What sampling temperature to use, between 0 and 2")]
    public double? Temperature { get; init; }

    [JsonPropertyName("tool_choice")]
    [Description("Controls which (if any) tool is called by the model")]
    public ToolChoice? ToolChoice { get; init; }

    [JsonPropertyName("tools")]
    [Description("A list of tools the model may call")]
    public Tool[]? Tools { get; init; }

    [JsonPropertyName("top_logprobs")]
    [Description("Number of most likely tokens to return at each token position")]
    public int? TopLogProbs { get; init; }

    [JsonPropertyName("top_p")]
    [Description("An alternative to sampling with temperature")]
    public double? TopP { get; init; }

    [JsonPropertyName("user")]
    [Description("A unique identifier representing your end-user")]
    public string? User { get; init; }
}

public class ChatMessage
{
    [JsonPropertyName("role")]
    [Required]
    public required string Role { get; init; }

    [JsonPropertyName("content")]
    [Required]
    public required string Content { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("function_call")]
    public FunctionCall? FunctionCall { get; init; }
}

public class AudioParameters
{
    [JsonPropertyName("voice")]
    [Required]
    public required string Voice { get; init; }

    [JsonPropertyName("response_format")]
    public string? ResponseFormat { get; init; }

    [JsonPropertyName("speed")]
    public double? Speed { get; init; }
}

public class ResponseFormat
{
    [JsonPropertyName("type")]
    [Required]
    public required string Type { get; init; }

    [JsonPropertyName("json_schema")]
    public object? JsonSchema { get; init; }
}

public class StreamOptions
{
    [JsonPropertyName("include_usage")]
    public bool? IncludeUsage { get; init; }
}

public class ToolChoice
{
    [JsonPropertyName("type")]
    [Required]
    public required string Type { get; init; }

    [JsonPropertyName("function")]
    public FunctionCall? Function { get; init; }
}

public class Tool
{
    [JsonPropertyName("type")]
    [Required]
    public required string Type { get; init; }

    [JsonPropertyName("function")]
    [Required]
    public required Function Function { get; init; }
}

public class Function
{
    [JsonPropertyName("name")]
    [Required]
    public required string Name { get; init; }

    [JsonPropertyName("description")]
    public string? Description { get; init; }

    [JsonPropertyName("parameters")]
    [Required]
    public required object Parameters { get; init; }
}

public class FunctionCall
{
    [JsonPropertyName("name")]
    [Required]
    public required string Name { get; init; }

    [JsonPropertyName("arguments")]
    [Required]
    public required string Arguments { get; init; }
}

public class CreateChatCompletionActionOutput
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("object")]
    public string Object { get; set; } = string.Empty;

    [JsonPropertyName("created")]
    public int Created { get; set; }

    [JsonPropertyName("model")]
    public string Model { get; set; } = string.Empty;

    [JsonPropertyName("choices")]
    public ChatCompletionChoice[] Choices { get; set; } = Array.Empty<ChatCompletionChoice>();

    [JsonPropertyName("usage")]
    public ChatCompletionUsage Usage { get; set; } = new() 
    { 
        PromptTokens = 0,
        CompletionTokens = 0,
        TotalTokens = 0
    };

    [JsonPropertyName("service_tier")]
    public string? ServiceTier { get; set; }

    [JsonPropertyName("system_fingerprint")]
    public string? SystemFingerprint { get; set; }
}
