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

namespace Connector.Assistants.v1.Run.Create;

public class CreateRunHandler : IActionHandler<CreateRunAction>
{
    private readonly ILogger<CreateRunHandler> _logger;
    private readonly ApiClient _apiClient;

    public CreateRunHandler(
        ILogger<CreateRunHandler> logger,
        ApiClient apiClient)
    {
        _logger = logger;
        _apiClient = apiClient;
    }
    
    public async Task<ActionHandlerOutcome> HandleQueuedActionAsync(ActionInstance actionInstance, CancellationToken cancellationToken)
    {
        var input = JsonSerializer.Deserialize<CreateRunActionInput>(actionInstance.InputJson);
        try
        {
            if (input == null)
            {
                throw new ArgumentException("Input cannot be null");
            }

            if (string.IsNullOrEmpty(input.ThreadId))
            {
                throw new ArgumentException("Thread ID is required");
            }

            if (string.IsNullOrEmpty(input.AssistantId))
            {
                throw new ArgumentException("Assistant ID is required");
            }

            var response = await _apiClient.CreateRun(input, cancellationToken)
                .ConfigureAwait(false);

            if (!response.IsSuccessful || response.Data == null)
            {
                throw new HttpRequestException($"Failed to create run. Status code: {response.StatusCode}");
            }

            var operations = new List<SyncOperation>();
            var keyResolver = new DefaultDataObjectKey();
            var key = keyResolver.BuildKeyResolver()(response.Data);
            operations.Add(SyncOperation.CreateSyncOperation(UpdateOperation.Upsert.ToString(), key.UrlPart, key.PropertyNames, response.Data));

            var resultList = new List<CacheSyncCollection>
            {
                new CacheSyncCollection() { DataObjectType = typeof(RunDataObject), CacheChanges = operations.ToArray() }
            };

            return ActionHandlerOutcome.Successful(response.Data, resultList);
        }
        catch (HttpRequestException exception)
        {
            var errorSource = new List<string> { "CreateRunHandler" };
            if (string.IsNullOrEmpty(exception.Source)) errorSource.Add(exception.Source!);
            
            return ActionHandlerOutcome.Failed(new StandardActionFailure
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
    }
}
