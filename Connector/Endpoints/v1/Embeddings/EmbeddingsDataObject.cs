namespace Connector.Endpoints.v1.Embeddings;

using Json.Schema.Generation;
using System;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.CacheWriter;

/// <summary>
/// Data object that will represent an object in the Xchange system. This will be converted to a JsonSchema, 
/// so add attributes to the properties to provide any descriptions, titles, ranges, max, min, etc... 
/// These types will be used for validation at runtime to make sure the objects being passed through the system 
/// are properly formed. The schema also helps provide integrators more information for what the values 
/// are intended to be.
/// </summary>
[PrimaryKey("id", nameof(Id))]
//[AlternateKey("alt-key-id", nameof(CompanyId), nameof(EquipmentNumber))]
[Description("Represents an embedding vector from OpenAI's API")]
public class EmbeddingsDataObject
{
    [JsonPropertyName("id")]
    [Description("Example primary key of the object")]
    [Required]
    public required Guid Id { get; init; }

    [JsonPropertyName("object")]
    [Description("The type of this object, which is always 'embedding'")]
    [Required]
    public required string Object { get; init; }

    [JsonPropertyName("embedding")]
    [Description("The embedding vector, which is a list of floats")]
    [Required]
    public required float[] Embedding { get; init; }

    [JsonPropertyName("index")]
    [Description("The index of the embedding in the list of embeddings")]
    [Required]
    public required int Index { get; init; }
}