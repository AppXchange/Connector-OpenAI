namespace Connector.Endpoints.v1.Upload.Create;

using Json.Schema.Generation;
using System;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.Action;

/// <summary>
/// Action for creating a new upload object in OpenAI
/// </summary>
[Description("Creates a new upload object for file uploads")]
public class CreateUploadAction : IStandardAction<CreateUploadActionInput, CreateUploadActionOutput>
{
    public CreateUploadActionInput ActionInput { get; set; } = new() 
    { 
        Bytes = 0,
        Filename = string.Empty,
        MimeType = string.Empty,
        Purpose = string.Empty
    };
    public CreateUploadActionOutput ActionOutput { get; set; } = new()
    {
        Id = string.Empty,
        Object = "upload",
        Bytes = 0,
        CreatedAt = 0,
        ExpiresAt = 0,
        Filename = string.Empty,
        Purpose = string.Empty,
        Status = "pending"
    };
    public StandardActionFailure ActionFailure { get; set; } = new();

    public bool CreateRtap => true;
}

/// <summary>
/// Input parameters for creating a new upload
/// </summary>
public class CreateUploadActionInput
{
    [JsonPropertyName("bytes")]
    [Description("The number of bytes in the file you are uploading")]
    [Required]
    public required long Bytes { get; set; }

    [JsonPropertyName("filename")]
    [Description("The name of the file to upload")]
    [Required]
    public required string Filename { get; set; }

    [JsonPropertyName("mime_type")]
    [Description("The MIME type of the file")]
    [Required]
    public required string MimeType { get; set; }

    [JsonPropertyName("purpose")]
    [Description("The intended purpose of the uploaded file")]
    [Required]
    public required string Purpose { get; set; }
}

/// <summary>
/// Output from creating a new upload
/// </summary>
public class CreateUploadActionOutput
{
    [JsonPropertyName("id")]
    [Description("The upload unique identifier")]
    [Required]
    public required string Id { get; set; }

    [JsonPropertyName("object")]
    [Description("The object type, which is always 'upload'")]
    [Required]
    public required string Object { get; set; }

    [JsonPropertyName("bytes")]
    [Description("The intended number of bytes to be uploaded")]
    [Required]
    public required long Bytes { get; set; }

    [JsonPropertyName("created_at")]
    [Description("The Unix timestamp (in seconds) for when the Upload was created")]
    [Required]
    public required long CreatedAt { get; set; }

    [JsonPropertyName("expires_at")]
    [Description("The Unix timestamp (in seconds) for when the Upload will expire")]
    [Required]
    public required long ExpiresAt { get; set; }

    [JsonPropertyName("filename")]
    [Description("The name of the file to be uploaded")]
    [Required]
    public required string Filename { get; set; }

    [JsonPropertyName("purpose")]
    [Description("The intended purpose of the file")]
    [Required]
    public required string Purpose { get; set; }

    [JsonPropertyName("status")]
    [Description("The status of the Upload")]
    [Required]
    public required string Status { get; set; }
}
