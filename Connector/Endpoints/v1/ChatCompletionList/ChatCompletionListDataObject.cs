namespace Connector.Endpoints.v1.ChatCompletionList;

using Json.Schema.Generation;
using System;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.CacheWriter;
using Connector.Endpoints.v1.ChatCompletion;

/// <summary>
/// Data object that will represent an object in the Xchange system. This will be converted to a JsonSchema, 
/// so add attributes to the properties to provide any descriptions, titles, ranges, max, min, etc... 
/// These types will be used for validation at runtime to make sure the objects being passed through the system 
/// are properly formed. The schema also helps provide integrators more information for what the values 
/// are intended to be.
/// </summary>
[Description("Represents a list of chat completions from OpenAI's API")]
[PrimaryKey("first_id", nameof(FirstId))]
public class ChatCompletionListDataObject
{
    [JsonPropertyName("object")]
    [Description("The type of this object, which is always 'list'")]
    [Required]
    public required string Object { get; init; }

    [JsonPropertyName("data")]
    [Description("An array of chat completion objects")]
    [Required]
    public required ChatCompletionDataObject[] Data { get; init; }

    [JsonPropertyName("first_id")]
    [Description("The identifier of the first chat completion in the data array")]
    [Required]
    public required string FirstId { get; init; }

    [JsonPropertyName("last_id")]
    [Description("The identifier of the last chat completion in the data array")]
    [Required]
    public required string LastId { get; init; }

    [JsonPropertyName("has_more")]
    [Description("Indicates whether there are more Chat Completions available")]
    [Required]
    public required bool HasMore { get; init; }
}