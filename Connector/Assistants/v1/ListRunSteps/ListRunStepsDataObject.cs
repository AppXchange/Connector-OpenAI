namespace Connector.Assistants.v1.ListRunSteps;

using Json.Schema.Generation;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.CacheWriter;

/// <summary>
/// Data object that represents a list of run steps in the Xchange system.
/// </summary>
[PrimaryKey("first_id", nameof(FirstId))]
[Description("Represents a list of run steps within a thread run")]
public class ListRunStepsDataObject
{
    [JsonPropertyName("object")]
    [Description("The object type, which is always 'list'")]
    [Required]
    public required string Object { get; init; }

    [JsonPropertyName("data")]
    [Description("The list of run steps")]
    [Required]
    public required RunStepData[] Data { get; init; }

    [JsonPropertyName("first_id")]
    [Description("The ID of the first run step in the list")]
    [Required]
    public required string FirstId { get; init; }

    [JsonPropertyName("last_id")]
    [Description("The ID of the last run step in the list")]
    [Required]
    public required string LastId { get; init; }

    [JsonPropertyName("has_more")]
    [Description("Whether there are more run steps to retrieve")]
    [Required]
    public required bool HasMore { get; init; }
}

public class RunStepData
{
    [JsonPropertyName("id")]
    [Description("The identifier of the run step")]
    [Required]
    public required string Id { get; init; }

    [JsonPropertyName("object")]
    [Description("The object type, which is always 'thread.run.step'")]
    [Required]
    public required string Object { get; init; }

    [JsonPropertyName("created_at")]
    [Description("The Unix timestamp (in seconds) for when the run step was created")]
    [Required]
    public required long CreatedAt { get; init; }

    [JsonPropertyName("run_id")]
    [Description("The ID of the run that this run step is a part of")]
    [Required]
    public required string RunId { get; init; }

    [JsonPropertyName("assistant_id")]
    [Description("The ID of the assistant associated with the run step")]
    [Required]
    public required string AssistantId { get; init; }

    [JsonPropertyName("thread_id")]
    [Description("The ID of the thread that was run")]
    [Required]
    public required string ThreadId { get; init; }

    [JsonPropertyName("type")]
    [Description("The type of run step")]
    [Required]
    public required string Type { get; init; }

    [JsonPropertyName("status")]
    [Description("The status of the run step")]
    [Required]
    public required string Status { get; init; }

    [JsonPropertyName("cancelled_at")]
    [Description("The Unix timestamp (in seconds) for when the run step was cancelled")]
    public long? CancelledAt { get; init; }

    [JsonPropertyName("completed_at")]
    [Description("The Unix timestamp (in seconds) for when the run step completed")]
    public long? CompletedAt { get; init; }

    [JsonPropertyName("expired_at")]
    [Description("The Unix timestamp (in seconds) for when the run step expired")]
    public long? ExpiredAt { get; init; }

    [JsonPropertyName("failed_at")]
    [Description("The Unix timestamp (in seconds) for when the run step failed")]
    public long? FailedAt { get; init; }

    [JsonPropertyName("last_error")]
    [Description("The last error associated with this run step")]
    public LastError? LastError { get; init; }

    [JsonPropertyName("step_details")]
    [Description("The details of the run step")]
    [Required]
    public required StepDetails StepDetails { get; init; }

    [JsonPropertyName("usage")]
    [Description("Usage statistics related to the run step")]
    public Usage? Usage { get; init; }
}

public class LastError
{
    [JsonPropertyName("code")]
    [Description("The error code")]
    [Required]
    public required string Code { get; init; }

    [JsonPropertyName("message")]
    [Description("The error message")]
    [Required]
    public required string Message { get; init; }
}

public class StepDetails
{
    [JsonPropertyName("type")]
    [Description("The type of step details")]
    [Required]
    public required string Type { get; init; }

    [JsonPropertyName("message_creation")]
    [Description("Details when the type is message_creation")]
    public MessageCreation? MessageCreation { get; init; }

    [JsonPropertyName("tool_calls")]
    [Description("Details when the type is tool_calls")]
    public ToolCall[]? ToolCalls { get; init; }
}

public class MessageCreation
{
    [JsonPropertyName("message_id")]
    [Description("The ID of the message that was created")]
    [Required]
    public required string MessageId { get; init; }
}

public class ToolCall
{
    [JsonPropertyName("id")]
    [Description("The ID of the tool call")]
    [Required]
    public required string Id { get; init; }

    [JsonPropertyName("type")]
    [Description("The type of tool call")]
    [Required]
    public required string Type { get; init; }

    [JsonPropertyName("function")]
    [Description("The function that was called")]
    public Function? Function { get; init; }
}

public class Function
{
    [JsonPropertyName("name")]
    [Description("The name of the function")]
    [Required]
    public required string Name { get; init; }

    [JsonPropertyName("arguments")]
    [Description("The arguments passed to the function")]
    [Required]
    public required string Arguments { get; init; }

    [JsonPropertyName("output")]
    [Description("The output of the function call")]
    public string? Output { get; init; }
}

public class Usage
{
    [JsonPropertyName("prompt_tokens")]
    [Description("The number of prompt tokens used")]
    [Required]
    public required int PromptTokens { get; init; }

    [JsonPropertyName("completion_tokens")]
    [Description("The number of completion tokens used")]
    [Required]
    public required int CompletionTokens { get; init; }

    [JsonPropertyName("total_tokens")]
    [Description("The total number of tokens used")]
    [Required]
    public required int TotalTokens { get; init; }
}