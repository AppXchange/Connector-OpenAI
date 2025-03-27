namespace Connector.Endpoints.v1.ModelList;

using Json.Schema.Generation;
using System;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.CacheWriter;

/// <summary>
/// Represents a list of OpenAI models that can be used with the API
/// </summary>
[Description("Represents a list of OpenAI models that can be used with the API")]
[PrimaryKey("object", nameof(Object))]
public class ModelListDataObject
{
    [JsonPropertyName("object")]
    [Description("The object type, which is always 'list'")]
    [Required]
    public required string Object { get; init; }

    [JsonPropertyName("data")]
    [Description("The list of available models")]
    [Required]
    public required ModelData[] Data { get; init; }
}

/// <summary>
/// Represents a single model in the list
/// </summary>
public class ModelData
{
    [JsonPropertyName("id")]
    [Description("The model identifier")]
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