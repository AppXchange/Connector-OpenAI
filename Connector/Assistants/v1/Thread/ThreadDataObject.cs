namespace Connector.Assistants.v1.Thread;

using Json.Schema.Generation;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.CacheWriter;

/// <summary>
/// Data object that represents an OpenAI Thread in the Xchange system.
/// </summary>
[PrimaryKey("id", nameof(Id))]
//[AlternateKey("alt-key-id", nameof(CompanyId), nameof(EquipmentNumber))]
[Description("Represents a thread that contains messages")]
public class ThreadDataObject
{
    [JsonPropertyName("id")]
    [Description("The identifier, which can be referenced in API endpoints")]
    [Required]
    public required string Id { get; init; }

    [JsonPropertyName("object")]
    [Description("The object type, which is always 'thread'")]
    [Required]
    public required string Object { get; init; }

    [JsonPropertyName("created_at")]
    [Description("The Unix timestamp (in seconds) for when the thread was created")]
    [Required]
    public required long CreatedAt { get; init; }

    [JsonPropertyName("metadata")]
    [Description("Set of 16 key-value pairs that can be attached to an object")]
    [Required]
    public required Dictionary<string, string> Metadata { get; init; }

    [JsonPropertyName("tool_resources")]
    [Description("A set of resources that are made available to the assistant's tools in this thread")]
    public ToolResources? ToolResources { get; init; }
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
    [Description("List of file IDs for code_interpreter tool")]
    public string[]? FileIds { get; init; }
}