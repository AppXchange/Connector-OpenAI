namespace Connector.Endpoints.v1.Translation;

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
[Description("Represents a translation response from OpenAI's API")]
public class TranslationDataObject
{
    [JsonPropertyName("id")]
    [Description("Unique identifier for the translation")]
    [Required]
    public required Guid Id { get; init; }

    [JsonPropertyName("text")]
    [Description("The translated text")]
    [Required]
    public required string Text { get; init; }

    [JsonPropertyName("duration")]
    [Description("The duration of the input audio in seconds")]
    public double? Duration { get; init; }

    [JsonPropertyName("language")]
    [Description("The language of the input audio")]
    public string? Language { get; init; }
}