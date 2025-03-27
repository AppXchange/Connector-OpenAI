namespace Connector.Endpoints.v1.Speech;

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
[Description("Represents a speech response from OpenAI's API")]
public class SpeechDataObject
{
    [JsonPropertyName("id")]
    [Description("Unique identifier for the speech")]
    [Required]
    public required Guid Id { get; init; }

    [JsonPropertyName("audio")]
    [Description("The generated audio content")]
    [Required]
    public required byte[] Audio { get; init; }

    [JsonPropertyName("model")]
    [Description("The model used to generate the speech")]
    [Required]
    public required string Model { get; init; }

    [JsonPropertyName("voice")]
    [Description("The voice used to generate the speech")]
    [Required]
    public required string Voice { get; init; }

    [JsonPropertyName("input")]
    [Description("The input text that was converted to speech")]
    [Required]
    public required string Input { get; init; }

    [JsonPropertyName("response_format")]
    [Description("The format of the audio output")]
    public string? ResponseFormat { get; init; }

    [JsonPropertyName("speed")]
    [Description("The speed of the generated audio")]
    public double? Speed { get; init; }
}