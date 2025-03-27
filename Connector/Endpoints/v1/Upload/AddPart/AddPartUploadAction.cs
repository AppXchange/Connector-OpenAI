namespace Connector.Endpoints.v1.Upload.AddPart;

using Json.Schema.Generation;
using System;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.Action;

/// <summary>
/// Action for adding a part to an existing upload in OpenAI
/// </summary>
[Description("Adds a part (chunk of bytes) to an existing upload object")]
public class AddPartUploadAction : IStandardAction<AddPartUploadActionInput, AddPartUploadActionOutput>
{
    public AddPartUploadActionInput ActionInput { get; set; } = new() 
    { 
        UploadId = string.Empty,
        Data = Array.Empty<byte>()
    };
    public AddPartUploadActionOutput ActionOutput { get; set; } = new()
    {
        Id = string.Empty,
        Object = "upload.part",
        CreatedAt = 0,
        UploadId = string.Empty
    };
    public StandardActionFailure ActionFailure { get; set; } = new();

    public bool CreateRtap => true;
}

/// <summary>
/// Input parameters for adding a part to an upload
/// </summary>
public class AddPartUploadActionInput
{
    [JsonPropertyName("upload_id")]
    [Description("The ID of the Upload to add the part to")]
    [Required]
    public required string UploadId { get; set; }

    [JsonPropertyName("data")]
    [Description("The chunk of bytes for this Part (max 64MB)")]
    [Required]
    public required byte[] Data { get; set; }
}

/// <summary>
/// Response from adding a part to an upload
/// </summary>
public class AddPartUploadActionOutput
{
    [JsonPropertyName("id")]
    [Description("The upload Part unique identifier")]
    [Required]
    public required string Id { get; set; }

    [JsonPropertyName("object")]
    [Description("The object type, which is always 'upload.part'")]
    [Required]
    public required string Object { get; set; }

    [JsonPropertyName("created_at")]
    [Description("The Unix timestamp (in seconds) for when the Part was created")]
    [Required]
    public required long CreatedAt { get; set; }

    [JsonPropertyName("upload_id")]
    [Description("The ID of the Upload object that this Part was added to")]
    [Required]
    public required string UploadId { get; set; }
}
