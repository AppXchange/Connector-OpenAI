namespace Connector.Endpoints.v1.FineTuningEvents;

using Json.Schema.Generation;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.CacheWriter;

/// <summary>
/// Represents a fine-tuning event from OpenAI's API
/// </summary>
[PrimaryKey("id", nameof(Id))]
[Description("Represents an event from a fine-tuning job")]
public class FineTuningEventsDataObject
{
    [JsonPropertyName("id")]
    [Description("The unique identifier for the event")]
    [Required]
    public required string Id { get; init; }

    [JsonPropertyName("object")]
    [Description("The object type, which is always 'fine_tuning.job.event'")]
    [Required]
    public required string Object { get; init; }

    [JsonPropertyName("created_at")]
    [Description("The Unix timestamp (in seconds) for when the event was created")]
    [Required]
    public required long CreatedAt { get; init; }

    [JsonPropertyName("level")]
    [Description("The level of the event (info, warning, error)")]
    [Required]
    public required string Level { get; init; }

    [JsonPropertyName("message")]
    [Description("The message describing the event")]
    [Required]
    public required string Message { get; init; }

    [JsonPropertyName("data")]
    [Description("Additional data associated with the event")]
    public object? Data { get; init; }

    [JsonPropertyName("type")]
    [Description("The type of event")]
    [Required]
    public required string Type { get; init; }
}

public class FineTuningEventsResponse
{
    [JsonPropertyName("object")]
    public string Object { get; set; } = string.Empty;

    [JsonPropertyName("data")]
    public List<FineTuningEventsDataObject> Data { get; set; } = new();

    [JsonPropertyName("has_more")]
    public bool HasMore { get; set; }
}