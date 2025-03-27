namespace Connector.Endpoints.v1.Embeddings.Create;

using Json.Schema.Generation;
using System;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.Action;

/// <summary>
/// Action object that will represent an action in the Xchange system. This will contain an input object type,
/// an output object type, and a Action failure type (this will default to <see cref="StandardActionFailure"/>
/// but that can be overridden with your own preferred type). These objects will be converted to a JsonSchema, 
/// so add attributes to the properties to provide any descriptions, titles, ranges, max, min, etc... 
/// These types will be used for validation at runtime to make sure the objects being passed through the system 
/// are properly formed. The schema also helps provide integrators more information for what the values 
/// are intended to be.
/// </summary>
[Description("Creates an embedding vector representing the input text")]
public class CreateEmbeddingsAction : IStandardAction<CreateEmbeddingsActionInput, CreateEmbeddingsActionOutput>
{
    public CreateEmbeddingsActionInput ActionInput { get; set; } = new() 
    { 
        Input = string.Empty,
        Model = "text-embedding-ada-002"
    };
    public CreateEmbeddingsActionOutput ActionOutput { get; set; } = new()
    {
        Object = "list",
        Data = Array.Empty<EmbeddingsDataObject>(),
        Model = "text-embedding-ada-002",
        Usage = new Usage
        {
            PromptTokens = 0,
            TotalTokens = 0
        }
    };
    public StandardActionFailure ActionFailure { get; set; } = new();

    public bool CreateRtap => true;
}

public class CreateEmbeddingsActionInput
{
    [JsonPropertyName("input")]
    [Description("Input text to embed, encoded as a string or array of tokens")]
    [Required]
    public required string Input { get; set; }

    [JsonPropertyName("model")]
    [Description("ID of the model to use")]
    [Required]
    public required string Model { get; set; }

    [JsonPropertyName("dimensions")]
    [Description("The number of dimensions the resulting output embeddings should have")]
    public int? Dimensions { get; set; }

    [JsonPropertyName("encoding_format")]
    [Description("The format to return the embeddings in. Can be either float or base64")]
    public string? EncodingFormat { get; set; }

    [JsonPropertyName("user")]
    [Description("A unique identifier representing your end-user")]
    public string? User { get; set; }
}

public class CreateEmbeddingsActionOutput
{
    [JsonPropertyName("object")]
    [Description("The type of this object, which is always 'list'")]
    [Required]
    public required string Object { get; set; }

    [JsonPropertyName("data")]
    [Description("The list of embedding objects")]
    [Required]
    public required EmbeddingsDataObject[] Data { get; set; }

    [JsonPropertyName("model")]
    [Description("The model used for the embedding")]
    [Required]
    public required string Model { get; set; }

    [JsonPropertyName("usage")]
    [Description("Usage statistics for the request")]
    [Required]
    public required Usage Usage { get; set; }
}

public class Usage
{
    [JsonPropertyName("prompt_tokens")]
    [Description("Number of tokens in the prompt")]
    [Required]
    public required int PromptTokens { get; set; }

    [JsonPropertyName("total_tokens")]
    [Description("Total number of tokens used")]
    [Required]
    public required int TotalTokens { get; set; }
}
