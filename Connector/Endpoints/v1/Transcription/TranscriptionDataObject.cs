namespace Connector.Endpoints.v1.Transcription;

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
[Description("Represents a transcription response from OpenAI's API")]
public class TranscriptionDataObject
{
    [JsonPropertyName("id")]
    [Description("Unique identifier for the transcription")]
    [Required]
    public required Guid Id { get; init; }

    [JsonPropertyName("text")]
    [Description("The transcribed text")]
    public string Text { get; init; } = string.Empty;

    [JsonPropertyName("duration")]
    [Description("The duration of the input audio in seconds")]
    public double? Duration { get; init; }

    [JsonPropertyName("language")]
    [Description("The language of the input audio")]
    public string? Language { get; init; }

    [JsonPropertyName("segments")]
    [Description("Segments of the transcribed text with timing information")]
    public TranscriptionSegment[]? Segments { get; init; }

    [JsonPropertyName("words")]
    [Description("Extracted words with their timestamps")]
    public TranscriptionWord[]? Words { get; init; }
}

public class TranscriptionSegment
{
    [JsonPropertyName("id")]
    public int Id { get; init; }

    [JsonPropertyName("seek")]
    public int Seek { get; init; }

    [JsonPropertyName("start")]
    public double Start { get; init; }

    [JsonPropertyName("end")]
    public double End { get; init; }

    [JsonPropertyName("text")]
    public string Text { get; init; } = string.Empty;

    [JsonPropertyName("tokens")]
    public int[]? Tokens { get; init; }

    [JsonPropertyName("temperature")]
    public double Temperature { get; init; }

    [JsonPropertyName("avg_logprob")]
    public double AvgLogProb { get; init; }

    [JsonPropertyName("compression_ratio")]
    public double CompressionRatio { get; init; }

    [JsonPropertyName("no_speech_prob")]
    public double NoSpeechProb { get; init; }
}

public class TranscriptionWord
{
    [JsonPropertyName("word")]
    public string Word { get; init; } = string.Empty;

    [JsonPropertyName("start")]
    public double Start { get; init; }

    [JsonPropertyName("end")]
    public double End { get; init; }
}