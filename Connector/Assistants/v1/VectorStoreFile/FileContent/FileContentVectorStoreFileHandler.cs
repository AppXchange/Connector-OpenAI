using Connector.Client;
using ESR.Hosting.Action;
using ESR.Hosting.CacheWriter;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Xchange.Connector.SDK.Action;
using Xchange.Connector.SDK.CacheWriter;
using Xchange.Connector.SDK.Client.AppNetwork;

namespace Connector.Assistants.v1.VectorStoreFile.FileContent;

public class FileContentVectorStoreFileHandler : IActionHandler<FileContentVectorStoreFileAction>
{
    private readonly ILogger<FileContentVectorStoreFileHandler> _logger;
    private readonly ApiClient _apiClient;

    public FileContentVectorStoreFileHandler(
        ILogger<FileContentVectorStoreFileHandler> logger,
        ApiClient apiClient)
    {
        _logger = logger;
        _apiClient = apiClient;
    }
    
    public async Task<ActionHandlerOutcome> HandleQueuedActionAsync(ActionInstance actionInstance, CancellationToken cancellationToken)
    {
        var input = JsonSerializer.Deserialize<FileContentVectorStoreFileActionInput>(actionInstance.InputJson);
        ActionHandlerOutcome result;

        try
        {
            if (input == null)
            {
                throw new ArgumentException("Input cannot be null");
            }

            var response = await _apiClient.GetVectorStoreFileContent(
                vectorStoreId: input.VectorStoreId,
                fileId: input.FileId,
                cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            if (!response.IsSuccessful || response.Data == null)
            {
                throw new HttpRequestException($"Failed to get vector store file content. Status code: {response.StatusCode}");
            }

            var operations = new List<SyncOperation>();
            var keyResolver = new DefaultDataObjectKey();
            var key = keyResolver.BuildKeyResolver()(response.Data);
            operations.Add(SyncOperation.CreateSyncOperation(UpdateOperation.Upsert.ToString(), key.UrlPart, key.PropertyNames, response.Data));

            var resultList = new List<CacheSyncCollection>
            {
                new CacheSyncCollection() { DataObjectType = typeof(VectorStoreFileDataObject), CacheChanges = operations.ToArray() }
            };

            result = ActionHandlerOutcome.Successful(response.Data, resultList);
        }
        catch (HttpRequestException exception)
        {
            var errorSource = new List<string> { "FileContentVectorStoreFileHandler" };
            if (!string.IsNullOrEmpty(exception.Source))
            {
                errorSource.Add(exception.Source);
            }
            
            result = ActionHandlerOutcome.Failed(new StandardActionFailure
            {
                Code = exception.StatusCode?.ToString() ?? "500",
                Errors = new []
                {
                    new Xchange.Connector.SDK.Action.Error
                    {
                        Source = errorSource.ToArray(),
                        Text = exception.Message
                    }
                }
            });
        }

        return result;
    }
}
