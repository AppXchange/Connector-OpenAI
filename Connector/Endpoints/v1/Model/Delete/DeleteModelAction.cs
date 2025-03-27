using Json.Schema.Generation;
using System;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.Action;

namespace Connector.Endpoints.v1.Model.Delete;

/// <summary>
/// Action for deleting a fine-tuned model from OpenAI
/// </summary>
[Description("Deletes a fine-tuned model from OpenAI. Requires Owner role in the organization.")]
public class DeleteModelAction : IStandardAction<DeleteModelActionInput, DeleteModelActionOutput>
{
    public DeleteModelActionInput ActionInput { get; set; } = new() { Model = string.Empty };
    public DeleteModelActionOutput ActionOutput { get; set; } = new() 
    { 
        Id = string.Empty,
        Object = "model",
        Deleted = false
    };
    public StandardActionFailure ActionFailure { get; set; } = new();

    public bool CreateRtap => true;
}

/// <summary>
/// Input parameters for deleting a model
/// </summary>
public class DeleteModelActionInput
{
    [JsonPropertyName("model")]
    [Description("The model identifier to delete")]
    [Required]
    public required string Model { get; set; }
}

/// <summary>
/// Response from deleting a model
/// </summary>
public class DeleteModelActionOutput
{
    [JsonPropertyName("id")]
    [Description("The identifier of the deleted model")]
    [Required]
    public required string Id { get; set; }

    [JsonPropertyName("object")]
    [Description("The object type, which is always 'model'")]
    [Required]
    public required string Object { get; set; }

    [JsonPropertyName("deleted")]
    [Description("Whether the model was successfully deleted")]
    [Required]
    public required bool Deleted { get; set; }
}
