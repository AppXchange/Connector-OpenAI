namespace Connector.Endpoints.v1.Transcription.Create;

using Json.Schema.Generation;
using System;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.Action;

/// <summary>
/// Action object that will represent an action in the Xchange system. This will contain an input object type,
/// an output object type, and a Action failure type (this will default to <see cref="StandardActionFailure"/>
/// but that can be overridden with your own preferred type). These objects will be converted to a JsonSchema, 
/// so add attributes to the properties to provide any descriptions, titles, ranges, max, min, etc... 
/// These types will be used for validation at runtime to make sure the objects being passed through the system 
/// are properly formed. The schema also helps provide integrators more information for what the values 
/// are intended to be.
/// </summary>
[Description("Creates a transcription from an audio file using OpenAI's API")]
public class CreateTranscriptionAction : IStandardAction<CreateTranscriptionActionInput, CreateTranscriptionActionOutput>
{
    public CreateTranscriptionActionInput ActionInput { get; set; } = new();
    public CreateTranscriptionActionOutput ActionOutput { get; set; } = new();
    public StandardActionFailure ActionFailure { get; set; } = new();

    public bool CreateRtap => true;
}

public class CreateTranscriptionActionInput
{
    [JsonPropertyName("file")]
    [Description("The audio file to transcribe")]
    [Required]
    public byte[] File { get; init; } = Array.Empty<byte>();

    [JsonPropertyName("model")]
    [Description("ID of the model to use (gpt-4o-transcribe, gpt-4o-mini-transcribe, or whisper-1)")]
    [Required]
    public string Model { get; init; } = "whisper-1";

    [JsonPropertyName("language")]
    [Description("The language of the input audio in ISO-639-1 format")]
    public string? Language { get; init; }

    [JsonPropertyName("prompt")]
    [Description("Optional text to guide the model's style or continue a previous audio segment")]
    public string? Prompt { get; init; }

    [JsonPropertyName("response_format")]
    [Description("The format of the output (json, text, srt, verbose_json, or vtt)")]
    public string? ResponseFormat { get; init; }

    [JsonPropertyName("temperature")]
    [Description("The sampling temperature, between 0 and 1")]
    public double? Temperature { get; init; }

    [JsonPropertyName("timestamp_granularities")]
    [Description("The timestamp granularities to populate (word, segment)")]
    public string[]? TimestampGranularities { get; init; }
}

public class CreateTranscriptionActionOutput
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("text")]
    public string Text { get; set; } = string.Empty;

    [JsonPropertyName("duration")]
    public double? Duration { get; set; }

    [JsonPropertyName("language")]
    public string? Language { get; set; }

    [JsonPropertyName("segments")]
    public TranscriptionSegment[]? Segments { get; set; }

    [JsonPropertyName("words")]
    public TranscriptionWord[]? Words { get; set; }
}
