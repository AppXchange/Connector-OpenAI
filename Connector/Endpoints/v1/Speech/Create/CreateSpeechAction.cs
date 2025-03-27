namespace Connector.Endpoints.v1.Speech.Create;

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
[Description("Creates speech from text using OpenAI's API")]
public class CreateSpeechAction : IStandardAction<CreateSpeechActionInput, CreateSpeechActionOutput>
{
    public CreateSpeechActionInput ActionInput { get; set; } = new() 
    { 
        Input = string.Empty,
        Model = "tts-1",
        Voice = "alloy"
    };
    public CreateSpeechActionOutput ActionOutput { get; set; } = new();
    public StandardActionFailure ActionFailure { get; set; } = new();

    public bool CreateRtap => true;
}

public class CreateSpeechActionInput
{
    [JsonPropertyName("input")]
    [Description("The text to generate audio for. Maximum length is 4096 characters")]
    [Required]
    public required string Input { get; init; }

    [JsonPropertyName("model")]
    [Description("One of the available TTS models: tts-1, tts-1-hd or gpt-4o-mini-tts")]
    [Required]
    public required string Model { get; init; }

    [JsonPropertyName("voice")]
    [Description("The voice to use when generating the audio")]
    [Required]
    public required string Voice { get; init; }

    [JsonPropertyName("instructions")]
    [Description("Control the voice of your generated audio with additional instructions")]
    public string? Instructions { get; init; }

    [JsonPropertyName("response_format")]
    [Description("The format to audio in (mp3, opus, aac, flac, wav, or pcm)")]
    public string? ResponseFormat { get; init; }

    [JsonPropertyName("speed")]
    [Description("The speed of the generated audio (0.25 to 4.0)")]
    public double? Speed { get; init; }
}

public class CreateSpeechActionOutput
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("audio")]
    public byte[] Audio { get; set; } = Array.Empty<byte>();

    [JsonPropertyName("model")]
    public string Model { get; set; } = string.Empty;

    [JsonPropertyName("voice")]
    public string Voice { get; set; } = string.Empty;

    [JsonPropertyName("input")]
    public string Input { get; set; } = string.Empty;

    [JsonPropertyName("response_format")]
    public string? ResponseFormat { get; set; }

    [JsonPropertyName("speed")]
    public double? Speed { get; set; }
}
