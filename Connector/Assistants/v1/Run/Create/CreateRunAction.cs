namespace Connector.Assistants.v1.Run.Create;

using Json.Schema.Generation;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.Action;

/// <summary>
/// Action to create a new run in a thread.
/// </summary>
[Description("Creates a new run in a thread")]
public class CreateRunAction : IStandardAction<CreateRunActionInput, CreateRunActionOutput>
{
    public CreateRunActionInput ActionInput { get; set; } = new() 
    { 
        ThreadId = string.Empty,
        AssistantId = string.Empty
    };
    public CreateRunActionOutput ActionOutput { get; set; } = new() 
    { 
        Id = string.Empty,
        Object = "thread.run",
        CreatedAt = 0,
        AssistantId = string.Empty,
        ThreadId = string.Empty,
        Status = "queued",
        Model = string.Empty,
        Tools = Array.Empty<Tool>(),
        Metadata = new Dictionary<string, string>(),
        ParallelToolCalls = true
    };
    public StandardActionFailure ActionFailure { get; set; } = new();

    public bool CreateRtap => true;
}

public class CreateRunActionInput
{
    [JsonPropertyName("thread_id")]
    [Description("The ID of the thread to run")]
    [Required]
    public required string ThreadId { get; set; }

    [JsonPropertyName("assistant_id")]
    [Description("The ID of the assistant to use to execute this run")]
    [Required]
    public required string AssistantId { get; set; }

    [JsonPropertyName("additional_instructions")]
    [Description("Appends additional instructions at the end of the instructions for the run")]
    public string? AdditionalInstructions { get; set; }

    [JsonPropertyName("additional_messages")]
    [Description("Adds additional messages to the thread before creating the run")]
    public AdditionalMessage[]? AdditionalMessages { get; set; }

    [JsonPropertyName("instructions")]
    [Description("Overrides the instructions of the assistant")]
    public string? Instructions { get; set; }

    [JsonPropertyName("max_completion_tokens")]
    [Description("The maximum number of completion tokens that may be used over the course of the run")]
    public int? MaxCompletionTokens { get; set; }

    [JsonPropertyName("max_prompt_tokens")]
    [Description("The maximum number of prompt tokens that may be used over the course of the run")]
    public int? MaxPromptTokens { get; set; }

    [JsonPropertyName("metadata")]
    [Description("Set of 16 key-value pairs that can be attached to an object")]
    public Dictionary<string, string>? Metadata { get; set; }

    [JsonPropertyName("model")]
    [Description("The ID of the Model to be used to execute this run")]
    public string? Model { get; set; }

    [JsonPropertyName("parallel_tool_calls")]
    [Description("Whether to enable parallel function calling during tool use")]
    public bool? ParallelToolCalls { get; set; }

    [JsonPropertyName("reasoning_effort")]
    [Description("Constrains effort on reasoning for reasoning models")]
    public string? ReasoningEffort { get; set; }

    [JsonPropertyName("response_format")]
    [Description("Specifies the format that the model must output")]
    public ResponseFormat? ResponseFormat { get; set; }

    [JsonPropertyName("stream")]
    [Description("If true, returns a stream of events that happen during the Run")]
    public bool? Stream { get; set; }

    [JsonPropertyName("temperature")]
    [Description("What sampling temperature to use, between 0 and 2")]
    public double? Temperature { get; set; }

    [JsonPropertyName("tool_choice")]
    [Description("Controls which (if any) tool is called by the model")]
    public ToolChoice? ToolChoice { get; set; }

    [JsonPropertyName("tools")]
    [Description("Override the tools the assistant can use for this run")]
    public Tool[]? Tools { get; set; }

    [JsonPropertyName("top_p")]
    [Description("An alternative to sampling with temperature, called nucleus sampling")]
    public double? TopP { get; set; }

    [JsonPropertyName("truncation_strategy")]
    [Description("Controls for how a thread will be truncated prior to the run")]
    public TruncationStrategy? TruncationStrategy { get; set; }
}

public class AdditionalMessage
{
    [JsonPropertyName("role")]
    [Description("The role of the entity that is creating the message")]
    [Required]
    public required string Role { get; set; }

    [JsonPropertyName("content")]
    [Description("The content of the message")]
    [Required]
    public required string Content { get; set; }
}

public class ResponseFormat
{
    [JsonPropertyName("type")]
    [Description("The type of response format")]
    [Required]
    public required string Type { get; set; }

    [JsonPropertyName("json_schema")]
    [Description("The JSON schema for structured outputs")]
    public object? JsonSchema { get; set; }
}

public class ToolChoice
{
    [JsonPropertyName("type")]
    [Description("The type of tool choice")]
    [Required]
    public required string Type { get; set; }

    [JsonPropertyName("function")]
    [Description("The function to call")]
    public Function? Function { get; set; }
}

public class Function
{
    [JsonPropertyName("name")]
    [Description("The name of the function")]
    [Required]
    public required string Name { get; set; }
}

public class TruncationStrategy
{
    [JsonPropertyName("type")]
    [Description("The type of truncation strategy")]
    [Required]
    public required string Type { get; set; }

    [JsonPropertyName("last_messages")]
    [Description("The number of last messages to keep")]
    public int? LastMessages { get; set; }
}

public class Tool
{
    [JsonPropertyName("type")]
    [Description("The type of tool")]
    [Required]
    public required string Type { get; set; }
}

public class CreateRunActionOutput
{
    [JsonPropertyName("id")]
    [Description("The identifier, which can be referenced in API endpoints")]
    [Required]
    public required string Id { get; set; }

    [JsonPropertyName("object")]
    [Description("The object type, which is always 'thread.run'")]
    [Required]
    public required string Object { get; set; }

    [JsonPropertyName("created_at")]
    [Description("The Unix timestamp (in seconds) for when the run was created")]
    [Required]
    public required long CreatedAt { get; set; }

    [JsonPropertyName("assistant_id")]
    [Description("The ID of the assistant used for execution of this run")]
    [Required]
    public required string AssistantId { get; set; }

    [JsonPropertyName("thread_id")]
    [Description("The ID of the thread that was executed on as a part of this run")]
    [Required]
    public required string ThreadId { get; set; }

    [JsonPropertyName("status")]
    [Description("The status of the run")]
    [Required]
    public required string Status { get; set; }

    [JsonPropertyName("started_at")]
    [Description("The Unix timestamp (in seconds) for when the run was started")]
    public long? StartedAt { get; set; }

    [JsonPropertyName("expires_at")]
    [Description("The Unix timestamp (in seconds) for when the run will expire")]
    public long? ExpiresAt { get; set; }

    [JsonPropertyName("cancelled_at")]
    [Description("The Unix timestamp (in seconds) for when the run was cancelled")]
    public long? CancelledAt { get; set; }

    [JsonPropertyName("failed_at")]
    [Description("The Unix timestamp (in seconds) for when the run failed")]
    public long? FailedAt { get; set; }

    [JsonPropertyName("completed_at")]
    [Description("The Unix timestamp (in seconds) for when the run was completed")]
    public long? CompletedAt { get; set; }

    [JsonPropertyName("last_error")]
    [Description("The last error associated with this run")]
    public LastError? LastError { get; set; }

    [JsonPropertyName("model")]
    [Description("The model that the assistant used for this run")]
    [Required]
    public required string Model { get; set; }

    [JsonPropertyName("instructions")]
    [Description("The instructions that the assistant used for this run")]
    public string? Instructions { get; set; }

    [JsonPropertyName("incomplete_details")]
    [Description("Details on why the run is incomplete")]
    public IncompleteDetails? IncompleteDetails { get; set; }

    [JsonPropertyName("tools")]
    [Description("The list of tools that the assistant used for this run")]
    [Required]
    public required Tool[] Tools { get; set; }

    [JsonPropertyName("metadata")]
    [Description("Set of 16 key-value pairs that can be attached to an object")]
    [Required]
    public required Dictionary<string, string> Metadata { get; set; }

    [JsonPropertyName("usage")]
    [Description("Usage statistics related to the run")]
    public Usage? Usage { get; set; }

    [JsonPropertyName("temperature")]
    [Description("The sampling temperature used for this run")]
    public double? Temperature { get; set; }

    [JsonPropertyName("top_p")]
    [Description("The nucleus sampling value used for this run")]
    public double? TopP { get; set; }

    [JsonPropertyName("max_prompt_tokens")]
    [Description("The maximum number of prompt tokens specified")]
    public int? MaxPromptTokens { get; set; }

    [JsonPropertyName("max_completion_tokens")]
    [Description("The maximum number of completion tokens specified")]
    public int? MaxCompletionTokens { get; set; }

    [JsonPropertyName("truncation_strategy")]
    [Description("Controls for how a thread will be truncated prior to the run")]
    public TruncationStrategy? TruncationStrategy { get; set; }

    [JsonPropertyName("response_format")]
    [Description("Specifies the format that the model must output")]
    public ResponseFormat? ResponseFormat { get; set; }

    [JsonPropertyName("tool_choice")]
    [Description("Controls which (if any) tool is called by the model")]
    public ToolChoice? ToolChoice { get; set; }

    [JsonPropertyName("parallel_tool_calls")]
    [Description("Whether to enable parallel function calling during tool use")]
    [Required]
    public required bool ParallelToolCalls { get; set; }
}

public class LastError
{
    [JsonPropertyName("code")]
    [Description("The error code")]
    [Required]
    public required string Code { get; set; }

    [JsonPropertyName("message")]
    [Description("The error message")]
    [Required]
    public required string Message { get; set; }
}

public class IncompleteDetails
{
    [JsonPropertyName("reason")]
    [Description("The reason why the run is incomplete")]
    [Required]
    public required string Reason { get; set; }
}

public class Usage
{
    [JsonPropertyName("prompt_tokens")]
    [Description("The number of prompt tokens used")]
    [Required]
    public required int PromptTokens { get; set; }

    [JsonPropertyName("completion_tokens")]
    [Description("The number of completion tokens used")]
    [Required]
    public required int CompletionTokens { get; set; }

    [JsonPropertyName("total_tokens")]
    [Description("The total number of tokens used")]
    [Required]
    public required int TotalTokens { get; set; }
}
