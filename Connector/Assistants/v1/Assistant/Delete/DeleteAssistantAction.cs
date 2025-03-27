namespace Connector.Assistants.v1.Assistant.Delete;

using Json.Schema.Generation;
using System;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.Action;

/// <summary>
/// Action to delete an OpenAI Assistant.
/// </summary>
[Description("Deletes an OpenAI Assistant by its ID")]
public class DeleteAssistantAction : IStandardAction<DeleteAssistantActionInput, DeleteAssistantActionOutput>
{
    public DeleteAssistantActionInput ActionInput { get; set; } = new() { AssistantId = string.Empty };
    public DeleteAssistantActionOutput ActionOutput { get; set; } = new() 
    { 
        Id = string.Empty,
        Object = "assistant.deleted",
        Deleted = false
    };
    public StandardActionFailure ActionFailure { get; set; } = new();

    public bool CreateRtap => true;
}

public class DeleteAssistantActionInput
{
    [JsonPropertyName("assistant_id")]
    [Description("The ID of the assistant to delete")]
    [Required]
    public required string AssistantId { get; set; }
}

public class DeleteAssistantActionOutput
{
    [JsonPropertyName("id")]
    [Description("The ID of the deleted assistant")]
    [Required]
    public required string Id { get; set; }

    [JsonPropertyName("object")]
    [Description("The object type, which is always 'assistant.deleted'")]
    [Required]
    public required string Object { get; set; }

    [JsonPropertyName("deleted")]
    [Description("Whether the assistant was successfully deleted")]
    [Required]
    public required bool Deleted { get; set; }
}
