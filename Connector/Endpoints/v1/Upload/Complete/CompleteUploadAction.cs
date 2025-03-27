namespace Connector.Endpoints.v1.Upload.Complete;

using Json.Schema.Generation;
using System;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.Action;

/// <summary>
/// Action for completing an existing upload in OpenAI
/// </summary>
[Description("Completes an existing upload. The number of bytes uploaded must match the number of bytes initially specified. No parts may be added after an upload is completed.")]
public class CompleteUploadAction : IStandardAction<CompleteUploadActionInput, CompleteUploadActionOutput>
{
    public CompleteUploadActionInput ActionInput { get; set; } = new() 
    { 
        UploadId = string.Empty,
        PartIds = Array.Empty<string>()
    };
    public CompleteUploadActionOutput ActionOutput { get; set; } = new()
    {
        Id = string.Empty,
        Object = "upload",
        Bytes = 0,
        CreatedAt = 0,
        ExpiresAt = 0,
        Filename = string.Empty,
        Purpose = string.Empty,
        Status = "completed",
        File = null
    };
    public StandardActionFailure ActionFailure { get; set; } = new();

    public bool CreateRtap => true;
}

/// <summary>
/// Input parameters for completing an upload
/// </summary>
public class CompleteUploadActionInput
{
    [JsonPropertyName("upload_id")]
    [Description("The ID of the Upload to complete")]
    [Required]
    public required string UploadId { get; set; }

    [JsonPropertyName("part_ids")]
    [Description("The ordered list of Part IDs")]
    [Required]
    public required string[] PartIds { get; set; }

    [JsonPropertyName("md5")]
    [Description("Optional MD5 checksum for the file contents to verify if the bytes uploaded matches what you expect")]
    public string? Md5 { get; set; }
}

/// <summary>
/// Response from completing an upload
/// </summary>
public class CompleteUploadActionOutput
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
    [Description("The number of bytes in the file")]
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
    [Description("The name of the file")]
    [Required]
    public required string Filename { get; set; }

    [JsonPropertyName("purpose")]
    [Description("The intended purpose of the file")]
    [Required]
    public required string Purpose { get; set; }

    [JsonPropertyName("status")]
    [Description("The status of the Upload, which will be 'completed'")]
    [Required]
    public required string Status { get; set; }

    [JsonPropertyName("file")]
    [Description("The ready File object after the Upload is completed")]
    public FileData? File { get; set; }
}

/// <summary>
/// Represents a file object that is created after an upload is completed
/// </summary>
public class FileData
{
    [JsonPropertyName("id")]
    [Description("The file unique identifier")]
    [Required]
    public required string Id { get; set; }

    [JsonPropertyName("object")]
    [Description("The object type, which is always 'file'")]
    [Required]
    public required string Object { get; set; }

    [JsonPropertyName("bytes")]
    [Description("The size of the file in bytes")]
    [Required]
    public required long Bytes { get; set; }

    [JsonPropertyName("created_at")]
    [Description("The Unix timestamp (in seconds) when the file was created")]
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
