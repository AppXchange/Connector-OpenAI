namespace Connector.Endpoints.v1.ChatCompletion.Delete;

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
[Description("Deletes a stored chat completion from OpenAI's API")]
public class DeleteChatCompletionAction : IStandardAction<DeleteChatCompletionActionInput, DeleteChatCompletionActionOutput>
{
    public DeleteChatCompletionActionInput ActionInput { get; set; } = new() { CompletionId = string.Empty };
    public DeleteChatCompletionActionOutput ActionOutput { get; set; } = new() 
    { 
        Object = "chat.completion.deleted",
        Id = string.Empty,
        Deleted = false
    };
    public StandardActionFailure ActionFailure { get; set; } = new();

    public bool CreateRtap => true;
}

public class DeleteChatCompletionActionInput
{
    [JsonPropertyName("completion_id")]
    [Description("The ID of the chat completion to delete")]
    [Required]
    public required string CompletionId { get; init; }
}

public class DeleteChatCompletionActionOutput
{
    [JsonPropertyName("object")]
    [Description("The object type, which is always chat.completion.deleted")]
    [Required]
    public required string Object { get; init; }

    [JsonPropertyName("id")]
    [Description("The ID of the deleted chat completion")]
    [Required]
    public required string Id { get; init; }

    [JsonPropertyName("deleted")]
    [Description("Whether the chat completion was successfully deleted")]
    [Required]
    public required bool Deleted { get; init; }
}
