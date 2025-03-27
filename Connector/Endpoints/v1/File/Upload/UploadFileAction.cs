namespace Connector.Endpoints.v1.File.Upload;

using Json.Schema.Generation;
using System;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.Action;

/// <summary>
/// Action for uploading a file to OpenAI's API
/// </summary>
[Description("Uploads a file that can be used across various OpenAI endpoints")]
public class UploadFileAction : IStandardAction<UploadFileActionInput, UploadFileActionOutput>
{
    public UploadFileActionInput ActionInput { get; set; } = new()
    {
        File = Array.Empty<byte>(),
        Filename = string.Empty,
        Purpose = string.Empty
    };
    public UploadFileActionOutput ActionOutput { get; set; } = new()
    {
        Id = string.Empty,
        Object = "file",
        Bytes = 0,
        CreatedAt = 0,
        Filename = string.Empty,
        Purpose = string.Empty
    };
    public StandardActionFailure ActionFailure { get; set; } = new();

    public bool CreateRtap => true;
}

public class UploadFileActionInput
{
    [JsonPropertyName("file")]
    [Description("The file content to be uploaded")]
    [Required]
    public required byte[] File { get; set; }

    [JsonPropertyName("filename")]
    [Description("The name of the file")]
    [Required]
    public required string Filename { get; set; }

    [JsonPropertyName("purpose")]
    [Description("The intended purpose of the uploaded file")]
    [Required]
    public required string Purpose { get; set; }
}

public class UploadFileActionOutput
{
    [JsonPropertyName("id")]
    [Description("The file identifier, which can be referenced in the API endpoints")]
    [Required]
    public required string Id { get; set; }

    [JsonPropertyName("object")]
    [Description("The object type, which is always 'file'")]
    [Required]
    public required string Object { get; set; }

    [JsonPropertyName("bytes")]
    [Description("The size of the file, in bytes")]
    [Required]
    public required int Bytes { get; set; }

    [JsonPropertyName("created_at")]
    [Description("The Unix timestamp (in seconds) for when the file was created")]
    [Required]
    public required long CreatedAt { get; set; }

    [JsonPropertyName("filename")]
    [Description("The name of the file")]
    [Required]
    public required string Filename { get; set; }

    [JsonPropertyName("purpose")]
    [Description("The intended purpose of the file")]
    [Required]
    public required string Purpose { get; set; }
}
