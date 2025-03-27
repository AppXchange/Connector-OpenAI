namespace Connector.Endpoints.v1.Upload;

using Json.Schema.Generation;
using System;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.CacheWriter;

/// <summary>
/// Represents an upload object in OpenAI's API for handling file uploads
/// </summary>
[PrimaryKey("id", nameof(Id))]
[Description("Represents an upload object in OpenAI's API for handling file uploads")]
public class UploadDataObject
{
    [JsonPropertyName("id")]
    [Description("The Upload unique identifier, which can be referenced in API endpoints")]
    [Required]
    public required string Id { get; init; }

    [JsonPropertyName("object")]
    [Description("The object type, which is always 'upload'")]
    [Required]
    public required string Object { get; init; }

    [JsonPropertyName("bytes")]
    [Description("The intended number of bytes to be uploaded")]
    [Required]
    public required long Bytes { get; init; }

    [JsonPropertyName("created_at")]
    [Description("The Unix timestamp (in seconds) for when the Upload was created")]
    [Required]
    public required long CreatedAt { get; init; }

    [JsonPropertyName("expires_at")]
    [Description("The Unix timestamp (in seconds) for when the Upload will expire")]
    [Required]
    public required long ExpiresAt { get; init; }

    [JsonPropertyName("filename")]
    [Description("The name of the file to be uploaded")]
    [Required]
    public required string Filename { get; init; }

    [JsonPropertyName("purpose")]
    [Description("The intended purpose of the file")]
    [Required]
    public required string Purpose { get; init; }

    [JsonPropertyName("status")]
    [Description("The status of the Upload")]
    [Required]
    public required string Status { get; init; }

    [JsonPropertyName("file")]
    [Description("The ready File object after the Upload is completed")]
    public FileData? File { get; init; }
}

/// <summary>
/// Represents a file object that is created after an upload is completed
/// </summary>
public class FileData
{
    [JsonPropertyName("id")]
    [Description("The file unique identifier")]
    [Required]
    public required string Id { get; init; }

    [JsonPropertyName("object")]
    [Description("The object type, which is always 'file'")]
    [Required]
    public required string Object { get; init; }

    [JsonPropertyName("bytes")]
    [Description("The size of the file in bytes")]
    [Required]
    public required long Bytes { get; init; }

    [JsonPropertyName("created_at")]
    [Description("The Unix timestamp (in seconds) when the file was created")]
    [Required]
    public required long CreatedAt { get; init; }

    [JsonPropertyName("filename")]
    [Description("The name of the file")]
    [Required]
    public required string Filename { get; init; }

    [JsonPropertyName("purpose")]
    [Description("The intended purpose of the file")]
    [Required]
    public required string Purpose { get; init; }
}

/// <summary>
/// Represents a part of an upload, which is a chunk of bytes that can be added to an Upload object
/// </summary>
[PrimaryKey("id", nameof(Id))]
[Description("Represents a part of an upload, which is a chunk of bytes that can be added to an Upload object")]
public class UploadPartDataObject
{
    [JsonPropertyName("id")]
    [Description("The upload Part unique identifier, which can be referenced in API endpoints")]
    [Required]
    public required string Id { get; init; }

    [JsonPropertyName("object")]
    [Description("The object type, which is always 'upload.part'")]
    [Required]
    public required string Object { get; init; }

    [JsonPropertyName("created_at")]
    [Description("The Unix timestamp (in seconds) for when the Part was created")]
    [Required]
    public required long CreatedAt { get; init; }

    [JsonPropertyName("upload_id")]
    [Description("The ID of the Upload object that this Part was added to")]
    [Required]
    public required string UploadId { get; init; }
}