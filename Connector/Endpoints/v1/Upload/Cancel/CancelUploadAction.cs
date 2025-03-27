namespace Connector.Endpoints.v1.Upload.Cancel;

using Json.Schema.Generation;
using System;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.Action;

/// <summary>
/// Action for cancelling an existing upload in OpenAI
/// </summary>
[Description("Cancels an existing upload. No parts may be added after an upload is cancelled")]
public class CancelUploadAction : IStandardAction<CancelUploadActionInput, CancelUploadActionOutput>
{
    public CancelUploadActionInput ActionInput { get; set; } = new() 
    { 
        UploadId = string.Empty
    };
    public CancelUploadActionOutput ActionOutput { get; set; } = new()
    {
        Id = string.Empty,
        Object = "upload",
        Bytes = 0,
        CreatedAt = 0,
        ExpiresAt = 0,
        Filename = string.Empty,
        Purpose = string.Empty,
        Status = "cancelled"
    };
    public StandardActionFailure ActionFailure { get; set; } = new();

    public bool CreateRtap => true;
}

/// <summary>
/// Input parameters for cancelling an upload
/// </summary>
public class CancelUploadActionInput
{
    [JsonPropertyName("upload_id")]
    [Description("The ID of the Upload to cancel")]
    [Required]
    public required string UploadId { get; set; }
}

/// <summary>
/// Response from cancelling an upload
/// </summary>
public class CancelUploadActionOutput
{
    [JsonPropertyName("id")]
    [Description("The Upload unique identifier")]
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
    [Description("The status of the Upload, which will be 'cancelled'")]
    [Required]
    public required string Status { get; set; }
}
