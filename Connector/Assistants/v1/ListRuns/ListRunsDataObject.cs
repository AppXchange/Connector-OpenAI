namespace Connector.Assistants.v1.ListRuns;

using Json.Schema.Generation;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.CacheWriter;

/// <summary>
/// Data object that represents a list of OpenAI Runs in the Xchange system.
/// </summary>
[PrimaryKey("first_id", nameof(FirstId))]
[Description("Represents a list of runs within a thread")]
public class ListRunsDataObject
{
    [JsonPropertyName("object")]
    [Description("The object type, which is always 'list'")]
    [Required]
    public required string Object { get; init; }

    [JsonPropertyName("data")]
    [Description("The list of runs")]
    [Required]
    public required RunData[] Data { get; init; }

    [JsonPropertyName("first_id")]
    [Description("The ID of the first run in the list")]
    [Required]
    public required string FirstId { get; init; }

    [JsonPropertyName("last_id")]
    [Description("The ID of the last run in the list")]
    [Required]
    public required string LastId { get; init; }

    [JsonPropertyName("has_more")]
    [Description("Whether there are more runs to retrieve")]
    [Required]
    public required bool HasMore { get; init; }
}

public class RunData
{
    [JsonPropertyName("id")]
    [Description("The identifier, which can be referenced in API endpoints")]
    [Required]
    public required string Id { get; init; }

    [JsonPropertyName("object")]
    [Description("The object type, which is always 'thread.run'")]
    [Required]
    public required string Object { get; init; }

    [JsonPropertyName("created_at")]
    [Description("The Unix timestamp (in seconds) for when the run was created")]
    [Required]
    public required long CreatedAt { get; init; }

    [JsonPropertyName("assistant_id")]
    [Description("The ID of the assistant used for execution of this run")]
    [Required]
    public required string AssistantId { get; init; }

    [JsonPropertyName("thread_id")]
    [Description("The ID of the thread that was executed on as a part of this run")]
    [Required]
    public required string ThreadId { get; init; }

    [JsonPropertyName("status")]
    [Description("The status of the run")]
    [Required]
    public required string Status { get; init; }

    [JsonPropertyName("started_at")]
    [Description("The Unix timestamp (in seconds) for when the run was started")]
    public long? StartedAt { get; init; }

    [JsonPropertyName("expires_at")]
    [Description("The Unix timestamp (in seconds) for when the run will expire")]
    public long? ExpiresAt { get; init; }

    [JsonPropertyName("cancelled_at")]
    [Description("The Unix timestamp (in seconds) for when the run was cancelled")]
    public long? CancelledAt { get; init; }

    [JsonPropertyName("failed_at")]
    [Description("The Unix timestamp (in seconds) for when the run failed")]
    public long? FailedAt { get; init; }

    [JsonPropertyName("completed_at")]
    [Description("The Unix timestamp (in seconds) for when the run was completed")]
    public long? CompletedAt { get; init; }

    [JsonPropertyName("last_error")]
    [Description("The last error associated with this run")]
    public LastError? LastError { get; init; }

    [JsonPropertyName("model")]
    [Description("The model that the assistant used for this run")]
    [Required]
    public required string Model { get; init; }

    [JsonPropertyName("instructions")]
    [Description("The instructions that the assistant used for this run")]
    public string? Instructions { get; init; }

    [JsonPropertyName("incomplete_details")]
    [Description("Details on why the run is incomplete")]
    public IncompleteDetails? IncompleteDetails { get; init; }

    [JsonPropertyName("tools")]
    [Description("The list of tools that the assistant used for this run")]
    [Required]
    public required Tool[] Tools { get; init; }

    [JsonPropertyName("tool_resources")]
    [Description("A set of resources that are used by the assistant's tools")]
    public ToolResources? ToolResources { get; init; }

    [JsonPropertyName("metadata")]
    [Description("Set of 16 key-value pairs that can be attached to an object")]
    [Required]
    public required Dictionary<string, string> Metadata { get; init; }

    [JsonPropertyName("usage")]
    [Description("Usage statistics related to the run")]
    public Usage? Usage { get; init; }

    [JsonPropertyName("temperature")]
    [Description("The sampling temperature used for this run")]
    public double? Temperature { get; init; }

    [JsonPropertyName("top_p")]
    [Description("The nucleus sampling value used for this run")]
    public double? TopP { get; init; }

    [JsonPropertyName("max_prompt_tokens")]
    [Description("The maximum number of prompt tokens specified")]
    public int? MaxPromptTokens { get; init; }

    [JsonPropertyName("max_completion_tokens")]
    [Description("The maximum number of completion tokens specified")]
    public int? MaxCompletionTokens { get; init; }

    [JsonPropertyName("truncation_strategy")]
    [Description("Controls for how a thread will be truncated prior to the run")]
    public TruncationStrategy? TruncationStrategy { get; init; }

    [JsonPropertyName("response_format")]
    [Description("Specifies the format that the model must output")]
    public string? ResponseFormat { get; init; }

    [JsonPropertyName("tool_choice")]
    [Description("Controls which (if any) tool is called by the model")]
    public string? ToolChoice { get; init; }

    [JsonPropertyName("parallel_tool_calls")]
    [Description("Whether to enable parallel function calling during tool use")]
    [Required]
    public required bool ParallelToolCalls { get; init; }
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

public class IncompleteDetails
{
    [JsonPropertyName("reason")]
    [Description("The reason why the run is incomplete")]
    [Required]
    public required string Reason { get; init; }
}

public class Tool
{
    [JsonPropertyName("type")]
    [Description("The type of tool")]
    [Required]
    public required string Type { get; init; }
}

public class ToolResources
{
    [JsonPropertyName("code_interpreter")]
    [Description("Resources for the code_interpreter tool")]
    public CodeInterpreterResources? CodeInterpreter { get; init; }
}

public class CodeInterpreterResources
{
    [JsonPropertyName("file_ids")]
    [Description("List of file IDs")]
    public string[]? FileIds { get; init; }
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

public class TruncationStrategy
{
    [JsonPropertyName("type")]
    [Description("The type of truncation strategy")]
    [Required]
    public required string Type { get; init; }

    [JsonPropertyName("last_messages")]
    [Description("The number of last messages to keep")]
    public int? LastMessages { get; init; }
}