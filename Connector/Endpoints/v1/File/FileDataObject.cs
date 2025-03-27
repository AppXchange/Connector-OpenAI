namespace Connector.Endpoints.v1.File;

using Json.Schema.Generation;
using System;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.CacheWriter;

/// <summary>
/// Represents a file object from OpenAI's API
/// </summary>
[PrimaryKey("id", nameof(Id))]
//[AlternateKey("alt-key-id", nameof(CompanyId), nameof(EquipmentNumber))]
[Description("Represents a file object from OpenAI's API")]
public class FileDataObject
{
    [JsonPropertyName("id")]
    [Description("The file identifier, which can be referenced in the API endpoints")]
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

    [JsonPropertyName("expires_at")]
    [Description("The Unix timestamp (in seconds) for when the file will expire")]
    public long? ExpiresAt { get; init; }

    [JsonPropertyName("filename")]
    [Description("The name of the file")]
    [Required]
    public required string Filename { get; init; }

    [JsonPropertyName("purpose")]
    [Description("The intended purpose of the file")]
    [Required]
    public required string Purpose { get; init; }

    [JsonPropertyName("status")]
    [Description("The current status of the file (deprecated)")]
    public string? Status { get; init; }

    [JsonPropertyName("status_details")]
    [Description("Details on why a fine-tuning training file failed validation (deprecated)")]
    public string? StatusDetails { get; init; }
}