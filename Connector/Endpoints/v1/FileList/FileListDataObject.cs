namespace Connector.Endpoints.v1.FileList;

using Json.Schema.Generation;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.CacheWriter;

/// <summary>
/// Represents a list of files from OpenAI's API
/// </summary>
[PrimaryKey("id", nameof(Id))]
//[AlternateKey("alt-key-id", nameof(CompanyId), nameof(EquipmentNumber))]
[Description("Represents a list of files from OpenAI's API")]
public class FileListDataObject
{
    [JsonPropertyName("id")]
    [Description("The unique identifier for the file")]
    [Required]
    public required string Id { get; init; }

    [JsonPropertyName("object")]
    [Description("The object type, which is always 'file'")]
    [Required]
    public required string Object { get; init; }

    [JsonPropertyName("bytes")]
    [Description("The size of the file, in bytes")]
    [Required]
    public required int Bytes { get; init; }

    [JsonPropertyName("created_at")]
    [Description("The Unix timestamp (in seconds) for when the file was created")]
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

public class FileListResponse
{
    [JsonPropertyName("data")]
    public List<FileListDataObject> Data { get; set; } = new();

    [JsonPropertyName("object")]
    public string Object { get; set; } = string.Empty;
}