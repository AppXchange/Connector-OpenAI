namespace Connector.Endpoints.v1.ChatMessages;

using Json.Schema.Generation;
using System;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.CacheWriter;

/// <summary>
/// Data object that represents a list of chat completion messages from OpenAI's API
/// </summary>
[Description("Represents a list of chat completion messages from OpenAI's API")]
[PrimaryKey("first_id", nameof(FirstId))]
public class ChatMessagesDataObject
{
    [JsonPropertyName("object")]
    [Description("The type of this object, which is always 'list'")]
    [Required]
    public required string Object { get; init; }

    [JsonPropertyName("data")]
    [Description("An array of chat completion message objects")]
    [Required]
    public required ChatMessage[] Data { get; init; }

    [JsonPropertyName("first_id")]
    [Description("The identifier of the first chat message in the data array")]
    [Required]
    public required string FirstId { get; init; }

    [JsonPropertyName("last_id")]
    [Description("The identifier of the last chat message in the data array")]
    [Required]
    public required string LastId { get; init; }

    [JsonPropertyName("has_more")]
    [Description("Indicates whether there are more chat messages available")]
    [Required]
    public required bool HasMore { get; init; }
}

public class ChatMessage
{
    [JsonPropertyName("id")]
    [Description("A unique identifier for the chat message")]
    [Required]
    public required string Id { get; init; }

    [JsonPropertyName("role")]
    [Description("The role of the message sender (e.g., user, assistant)")]
    [Required]
    public required string Role { get; init; }

    [JsonPropertyName("content")]
    [Description("The content of the message")]
    [Required]
    public required string Content { get; init; }

    [JsonPropertyName("name")]
    [Description("The name of the message sender")]
    public string? Name { get; init; }

    [JsonPropertyName("content_parts")]
    [Description("Additional content parts for the message")]
    public object? ContentParts { get; init; }
}