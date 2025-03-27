namespace Connector.Endpoints.v1.Model;

using Json.Schema.Generation;
using System;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.CacheWriter;

/// <summary>
/// Represents an OpenAI model that can be used with the API
/// </summary>
[PrimaryKey("id", nameof(Id))]
[Description("Represents an OpenAI model that can be used with the API")]
public class ModelDataObject
{
    [JsonPropertyName("id")]
    [Description("The model identifier, which can be referenced in the API endpoints")]
    [Required]
    public required string Id { get; init; }

    [JsonPropertyName("object")]
    [Description("The object type, which is always 'model'")]
    [Required]
    public required string Object { get; init; }

    [JsonPropertyName("created")]
    [Description("The Unix timestamp (in seconds) when the model was created")]
    [Required]
    public required long Created { get; init; }

    [JsonPropertyName("owned_by")]
    [Description("The organization that owns the model")]
    [Required]
    public required string OwnedBy { get; init; }
}