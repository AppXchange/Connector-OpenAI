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
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text.Json;
using Xchange.Connector.SDK.Abstraction.Change;
using Xchange.Connector.SDK.Abstraction.Hosting;
using Xchange.Connector.SDK.CacheWriter;
using Xchange.Connector.SDK.Hosting.Configuration;

public class AssistantsV1CacheWriterServiceDefinition : BaseCacheWriterServiceDefinition<AssistantsV1CacheWriterConfig>
{
    public override string ModuleId => "assistants-1";
    public override Type ServiceType => typeof(GenericCacheWriterService<AssistantsV1CacheWriterConfig>);

    public override void ConfigureServiceDependencies(IServiceCollection serviceCollection, string serviceConfigJson)
    {
        var serviceConfig = JsonSerializer.Deserialize<AssistantsV1CacheWriterConfig>(serviceConfigJson);
        serviceCollection.AddSingleton<AssistantsV1CacheWriterConfig>(serviceConfig!);
        serviceCollection.AddSingleton<GenericCacheWriterService<AssistantsV1CacheWriterConfig>>();
        serviceCollection.AddSingleton<ICacheWriterServiceDefinition<AssistantsV1CacheWriterConfig>>(this);
        // Register Data Readers as Singletons
        serviceCollection.AddSingleton<AssistantDataReader>();
        serviceCollection.AddSingleton<ListAssistantsDataReader>();
        serviceCollection.AddSingleton<ThreadDataReader>();
        serviceCollection.AddSingleton<ListMessagesDataReader>();
        serviceCollection.AddSingleton<MessageDataReader>();
        serviceCollection.AddSingleton<RunDataReader>();
        serviceCollection.AddSingleton<ListRunStepsDataReader>();
        serviceCollection.AddSingleton<ListRunsDataReader>();
        serviceCollection.AddSingleton<RunStepDataReader>();
        serviceCollection.AddSingleton<ListVectorStoresDataReader>();
        serviceCollection.AddSingleton<VectorStoreDataReader>();
        serviceCollection.AddSingleton<ListVectorStoreFilesDataReader>();
        serviceCollection.AddSingleton<VectorStoreFileDataReader>();
        serviceCollection.AddSingleton<ListVectorStoreFilesInBatchDataReader>();
        serviceCollection.AddSingleton<VectorStoreFileInBatchDataReader>();
    }

    public override IDataObjectChangeDetectorProvider ConfigureChangeDetectorProvider(IChangeDetectorFactory factory, ConnectorDefinition connectorDefinition)
    {
        var options = factory.CreateProviderOptionsWithNoDefaultResolver();
        // Configure Data Object Keys for Data Objects that do not use the default
        this.RegisterKeysForObject<AssistantDataObject>(options, connectorDefinition);
        this.RegisterKeysForObject<ListAssistantsDataObject>(options, connectorDefinition);
        this.RegisterKeysForObject<ThreadDataObject>(options, connectorDefinition);
        this.RegisterKeysForObject<ListMessagesDataObject>(options, connectorDefinition);
        this.RegisterKeysForObject<MessageDataObject>(options, connectorDefinition);
        this.RegisterKeysForObject<RunDataObject>(options, connectorDefinition);
        this.RegisterKeysForObject<ListRunStepsDataObject>(options, connectorDefinition);
        this.RegisterKeysForObject<ListRunsDataObject>(options, connectorDefinition);
        this.RegisterKeysForObject<RunStepDataObject>(options, connectorDefinition);
        this.RegisterKeysForObject<ListVectorStoresDataObject>(options, connectorDefinition);
        this.RegisterKeysForObject<VectorStoreDataObject>(options, connectorDefinition);
        this.RegisterKeysForObject<ListVectorStoreFilesDataObject>(options, connectorDefinition);
        this.RegisterKeysForObject<VectorStoreFileDataObject>(options, connectorDefinition);
        this.RegisterKeysForObject<ListVectorStoreFilesInBatchDataObject>(options, connectorDefinition);
        this.RegisterKeysForObject<VectorStoreFileInBatchDataObject>(options, connectorDefinition);
        return factory.CreateProvider(options);
    }

    public override void ConfigureService(ICacheWriterService service, AssistantsV1CacheWriterConfig config)
    {
        var dataReaderSettings = new DataReaderSettings
        {
            DisableDeletes = false,
            UseChangeDetection = true
        };
        // Register Data Reader configurations for the Cache Writer Service
        service.RegisterDataReader<AssistantDataReader, AssistantDataObject>(ModuleId, config.AssistantConfig, dataReaderSettings);
        service.RegisterDataReader<ListAssistantsDataReader, ListAssistantsDataObject>(ModuleId, config.ListAssistantsConfig, dataReaderSettings);
        service.RegisterDataReader<ThreadDataReader, ThreadDataObject>(ModuleId, config.ThreadConfig, dataReaderSettings);
        service.RegisterDataReader<ListMessagesDataReader, ListMessagesDataObject>(ModuleId, config.ListMessagesConfig, dataReaderSettings);
        service.RegisterDataReader<MessageDataReader, MessageDataObject>(ModuleId, config.MessageConfig, dataReaderSettings);
        service.RegisterDataReader<RunDataReader, RunDataObject>(ModuleId, config.RunConfig, dataReaderSettings);
        service.RegisterDataReader<ListRunStepsDataReader, ListRunStepsDataObject>(ModuleId, config.ListRunStepsConfig, dataReaderSettings);
        service.RegisterDataReader<ListRunsDataReader, ListRunsDataObject>(ModuleId, config.ListRunsConfig, dataReaderSettings);
        service.RegisterDataReader<RunStepDataReader, RunStepDataObject>(ModuleId, config.RunStepConfig, dataReaderSettings);
        service.RegisterDataReader<ListVectorStoresDataReader, ListVectorStoresDataObject>(ModuleId, config.ListVectorStoresConfig, dataReaderSettings);
        service.RegisterDataReader<VectorStoreDataReader, VectorStoreDataObject>(ModuleId, config.VectorStoreConfig, dataReaderSettings);
        service.RegisterDataReader<ListVectorStoreFilesDataReader, ListVectorStoreFilesDataObject>(ModuleId, config.ListVectorStoreFilesConfig, dataReaderSettings);
        service.RegisterDataReader<VectorStoreFileDataReader, VectorStoreFileDataObject>(ModuleId, config.VectorStoreFileConfig, dataReaderSettings);
        service.RegisterDataReader<ListVectorStoreFilesInBatchDataReader, ListVectorStoreFilesInBatchDataObject>(ModuleId, config.ListVectorStoreFilesInBatchConfig, dataReaderSettings);
        service.RegisterDataReader<VectorStoreFileInBatchDataReader, VectorStoreFileInBatchDataObject>(ModuleId, config.VectorStoreFileInBatchConfig, dataReaderSettings);
    }
}