namespace Connector.Assistants.v1.Assistant;

using Json.Schema.Generation;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.CacheWriter;

/// <summary>
/// Data object that represents an OpenAI Assistant in the Xchange system.
/// </summary>
[PrimaryKey("id", nameof(Id))]
//[AlternateKey("alt-key-id", nameof(CompanyId), nameof(EquipmentNumber))]
[Description("Represents an OpenAI Assistant that can call models and use tools.")]
public class AssistantDataObject
{
    [JsonPropertyName("id")]
    [Description("The identifier of the assistant")]
    [Required]
    public required string Id { get; init; }

    [JsonPropertyName("object")]
    [Description("The object type, which is always 'assistant'")]
    [Required]
    public required string Object { get; init; }

    [JsonPropertyName("created_at")]
    [Description("The Unix timestamp (in seconds) for when the assistant was created")]
    [Required]
    public required long CreatedAt { get; init; }

    [JsonPropertyName("name")]
    [Description("The name of the assistant. Maximum length is 256 characters")]
    public string? Name { get; init; }

    [JsonPropertyName("description")]
    [Description("The description of the assistant. Maximum length is 512 characters")]
    public string? Description { get; init; }

    [JsonPropertyName("model")]
    [Description("ID of the model to use")]
    [Required]
    public required string Model { get; init; }

    [JsonPropertyName("instructions")]
    [Description("The system instructions that the assistant uses. Maximum length is 256,000 characters")]
    public string? Instructions { get; init; }

    [JsonPropertyName("tools")]
    [Description("A list of tools enabled on the assistant. Maximum of 128 tools per assistant")]
    [Required]
    public required AssistantTool[] Tools { get; init; }

    [JsonPropertyName("metadata")]
    [Description("Set of 16 key-value pairs that can be attached to an object")]
    [Required]
    public required Dictionary<string, string> Metadata { get; init; }

    [JsonPropertyName("top_p")]
    [Description("An alternative to sampling with temperature, called nucleus sampling")]
    public double? TopP { get; init; }

    [JsonPropertyName("temperature")]
    [Description("What sampling temperature to use, between 0 and 2")]
    public double? Temperature { get; init; }

    [JsonPropertyName("response_format")]
    [Description("Specifies the format that the model must output")]
    public ResponseFormat? ResponseFormat { get; init; }

    [JsonPropertyName("tool_resources")]
    [Description("A set of resources that are used by the assistant's tools")]
    public ToolResources? ToolResources { get; init; }
}

public class AssistantTool
{
    [JsonPropertyName("type")]
    [Description("The type of tool")]
    [Required]
    public required string Type { get; init; }
}

public class ResponseFormat
{
    [JsonPropertyName("type")]
    [Description("The type of response format")]
    [Required]
    public required string Type { get; init; }

    [JsonPropertyName("json_schema")]
    [Description("The JSON schema for structured outputs")]
    public object? JsonSchema { get; init; }
}

public class ToolResources
{
    [JsonPropertyName("file_ids")]
    [Description("List of file IDs for code_interpreter tool")]
    public string[]? FileIds { get; init; }

    [JsonPropertyName("vector_store_ids")]
    [Description("List of vector store IDs for file_search tool")]
    public string[]? VectorStoreIds { get; init; }
}