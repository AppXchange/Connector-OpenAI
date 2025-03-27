namespace Connector.Assistants.v1;
using Connector.Assistants.v1.Assistant;
using Connector.Assistants.v1.Assistant.Create;
using Connector.Assistants.v1.Assistant.Delete;
using Connector.Assistants.v1.Assistant.Modify;
using Connector.Assistants.v1.Message;
using Connector.Assistants.v1.Message.Create;
using Connector.Assistants.v1.Message.Delete;
using Connector.Assistants.v1.Message.Modify;
using Connector.Assistants.v1.Run;
using Connector.Assistants.v1.Run.Cancel;
using Connector.Assistants.v1.Run.Create;
using Connector.Assistants.v1.Run.CreateThread;
using Connector.Assistants.v1.Run.Modify;
using Connector.Assistants.v1.Run.SubmitToolOutputs;
using Connector.Assistants.v1.Thread;
using Connector.Assistants.v1.Thread.Create;
using Connector.Assistants.v1.Thread.Delete;
using Connector.Assistants.v1.Thread.Modify;
using Connector.Assistants.v1.VectorStore;
using Connector.Assistants.v1.VectorStore.Create;
using Connector.Assistants.v1.VectorStore.Delete;
using Connector.Assistants.v1.VectorStore.Modify;
using Connector.Assistants.v1.VectorStoreFile;
using Connector.Assistants.v1.VectorStoreFile.Create;
using Connector.Assistants.v1.VectorStoreFile.Delete;
using Connector.Assistants.v1.VectorStoreFile.FileContent;
using Connector.Assistants.v1.VectorStoreFileInBatch;
using Connector.Assistants.v1.VectorStoreFileInBatch.Cancel;
using Connector.Assistants.v1.VectorStoreFileInBatch.Create;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Xchange.Connector.SDK.Abstraction.Hosting;
using Xchange.Connector.SDK.Action;

public class AssistantsV1ActionProcessorServiceDefinition : BaseActionHandlerServiceDefinition<AssistantsV1ActionProcessorConfig>
{
    public override string ModuleId => "assistants-1";
    public override Type ServiceType => typeof(GenericActionHandlerService<AssistantsV1ActionProcessorConfig>);

    public override void ConfigureServiceDependencies(IServiceCollection serviceCollection, string serviceConfigJson)
    {
        var options = new JsonSerializerOptions
        {
            Converters =
            {
                new JsonStringEnumConverter()
            }
        };
        var serviceConfig = JsonSerializer.Deserialize<AssistantsV1ActionProcessorConfig>(serviceConfigJson, options);
        serviceCollection.AddSingleton<AssistantsV1ActionProcessorConfig>(serviceConfig!);
        serviceCollection.AddSingleton<GenericActionHandlerService<AssistantsV1ActionProcessorConfig>>();
        serviceCollection.AddSingleton<IActionHandlerServiceDefinition<AssistantsV1ActionProcessorConfig>>(this);
        // Register Action Handlers as scoped dependencies
        serviceCollection.AddScoped<CreateAssistantHandler>();
        serviceCollection.AddScoped<ModifyAssistantHandler>();
        serviceCollection.AddScoped<DeleteAssistantHandler>();
        serviceCollection.AddScoped<CreateThreadHandler>();
        serviceCollection.AddScoped<ModifyThreadHandler>();
        serviceCollection.AddScoped<DeleteThreadHandler>();
        serviceCollection.AddScoped<CreateMessageHandler>();
        serviceCollection.AddScoped<ModifyMessageHandler>();
        serviceCollection.AddScoped<DeleteMessageHandler>();
        serviceCollection.AddScoped<CreateRunHandler>();
        serviceCollection.AddScoped<ModifyRunHandler>();
        serviceCollection.AddScoped<CancelRunHandler>();
        serviceCollection.AddScoped<CreateThreadRunHandler>();
        serviceCollection.AddScoped<CreateVectorStoreHandler>();
        serviceCollection.AddScoped<ModifyVectorStoreHandler>();
        serviceCollection.AddScoped<DeleteVectorStoreHandler>();
        serviceCollection.AddScoped<CreateVectorStoreFileHandler>();
        serviceCollection.AddScoped<DeleteVectorStoreFileHandler>();
        serviceCollection.AddScoped<CreateVectorStoreFileInBatchHandler>();
        serviceCollection.AddScoped<CancelVectorStoreFileInBatchHandler>();
        serviceCollection.AddScoped<SubmitToolOutputsRunHandler>();
        serviceCollection.AddScoped<FileContentVectorStoreFileHandler>();
    }

    public override void ConfigureService(IActionHandlerService service, AssistantsV1ActionProcessorConfig config)
    {
        // Register Action Handler configurations for the Action Processor Service
        service.RegisterHandlerForDataObjectAction<CreateAssistantHandler, AssistantDataObject>(ModuleId, "assistant", "create", config.CreateAssistantConfig);
        service.RegisterHandlerForDataObjectAction<ModifyAssistantHandler, AssistantDataObject>(ModuleId, "assistant", "modify", config.ModifyAssistantConfig);
        service.RegisterHandlerForDataObjectAction<DeleteAssistantHandler, AssistantDataObject>(ModuleId, "assistant", "delete", config.DeleteAssistantConfig);
        service.RegisterHandlerForDataObjectAction<CreateThreadHandler, ThreadDataObject>(ModuleId, "thread", "create", config.CreateThreadConfig);
        service.RegisterHandlerForDataObjectAction<ModifyThreadHandler, ThreadDataObject>(ModuleId, "thread", "modify", config.ModifyThreadConfig);
        service.RegisterHandlerForDataObjectAction<DeleteThreadHandler, ThreadDataObject>(ModuleId, "thread", "delete", config.DeleteThreadConfig);
        service.RegisterHandlerForDataObjectAction<CreateMessageHandler, MessageDataObject>(ModuleId, "message", "create", config.CreateMessageConfig);
        service.RegisterHandlerForDataObjectAction<ModifyMessageHandler, MessageDataObject>(ModuleId, "message", "modify", config.ModifyMessageConfig);
        service.RegisterHandlerForDataObjectAction<DeleteMessageHandler, MessageDataObject>(ModuleId, "message", "delete", config.DeleteMessageConfig);
        service.RegisterHandlerForDataObjectAction<CreateRunHandler, RunDataObject>(ModuleId, "run", "create", config.CreateRunConfig);
        service.RegisterHandlerForDataObjectAction<ModifyRunHandler, RunDataObject>(ModuleId, "run", "modify", config.ModifyRunConfig);
        service.RegisterHandlerForDataObjectAction<CancelRunHandler, RunDataObject>(ModuleId, "run", "cancel", config.CancelRunConfig);
        service.RegisterHandlerForDataObjectAction<CreateThreadRunHandler, RunDataObject>(ModuleId, "run", "create-thread", config.CreateThreadRunConfig);
        service.RegisterHandlerForDataObjectAction<CreateVectorStoreHandler, VectorStoreDataObject>(ModuleId, "vector-store", "create", config.CreateVectorStoreConfig);
        service.RegisterHandlerForDataObjectAction<ModifyVectorStoreHandler, VectorStoreDataObject>(ModuleId, "vector-store", "modify", config.ModifyVectorStoreConfig);
        service.RegisterHandlerForDataObjectAction<DeleteVectorStoreHandler, VectorStoreDataObject>(ModuleId, "vector-store", "delete", config.DeleteVectorStoreConfig);
        service.RegisterHandlerForDataObjectAction<CreateVectorStoreFileHandler, VectorStoreFileDataObject>(ModuleId, "vector-store-file", "create", config.CreateVectorStoreFileConfig);
        service.RegisterHandlerForDataObjectAction<DeleteVectorStoreFileHandler, VectorStoreFileDataObject>(ModuleId, "vector-store-file", "delete", config.DeleteVectorStoreFileConfig);
        service.RegisterHandlerForDataObjectAction<CreateVectorStoreFileInBatchHandler, VectorStoreFileInBatchDataObject>(ModuleId, "vector-store-file-in-batch", "create", config.CreateVectorStoreFileInBatchConfig);
        service.RegisterHandlerForDataObjectAction<CancelVectorStoreFileInBatchHandler, VectorStoreFileInBatchDataObject>(ModuleId, "vector-store-file-in-batch", "cancel", config.CancelVectorStoreFileInBatchConfig);
        service.RegisterHandlerForDataObjectAction<SubmitToolOutputsRunHandler, RunDataObject>(ModuleId, "run", "submit-tool-outputs", config.SubmitToolOutputsRunConfig);
        service.RegisterHandlerForDataObjectAction<FileContentVectorStoreFileHandler, VectorStoreFileDataObject>(ModuleId, "vector-store-file", "file-content", config.FileContentVectorStoreFileConfig);
    }
}