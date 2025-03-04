namespace Connector.Endpoints.v1;
using Connector.Endpoints.v1.Batch.Cancel;
using Connector.Endpoints.v1.Batch.Create;
using Connector.Endpoints.v1.ChatCompletion.Create;
using Connector.Endpoints.v1.ChatCompletion.Delete;
using Connector.Endpoints.v1.ChatCompletion.Update;
using Connector.Endpoints.v1.Embeddings.Create;
using Connector.Endpoints.v1.File.Delete;
using Connector.Endpoints.v1.File.Upload;
using Connector.Endpoints.v1.FineTuningJob.Cancel;
using Connector.Endpoints.v1.FineTuningJob.Create;
using Connector.Endpoints.v1.Image.Create;
using Connector.Endpoints.v1.Image.CreateEdit;
using Connector.Endpoints.v1.Image.CreateVariation;
using Connector.Endpoints.v1.Model.Delete;
using Connector.Endpoints.v1.Moderation.Create;
using Connector.Endpoints.v1.Speech.Create;
using Connector.Endpoints.v1.Transcription.Create;
using Connector.Endpoints.v1.Translation.Create;
using Connector.Endpoints.v1.Upload.AddPart;
using Connector.Endpoints.v1.Upload.Cancel;
using Connector.Endpoints.v1.Upload.Complete;
using Connector.Endpoints.v1.Upload.Create;
using Json.Schema.Generation;
using Xchange.Connector.SDK.Action;

/// <summary>
/// Configuration for the Action Processor for this module. This configuration will be converted to a JsonSchema, 
/// so add attributes to the properties to provide any descriptions, titles, ranges, max, min, etc... 
/// The schema will be used for validation at runtime to make sure the configurations are properly formed. 
/// The schema also helps provide integrators more information for what the values are intended to be.
/// </summary>
[Title("Endpoints V1 Action Processor Configuration")]
[Description("Configuration of the data object actions for the module.")]
public class EndpointsV1ActionProcessorConfig
{
    // Action Handler configuration
    public DefaultActionHandlerConfig CreateTranscriptionConfig { get; set; } = new();
    public DefaultActionHandlerConfig CreateSpeechConfig { get; set; } = new();
    public DefaultActionHandlerConfig CreateTranslationConfig { get; set; } = new();
    public DefaultActionHandlerConfig CreateChatCompletionConfig { get; set; } = new();
    public DefaultActionHandlerConfig UpdateChatCompletionConfig { get; set; } = new();
    public DefaultActionHandlerConfig DeleteChatCompletionConfig { get; set; } = new();
    public DefaultActionHandlerConfig CreateEmbeddingsConfig { get; set; } = new();
    public DefaultActionHandlerConfig CreateFineTuningJobConfig { get; set; } = new();
    public DefaultActionHandlerConfig CancelFineTuningJobConfig { get; set; } = new();
    public DefaultActionHandlerConfig CreateBatchConfig { get; set; } = new();
    public DefaultActionHandlerConfig CancelBatchConfig { get; set; } = new();
    public DefaultActionHandlerConfig UploadFileConfig { get; set; } = new();
    public DefaultActionHandlerConfig DeleteFileConfig { get; set; } = new();
    public DefaultActionHandlerConfig CreateUploadConfig { get; set; } = new();
    public DefaultActionHandlerConfig AddPartUploadConfig { get; set; } = new();
    public DefaultActionHandlerConfig CompleteUploadConfig { get; set; } = new();
    public DefaultActionHandlerConfig CancelUploadConfig { get; set; } = new();
    public DefaultActionHandlerConfig CreateImageConfig { get; set; } = new();
    public DefaultActionHandlerConfig CreateEditImageConfig { get; set; } = new();
    public DefaultActionHandlerConfig CreateVariationImageConfig { get; set; } = new();
    public DefaultActionHandlerConfig DeleteModelConfig { get; set; } = new();
    public DefaultActionHandlerConfig CreateModerationConfig { get; set; } = new();
}