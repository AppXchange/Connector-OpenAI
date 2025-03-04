namespace Connector.Assistants.v1;
using Connector.Assistants.v1.Assistant;
using Connector.Assistants.v1.ListAssistants;
using Connector.Assistants.v1.ListMessages;
using Connector.Assistants.v1.ListRuns;
using Connector.Assistants.v1.ListRunSteps;
using Connector.Assistants.v1.ListVectorStoreFiles;
using Connector.Assistants.v1.ListVectorStoreFilesInBatch;
using Connector.Assistants.v1.ListVectorStores;
using Connector.Assistants.v1.Message;
using Connector.Assistants.v1.Run;
using Connector.Assistants.v1.RunStep;
using Connector.Assistants.v1.Thread;
using Connector.Assistants.v1.VectorStore;
using Connector.Assistants.v1.VectorStoreFile;
using Connector.Assistants.v1.VectorStoreFileInBatch;
using ESR.Hosting.CacheWriter;
using Json.Schema.Generation;

/// <summary>
/// Configuration for the Cache writer for this module. This configuration will be converted to a JsonSchema, 
/// so add attributes to the properties to provide any descriptions, titles, ranges, max, min, etc... 
/// The schema will be used for validation at runtime to make sure the configurations are properly formed. 
/// The schema also helps provide integrators more information for what the values are intended to be.
/// </summary>
[Title("Assistants V1 Cache Writer Configuration")]
[Description("Configuration of the data object caches for the module.")]
public class AssistantsV1CacheWriterConfig
{
    // Data Reader configuration
    public CacheWriterObjectConfig AssistantConfig { get; set; } = new();
    public CacheWriterObjectConfig ListAssistantsConfig { get; set; } = new();
    public CacheWriterObjectConfig ThreadConfig { get; set; } = new();
    public CacheWriterObjectConfig ListMessagesConfig { get; set; } = new();
    public CacheWriterObjectConfig MessageConfig { get; set; } = new();
    public CacheWriterObjectConfig RunConfig { get; set; } = new();
    public CacheWriterObjectConfig ListRunStepsConfig { get; set; } = new();
    public CacheWriterObjectConfig ListRunsConfig { get; set; } = new();
    public CacheWriterObjectConfig RunStepConfig { get; set; } = new();
    public CacheWriterObjectConfig ListVectorStoresConfig { get; set; } = new();
    public CacheWriterObjectConfig VectorStoreConfig { get; set; } = new();
    public CacheWriterObjectConfig ListVectorStoreFilesConfig { get; set; } = new();
    public CacheWriterObjectConfig VectorStoreFileConfig { get; set; } = new();
    public CacheWriterObjectConfig ListVectorStoreFilesInBatchConfig { get; set; } = new();
    public CacheWriterObjectConfig VectorStoreFileInBatchConfig { get; set; } = new();
}