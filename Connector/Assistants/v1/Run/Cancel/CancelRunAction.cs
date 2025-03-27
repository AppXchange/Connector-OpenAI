namespace Connector.Assistants.v1.Run.Cancel;

using Json.Schema.Generation;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.Action;

/// <summary>
/// Action to cancel a run that is in progress.
/// </summary>
[Description("Cancels a run that is in_progress")]
public class CancelRunAction : IStandardAction<CancelRunActionInput, CancelRunActionOutput>
{
    public CancelRunActionInput ActionInput { get; set; } = new() 
    { 
        ThreadId = string.Empty,
        RunId = string.Empty
    };
    public CancelRunActionOutput ActionOutput { get; set; } = new() 
    { 
        Id = string.Empty,
        Object = "thread.run",
        CreatedAt = 0,
        AssistantId = string.Empty,
        ThreadId = string.Empty,
        Status = "cancelled",
        Model = string.Empty,
        Tools = Array.Empty<Tool>(),
        Metadata = new Dictionary<string, string>(),
        ParallelToolCalls = false
    };
    public StandardActionFailure ActionFailure { get; set; } = new();

    public bool CreateRtap => true;
}

public class CancelRunActionInput
{
    [JsonPropertyName("thread_id")]
    [Description("The ID of the thread to which this run belongs")]
    [Required]
    public required string ThreadId { get; set; }

    [JsonPropertyName("run_id")]
    [Description("The ID of the run to cancel")]
    [Required]
    public required string RunId { get; set; }
}

public class CancelRunActionOutput
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

    [JsonPropertyName("tools")]
    [Description("The list of tools that the assistant used for this run")]
    [Required]
    public required Tool[] Tools { get; set; }

    [JsonPropertyName("tool_resources")]
    [Description("A set of resources that are used by the assistant's tools")]
    public ToolResources? ToolResources { get; set; }

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

    [JsonPropertyName("response_format")]
    [Description("Specifies the format that the model must output")]
    public string? ResponseFormat { get; set; }

    [JsonPropertyName("tool_choice")]
    [Description("Controls which (if any) tool is called by the model")]
    public string? ToolChoice { get; set; }

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

public class Tool
{
    [JsonPropertyName("type")]
    [Description("The type of tool")]
    [Required]
    public required string Type { get; set; }
}

public class ToolResources
{
    [JsonPropertyName("file_search")]
    [Description("Resources for the file_search tool")]
    public FileSearchResources? FileSearch { get; set; }
}

public class FileSearchResources
{
    [JsonPropertyName("vector_store_ids")]
    [Description("List of vector store IDs")]
    public string[]? VectorStoreIds { get; set; }
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
