namespace Connector.Endpoints.v1.Image.CreateEdit;

using Json.Schema.Generation;
using System;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.Action;

/// <summary>
/// Action for editing images using OpenAI's API
/// </summary>
[Description("Creates an edited or extended image given an original image and a prompt")]
public class CreateEditImageAction : IStandardAction<CreateEditImageActionInput, CreateEditImageActionOutput>
{
    public CreateEditImageActionInput ActionInput { get; set; } = new() 
    { 
        Prompt = string.Empty,
        Image = Array.Empty<byte>()
    };
    public CreateEditImageActionOutput ActionOutput { get; set; } = new()
    {
        Created = 0,
        Data = Array.Empty<ImageData>()
    };
    public StandardActionFailure ActionFailure { get; set; } = new();

    public bool CreateRtap => true;
}

public class CreateEditImageActionInput
{
    [JsonPropertyName("image")]
    [Description("The image to edit. Must be a valid PNG file, less than 4MB, and square")]
    [Required]
    public required byte[] Image { get; init; }

    [JsonPropertyName("prompt")]
    [Description("A text description of the desired image(s). The maximum length is 1000 characters")]
    [Required]
    public required string Prompt { get; init; }

    [JsonPropertyName("mask")]
    [Description("An additional image whose fully transparent areas indicate where image should be edited")]
    public byte[]? Mask { get; init; }

    [JsonPropertyName("model")]
    [Description("The model to use for image generation. Only dall-e-2 is supported at this time")]
    public string? Model { get; init; }

    [JsonPropertyName("n")]
    [Description("The number of images to generate. Must be between 1 and 10")]
    public int? N { get; init; }

    [JsonPropertyName("response_format")]
    [Description("The format in which the generated images are returned. Must be one of url or b64_json")]
    public string? ResponseFormat { get; init; }

    [JsonPropertyName("size")]
    [Description("The size of the generated images. Must be one of 256x256, 512x512, or 1024x1024")]
    public string? Size { get; init; }

    [JsonPropertyName("user")]
    [Description("A unique identifier representing your end-user")]
    public string? User { get; init; }
}

public class CreateEditImageActionOutput
{
    [JsonPropertyName("created")]
    [Description("The Unix timestamp (in seconds) for when the images were created")]
    [Required]
    public required long Created { get; init; }

    [JsonPropertyName("data")]
    [Description("The list of generated images")]
    [Required]
    public required ImageData[] Data { get; init; }
}

public class ImageData
{
    [JsonPropertyName("url")]
    [Description("The URL of the generated image")]
    public string? Url { get; init; }

    [JsonPropertyName("b64_json")]
    [Description("The base64-encoded JSON of the generated image")]
    public string? B64Json { get; init; }
}
