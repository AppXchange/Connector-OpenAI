namespace Connector.Assistants.v1;
using Connector.Assistants.v1.Assistant.Create;
using Connector.Assistants.v1.Assistant.Delete;
using Connector.Assistants.v1.Assistant.Modify;
using Connector.Assistants.v1.Message.Create;
using Connector.Assistants.v1.Message.Delete;
using Connector.Assistants.v1.Message.Modify;
using Connector.Assistants.v1.Run.Cancel;
using Connector.Assistants.v1.Run.Create;
using Connector.Assistants.v1.Run.CreateThread;
using Connector.Assistants.v1.Run.Modify;
using Connector.Assistants.v1.Run.SubmitToolOutputs;
using Connector.Assistants.v1.Thread.Create;
using Connector.Assistants.v1.Thread.Delete;
using Connector.Assistants.v1.Thread.Modify;
using Connector.Assistants.v1.VectorStore.Create;
using Connector.Assistants.v1.VectorStore.Delete;
using Connector.Assistants.v1.VectorStore.Modify;
using Connector.Assistants.v1.VectorStoreFile.Create;
using Connector.Assistants.v1.VectorStoreFile.Delete;
using Connector.Assistants.v1.VectorStoreFile.FileContent;
using Connector.Assistants.v1.VectorStoreFileInBatch.Cancel;
using Connector.Assistants.v1.VectorStoreFileInBatch.Create;
using Json.Schema.Generation;
using Xchange.Connector.SDK.Action;

/// <summary>
/// Configuration for the Action Processor for this module. This configuration will be converted to a JsonSchema, 
/// so add attributes to the properties to provide any descriptions, titles, ranges, max, min, etc... 
/// The schema will be used for validation at runtime to make sure the configurations are properly formed. 
/// The schema also helps provide integrators more information for what the values are intended to be.
/// </summary>
[Title("Assistants V1 Action Processor Configuration")]
[Description("Configuration of the data object actions for the module.")]
public class AssistantsV1ActionProcessorConfig
{
    // Action Handler configuration
    public DefaultActionHandlerConfig CreateAssistantConfig { get; set; } = new();
    public DefaultActionHandlerConfig ModifyAssistantConfig { get; set; } = new();
    public DefaultActionHandlerConfig DeleteAssistantConfig { get; set; } = new();
    public DefaultActionHandlerConfig CreateThreadConfig { get; set; } = new();
    public DefaultActionHandlerConfig ModifyThreadConfig { get; set; } = new();
    public DefaultActionHandlerConfig DeleteThreadConfig { get; set; } = new();
    public DefaultActionHandlerConfig CreateMessageConfig { get; set; } = new();
    public DefaultActionHandlerConfig ModifyMessageConfig { get; set; } = new();
    public DefaultActionHandlerConfig DeleteMessageConfig { get; set; } = new();
    public DefaultActionHandlerConfig CreateRunConfig { get; set; } = new();
    public DefaultActionHandlerConfig ModifyRunConfig { get; set; } = new();
    public DefaultActionHandlerConfig CancelRunConfig { get; set; } = new();
    public DefaultActionHandlerConfig CreateThreadRunConfig { get; set; } = new();
    public DefaultActionHandlerConfig CreateVectorStoreConfig { get; set; } = new();
    public DefaultActionHandlerConfig ModifyVectorStoreConfig { get; set; } = new();
    public DefaultActionHandlerConfig DeleteVectorStoreConfig { get; set; } = new();
    public DefaultActionHandlerConfig CreateVectorStoreFileConfig { get; set; } = new();
    public DefaultActionHandlerConfig DeleteVectorStoreFileConfig { get; set; } = new();
    public DefaultActionHandlerConfig CreateVectorStoreFileInBatchConfig { get; set; } = new();
    public DefaultActionHandlerConfig CancelVectorStoreFileInBatchConfig { get; set; } = new();
    public DefaultActionHandlerConfig SubmitToolOutputsRunConfig { get; set; } = new();
    public DefaultActionHandlerConfig FileContentVectorStoreFileConfig { get; set; } = new();
}