namespace Connector.Endpoints.v1.FileContent;

using Json.Schema.Generation;
using System;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.CacheWriter;

/// <summary>
/// Represents the content of a file from OpenAI's API
/// </summary>
[PrimaryKey("id", nameof(Id))]
[Description("Represents the content of a file from OpenAI's API")]
public class FileContentDataObject
{
    [JsonPropertyName("id")]
    [Description("The unique identifier for the file content")]
    [Required]
    public required string Id { get; init; }

    [JsonPropertyName("content")]
    [Description("The actual content of the file")]
    [Required]
    public required string Content { get; init; }

    [JsonPropertyName("file_id")]
    [Description("The ID of the file this content belongs to")]
    [Required]
    public required string FileId { get; init; }

    [JsonPropertyName("created_at")]
    [Description("The Unix timestamp (in seconds) for when the content was retrieved")]
    [Required]
    public required long CreatedAt { get; init; }
}