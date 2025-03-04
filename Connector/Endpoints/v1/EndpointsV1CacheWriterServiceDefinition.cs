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
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text.Json;
using Xchange.Connector.SDK.Abstraction.Change;
using Xchange.Connector.SDK.Abstraction.Hosting;
using Xchange.Connector.SDK.CacheWriter;
using Xchange.Connector.SDK.Hosting.Configuration;

public class EndpointsV1CacheWriterServiceDefinition : BaseCacheWriterServiceDefinition<EndpointsV1CacheWriterConfig>
{
    public override string ModuleId => "endpoints-1";
    public override Type ServiceType => typeof(GenericCacheWriterService<EndpointsV1CacheWriterConfig>);

    public override void ConfigureServiceDependencies(IServiceCollection serviceCollection, string serviceConfigJson)
    {
        var serviceConfig = JsonSerializer.Deserialize<EndpointsV1CacheWriterConfig>(serviceConfigJson);
        serviceCollection.AddSingleton<EndpointsV1CacheWriterConfig>(serviceConfig!);
        serviceCollection.AddSingleton<GenericCacheWriterService<EndpointsV1CacheWriterConfig>>();
        serviceCollection.AddSingleton<ICacheWriterServiceDefinition<EndpointsV1CacheWriterConfig>>(this);
        // Register Data Readers as Singletons
        serviceCollection.AddSingleton<TranscriptionDataReader>();
        serviceCollection.AddSingleton<SpeechDataReader>();
        serviceCollection.AddSingleton<TranslationDataReader>();
        serviceCollection.AddSingleton<ChatCompletionDataReader>();
        serviceCollection.AddSingleton<ChatCompletionListDataReader>();
        serviceCollection.AddSingleton<ChatMessagesDataReader>();
        serviceCollection.AddSingleton<EmbeddingsDataReader>();
        serviceCollection.AddSingleton<FineTuningJobDataReader>();
        serviceCollection.AddSingleton<FineTuningJobsDataReader>();
        serviceCollection.AddSingleton<FineTuningEventsDataReader>();
        serviceCollection.AddSingleton<FineTuningCheckpointsDataReader>();
        serviceCollection.AddSingleton<BatchListDataReader>();
        serviceCollection.AddSingleton<BatchDataReader>();
        serviceCollection.AddSingleton<FileListDataReader>();
        serviceCollection.AddSingleton<FileDataReader>();
        serviceCollection.AddSingleton<FileContentDataReader>();
        serviceCollection.AddSingleton<UploadDataReader>();
        serviceCollection.AddSingleton<ImageDataReader>();
        serviceCollection.AddSingleton<ModelDataReader>();
        serviceCollection.AddSingleton<ModelListDataReader>();
        serviceCollection.AddSingleton<ModerationDataReader>();
    }

    public override IDataObjectChangeDetectorProvider ConfigureChangeDetectorProvider(IChangeDetectorFactory factory, ConnectorDefinition connectorDefinition)
    {
        var options = factory.CreateProviderOptionsWithNoDefaultResolver();
        // Configure Data Object Keys for Data Objects that do not use the default
        this.RegisterKeysForObject<TranscriptionDataObject>(options, connectorDefinition);
        this.RegisterKeysForObject<SpeechDataObject>(options, connectorDefinition);
        this.RegisterKeysForObject<TranslationDataObject>(options, connectorDefinition);
        this.RegisterKeysForObject<ChatCompletionDataObject>(options, connectorDefinition);
        this.RegisterKeysForObject<ChatCompletionListDataObject>(options, connectorDefinition);
        this.RegisterKeysForObject<ChatMessagesDataObject>(options, connectorDefinition);
        this.RegisterKeysForObject<EmbeddingsDataObject>(options, connectorDefinition);
        this.RegisterKeysForObject<FineTuningJobDataObject>(options, connectorDefinition);
        this.RegisterKeysForObject<FineTuningJobsDataObject>(options, connectorDefinition);
        this.RegisterKeysForObject<FineTuningEventsDataObject>(options, connectorDefinition);
        this.RegisterKeysForObject<FineTuningCheckpointsDataObject>(options, connectorDefinition);
        this.RegisterKeysForObject<BatchListDataObject>(options, connectorDefinition);
        this.RegisterKeysForObject<BatchDataObject>(options, connectorDefinition);
        this.RegisterKeysForObject<FileListDataObject>(options, connectorDefinition);
        this.RegisterKeysForObject<FileDataObject>(options, connectorDefinition);
        this.RegisterKeysForObject<FileContentDataObject>(options, connectorDefinition);
        this.RegisterKeysForObject<UploadDataObject>(options, connectorDefinition);
        this.RegisterKeysForObject<ImageDataObject>(options, connectorDefinition);
        this.RegisterKeysForObject<ModelDataObject>(options, connectorDefinition);
        this.RegisterKeysForObject<ModelListDataObject>(options, connectorDefinition);
        this.RegisterKeysForObject<ModerationDataObject>(options, connectorDefinition);
        return factory.CreateProvider(options);
    }

    public override void ConfigureService(ICacheWriterService service, EndpointsV1CacheWriterConfig config)
    {
        var dataReaderSettings = new DataReaderSettings
        {
            DisableDeletes = false,
            UseChangeDetection = true
        };
        // Register Data Reader configurations for the Cache Writer Service
        service.RegisterDataReader<TranscriptionDataReader, TranscriptionDataObject>(ModuleId, config.TranscriptionConfig, dataReaderSettings);
        service.RegisterDataReader<SpeechDataReader, SpeechDataObject>(ModuleId, config.SpeechConfig, dataReaderSettings);
        service.RegisterDataReader<TranslationDataReader, TranslationDataObject>(ModuleId, config.TranslationConfig, dataReaderSettings);
        service.RegisterDataReader<ChatCompletionDataReader, ChatCompletionDataObject>(ModuleId, config.ChatCompletionConfig, dataReaderSettings);
        service.RegisterDataReader<ChatCompletionListDataReader, ChatCompletionListDataObject>(ModuleId, config.ChatCompletionListConfig, dataReaderSettings);
        service.RegisterDataReader<ChatMessagesDataReader, ChatMessagesDataObject>(ModuleId, config.ChatMessagesConfig, dataReaderSettings);
        service.RegisterDataReader<EmbeddingsDataReader, EmbeddingsDataObject>(ModuleId, config.EmbeddingsConfig, dataReaderSettings);
        service.RegisterDataReader<FineTuningJobDataReader, FineTuningJobDataObject>(ModuleId, config.FineTuningJobConfig, dataReaderSettings);
        service.RegisterDataReader<FineTuningJobsDataReader, FineTuningJobsDataObject>(ModuleId, config.FineTuningJobsConfig, dataReaderSettings);
        service.RegisterDataReader<FineTuningEventsDataReader, FineTuningEventsDataObject>(ModuleId, config.FineTuningEventsConfig, dataReaderSettings);
        service.RegisterDataReader<FineTuningCheckpointsDataReader, FineTuningCheckpointsDataObject>(ModuleId, config.FineTuningCheckpointsConfig, dataReaderSettings);
        service.RegisterDataReader<BatchListDataReader, BatchListDataObject>(ModuleId, config.BatchListConfig, dataReaderSettings);
        service.RegisterDataReader<BatchDataReader, BatchDataObject>(ModuleId, config.BatchConfig, dataReaderSettings);
        service.RegisterDataReader<FileListDataReader, FileListDataObject>(ModuleId, config.FileListConfig, dataReaderSettings);
        service.RegisterDataReader<FileDataReader, FileDataObject>(ModuleId, config.FileConfig, dataReaderSettings);
        service.RegisterDataReader<FileContentDataReader, FileContentDataObject>(ModuleId, config.FileContentConfig, dataReaderSettings);
        service.RegisterDataReader<UploadDataReader, UploadDataObject>(ModuleId, config.UploadConfig, dataReaderSettings);
        service.RegisterDataReader<ImageDataReader, ImageDataObject>(ModuleId, config.ImageConfig, dataReaderSettings);
        service.RegisterDataReader<ModelDataReader, ModelDataObject>(ModuleId, config.ModelConfig, dataReaderSettings);
        service.RegisterDataReader<ModelListDataReader, ModelListDataObject>(ModuleId, config.ModelListConfig, dataReaderSettings);
        service.RegisterDataReader<ModerationDataReader, ModerationDataObject>(ModuleId, config.ModerationConfig, dataReaderSettings);
    }
}