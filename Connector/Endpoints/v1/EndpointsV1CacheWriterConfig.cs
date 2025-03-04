namespace Connector.Endpoints.v1;
using Connector.Endpoints.v1.Batch;
using Connector.Endpoints.v1.BatchList;
using Connector.Endpoints.v1.ChatCompletion;
using Connector.Endpoints.v1.ChatCompletionList;
using Connector.Endpoints.v1.ChatMessages;
using Connector.Endpoints.v1.Embeddings;
using Connector.Endpoints.v1.File;
using Connector.Endpoints.v1.FileContent;
using Connector.Endpoints.v1.FileList;
using Connector.Endpoints.v1.FineTuningCheckpoints;
using Connector.Endpoints.v1.FineTuningEvents;
using Connector.Endpoints.v1.FineTuningJob;
using Connector.Endpoints.v1.FineTuningJobs;
using Connector.Endpoints.v1.Image;
using Connector.Endpoints.v1.Model;
using Connector.Endpoints.v1.ModelList;
using Connector.Endpoints.v1.Moderation;
using Connector.Endpoints.v1.Speech;
using Connector.Endpoints.v1.Transcription;
using Connector.Endpoints.v1.Translation;
using Connector.Endpoints.v1.Upload;
using ESR.Hosting.CacheWriter;
using Json.Schema.Generation;

/// <summary>
/// Configuration for the Cache writer for this module. This configuration will be converted to a JsonSchema, 
/// so add attributes to the properties to provide any descriptions, titles, ranges, max, min, etc... 
/// The schema will be used for validation at runtime to make sure the configurations are properly formed. 
/// The schema also helps provide integrators more information for what the values are intended to be.
/// </summary>
[Title("Endpoints V1 Cache Writer Configuration")]
[Description("Configuration of the data object caches for the module.")]
public class EndpointsV1CacheWriterConfig
{
    // Data Reader configuration
    public CacheWriterObjectConfig TranscriptionConfig { get; set; } = new();
    public CacheWriterObjectConfig SpeechConfig { get; set; } = new();
    public CacheWriterObjectConfig TranslationConfig { get; set; } = new();
    public CacheWriterObjectConfig ChatCompletionConfig { get; set; } = new();
    public CacheWriterObjectConfig ChatCompletionListConfig { get; set; } = new();
    public CacheWriterObjectConfig ChatMessagesConfig { get; set; } = new();
    public CacheWriterObjectConfig EmbeddingsConfig { get; set; } = new();
    public CacheWriterObjectConfig FineTuningJobConfig { get; set; } = new();
    public CacheWriterObjectConfig FineTuningJobsConfig { get; set; } = new();
    public CacheWriterObjectConfig FineTuningEventsConfig { get; set; } = new();
    public CacheWriterObjectConfig FineTuningCheckpointsConfig { get; set; } = new();
    public CacheWriterObjectConfig BatchListConfig { get; set; } = new();
    public CacheWriterObjectConfig BatchConfig { get; set; } = new();
    public CacheWriterObjectConfig FileListConfig { get; set; } = new();
    public CacheWriterObjectConfig FileConfig { get; set; } = new();
    public CacheWriterObjectConfig FileContentConfig { get; set; } = new();
    public CacheWriterObjectConfig UploadConfig { get; set; } = new();
    public CacheWriterObjectConfig ImageConfig { get; set; } = new();
    public CacheWriterObjectConfig ModelConfig { get; set; } = new();
    public CacheWriterObjectConfig ModelListConfig { get; set; } = new();
    public CacheWriterObjectConfig ModerationConfig { get; set; } = new();
}