namespace Connector.Endpoints.v1;
using Connector.Endpoints.v1.Batch;
using Connector.Endpoints.v1.Batch.Cancel;
using Connector.Endpoints.v1.Batch.Create;
using Connector.Endpoints.v1.ChatCompletion;
using Connector.Endpoints.v1.ChatCompletion.Create;
using Connector.Endpoints.v1.ChatCompletion.Delete;
using Connector.Endpoints.v1.ChatCompletion.Update;
using Connector.Endpoints.v1.Embeddings;
using Connector.Endpoints.v1.Embeddings.Create;
using Connector.Endpoints.v1.File;
using Connector.Endpoints.v1.File.Delete;
using Connector.Endpoints.v1.File.Upload;
using Connector.Endpoints.v1.FineTuningJob;
using Connector.Endpoints.v1.FineTuningJob.Cancel;
using Connector.Endpoints.v1.FineTuningJob.Create;
using Connector.Endpoints.v1.Image;
using Connector.Endpoints.v1.Image.Create;
using Connector.Endpoints.v1.Image.CreateEdit;
using Connector.Endpoints.v1.Image.CreateVariation;
using Connector.Endpoints.v1.Model;
using Connector.Endpoints.v1.Model.Delete;
using Connector.Endpoints.v1.Moderation;
using Connector.Endpoints.v1.Moderation.Create;
using Connector.Endpoints.v1.Speech;
using Connector.Endpoints.v1.Speech.Create;
using Connector.Endpoints.v1.Transcription;
using Connector.Endpoints.v1.Transcription.Create;
using Connector.Endpoints.v1.Translation;
using Connector.Endpoints.v1.Translation.Create;
using Connector.Endpoints.v1.Upload;
using Connector.Endpoints.v1.Upload.AddPart;
using Connector.Endpoints.v1.Upload.Cancel;
using Connector.Endpoints.v1.Upload.Complete;
using Connector.Endpoints.v1.Upload.Create;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.Abstraction.Hosting;
using Xchange.Connector.SDK.Action;

public class EndpointsV1ActionProcessorServiceDefinition : BaseActionHandlerServiceDefinition<EndpointsV1ActionProcessorConfig>
{
    public override string ModuleId => "endpoints-1";
    public override Type ServiceType => typeof(GenericActionHandlerService<EndpointsV1ActionProcessorConfig>);

    public override void ConfigureServiceDependencies(IServiceCollection serviceCollection, string serviceConfigJson)
    {
        var options = new JsonSerializerOptions
        {
            Converters =
            {
                new JsonStringEnumConverter()
            }
        };
        var serviceConfig = JsonSerializer.Deserialize<EndpointsV1ActionProcessorConfig>(serviceConfigJson, options);
        serviceCollection.AddSingleton<EndpointsV1ActionProcessorConfig>(serviceConfig!);
        serviceCollection.AddSingleton<GenericActionHandlerService<EndpointsV1ActionProcessorConfig>>();
        serviceCollection.AddSingleton<IActionHandlerServiceDefinition<EndpointsV1ActionProcessorConfig>>(this);
        // Register Action Handlers as scoped dependencies
        serviceCollection.AddScoped<CreateTranscriptionHandler>();
        serviceCollection.AddScoped<CreateSpeechHandler>();
        serviceCollection.AddScoped<CreateTranslationHandler>();
        serviceCollection.AddScoped<CreateChatCompletionHandler>();
        serviceCollection.AddScoped<UpdateChatCompletionHandler>();
        serviceCollection.AddScoped<DeleteChatCompletionHandler>();
        serviceCollection.AddScoped<CreateEmbeddingsHandler>();
        serviceCollection.AddScoped<CreateFineTuningJobHandler>();
        serviceCollection.AddScoped<CancelFineTuningJobHandler>();
        serviceCollection.AddScoped<CreateBatchHandler>();
        serviceCollection.AddScoped<CancelBatchHandler>();
        serviceCollection.AddScoped<UploadFileHandler>();
        serviceCollection.AddScoped<DeleteFileHandler>();
        serviceCollection.AddScoped<CreateUploadHandler>();
        serviceCollection.AddScoped<AddPartUploadHandler>();
        serviceCollection.AddScoped<CompleteUploadHandler>();
        serviceCollection.AddScoped<CancelUploadHandler>();
        serviceCollection.AddScoped<CreateImageHandler>();
        serviceCollection.AddScoped<CreateEditImageHandler>();
        serviceCollection.AddScoped<CreateVariationImageHandler>();
        serviceCollection.AddScoped<DeleteModelHandler>();
        serviceCollection.AddScoped<CreateModerationHandler>();
    }

    public override void ConfigureService(IActionHandlerService service, EndpointsV1ActionProcessorConfig config)
    {
        // Register Action Handler configurations for the Action Processor Service
        service.RegisterHandlerForDataObjectAction<CreateTranscriptionHandler, TranscriptionDataObject>(ModuleId, "transcription", "create", config.CreateTranscriptionConfig);
        service.RegisterHandlerForDataObjectAction<CreateSpeechHandler, SpeechDataObject>(ModuleId, "speech", "create", config.CreateSpeechConfig);
        service.RegisterHandlerForDataObjectAction<CreateTranslationHandler, TranslationDataObject>(ModuleId, "translation", "create", config.CreateTranslationConfig);
        service.RegisterHandlerForDataObjectAction<CreateChatCompletionHandler, ChatCompletionDataObject>(ModuleId, "chat-completion", "create", config.CreateChatCompletionConfig);
        service.RegisterHandlerForDataObjectAction<UpdateChatCompletionHandler, ChatCompletionDataObject>(ModuleId, "chat-completion", "update", config.UpdateChatCompletionConfig);
        service.RegisterHandlerForDataObjectAction<DeleteChatCompletionHandler, ChatCompletionDataObject>(ModuleId, "chat-completion", "delete", config.DeleteChatCompletionConfig);
        service.RegisterHandlerForDataObjectAction<CreateEmbeddingsHandler, EmbeddingsDataObject>(ModuleId, "embeddings", "create", config.CreateEmbeddingsConfig);
        service.RegisterHandlerForDataObjectAction<CreateFineTuningJobHandler, FineTuningJobDataObject>(ModuleId, "fine-tuning-job", "create", config.CreateFineTuningJobConfig);
        service.RegisterHandlerForDataObjectAction<CancelFineTuningJobHandler, FineTuningJobDataObject>(ModuleId, "fine-tuning-job", "cancel", config.CancelFineTuningJobConfig);
        service.RegisterHandlerForDataObjectAction<CreateBatchHandler, BatchDataObject>(ModuleId, "batch", "create", config.CreateBatchConfig);
        service.RegisterHandlerForDataObjectAction<CancelBatchHandler, BatchDataObject>(ModuleId, "batch", "cancel", config.CancelBatchConfig);
        service.RegisterHandlerForDataObjectAction<UploadFileHandler, FileDataObject>(ModuleId, "file", "upload", config.UploadFileConfig);
        service.RegisterHandlerForDataObjectAction<DeleteFileHandler, FileDataObject>(ModuleId, "file", "delete", config.DeleteFileConfig);
        service.RegisterHandlerForDataObjectAction<CreateUploadHandler, UploadDataObject>(ModuleId, "upload", "create", config.CreateUploadConfig);
        service.RegisterHandlerForDataObjectAction<AddPartUploadHandler, UploadDataObject>(ModuleId, "upload", "add-part", config.AddPartUploadConfig);
        service.RegisterHandlerForDataObjectAction<CompleteUploadHandler, UploadDataObject>(ModuleId, "upload", "complete", config.CompleteUploadConfig);
        service.RegisterHandlerForDataObjectAction<CancelUploadHandler, UploadDataObject>(ModuleId, "upload", "cancel", config.CancelUploadConfig);
        service.RegisterHandlerForDataObjectAction<CreateImageHandler, ImageDataObject>(ModuleId, "image", "create", config.CreateImageConfig);
        service.RegisterHandlerForDataObjectAction<CreateEditImageHandler, ImageDataObject>(ModuleId, "image", "create-edit", config.CreateEditImageConfig);
        service.RegisterHandlerForDataObjectAction<CreateVariationImageHandler, ImageDataObject>(ModuleId, "image", "create-variation", config.CreateVariationImageConfig);
        service.RegisterHandlerForDataObjectAction<DeleteModelHandler, ModelDataObject>(ModuleId, "model", "delete", config.DeleteModelConfig);
        service.RegisterHandlerForDataObjectAction<CreateModerationHandler, ModerationDataObject>(ModuleId, "moderation", "create", config.CreateModerationConfig);
    }
}