namespace Connector.Endpoints.v1.Image.Create;

using Json.Schema.Generation;
using System;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.Action;

/// <summary>
/// Action for generating images using OpenAI's API
/// </summary>
[Description("Generates images based on a text prompt using OpenAI's API")]
public class CreateImageAction : IStandardAction<CreateImageActionInput, CreateImageActionOutput>
{
    public CreateImageActionInput ActionInput { get; set; } = new() 
    { 
        Prompt = string.Empty
    };
    public CreateImageActionOutput ActionOutput { get; set; } = new()
    {
        Created = 0,
        Data = Array.Empty<ImageData>()
    };
    public StandardActionFailure ActionFailure { get; set; } = new();

    public bool CreateRtap => true;
}

public class CreateImageActionInput
{
    [JsonPropertyName("prompt")]
    [Description("A text description of the desired image(s). The maximum length is 1000 characters for dall-e-2 and 4000 characters for dall-e-3.")]
    [Required]
    public required string Prompt { get; init; }

    [JsonPropertyName("model")]
    [Description("The model to use for image generation. Defaults to dall-e-2")]
    public string? Model { get; init; }

    [JsonPropertyName("n")]
    [Description("The number of images to generate. Must be between 1 and 10. For dall-e-3, only n=1 is supported.")]
    public int? N { get; init; }

    [JsonPropertyName("quality")]
    [Description("The quality of the image that will be generated. hd creates images with finer details and greater consistency across the image. This param is only supported for dall-e-3.")]
    public string? Quality { get; init; }

    [JsonPropertyName("response_format")]
    [Description("The format in which the generated images are returned. Must be one of url or b64_json. URLs are only valid for 60 minutes after the image has been generated.")]
    public string? ResponseFormat { get; init; }

    [JsonPropertyName("size")]
    [Description("The size of the generated images. Must be one of 256x256, 512x512, or 1024x1024 for dall-e-2. Must be one of 1024x1024, 1792x1024, or 1024x1792 for dall-e-3 models.")]
    public string? Size { get; init; }

    [JsonPropertyName("style")]
    [Description("The style of the generated images. Must be one of vivid or natural. Vivid causes the model to lean towards generating hyper-real and dramatic images. Natural causes the model to produce more natural, less hyper-real looking images. This param is only supported for dall-e-3.")]
    public string? Style { get; init; }

    [JsonPropertyName("user")]
    [Description("A unique identifier representing your end-user, which can help OpenAI to monitor and detect abuse.")]
    public string? User { get; init; }
}

public class CreateImageActionOutput
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

    [JsonPropertyName("revised_prompt")]
    [Description("The prompt that was used to generate the image, if there was any revision")]
    public string? RevisedPrompt { get; init; }
}
