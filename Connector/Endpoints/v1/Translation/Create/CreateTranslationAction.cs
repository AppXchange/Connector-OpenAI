namespace Connector.Endpoints.v1.Translation.Create;

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
[Description("Creates a translation from an audio file using OpenAI's API")]
public class CreateTranslationAction : IStandardAction<CreateTranslationActionInput, CreateTranslationActionOutput>
{
    public CreateTranslationActionInput ActionInput { get; set; } = new();
    public CreateTranslationActionOutput ActionOutput { get; set; } = new();
    public StandardActionFailure ActionFailure { get; set; } = new();

    public bool CreateRtap => true;
}

public class CreateTranslationActionInput
{
    [JsonPropertyName("file")]
    [Description("The audio file to translate")]
    [Required]
    public byte[] File { get; init; } = Array.Empty<byte>();

    [JsonPropertyName("model")]
    [Description("ID of the model to use (whisper-1)")]
    [Required]
    public string Model { get; init; } = "whisper-1";

    [JsonPropertyName("prompt")]
    [Description("Optional text to guide the model's style or continue a previous audio segment")]
    public string? Prompt { get; init; }

    [JsonPropertyName("response_format")]
    [Description("The format of the output (json, text, srt, verbose_json, or vtt)")]
    public string? ResponseFormat { get; init; }

    [JsonPropertyName("temperature")]
    [Description("The sampling temperature, between 0 and 1")]
    public double? Temperature { get; init; }
}

public class CreateTranslationActionOutput
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("text")]
    public string Text { get; set; } = string.Empty;

    [JsonPropertyName("duration")]
    public double? Duration { get; set; }

    [JsonPropertyName("language")]
    public string? Language { get; set; }
}
