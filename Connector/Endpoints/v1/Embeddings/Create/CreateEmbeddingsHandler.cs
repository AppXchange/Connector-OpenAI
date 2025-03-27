using Connector.Client;
using ESR.Hosting.Action;
using ESR.Hosting.CacheWriter;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Xchange.Connector.SDK.Action;
using Xchange.Connector.SDK.CacheWriter;
using Xchange.Connector.SDK.Client.AppNetwork;

namespace Connector.Endpoints.v1.Embeddings.Create;

public class CreateEmbeddingsHandler : IActionHandler<CreateEmbeddingsAction>
{
    private readonly ILogger<CreateEmbeddingsHandler> _logger;
    private readonly ApiClient _apiClient;

    public CreateEmbeddingsHandler(
        ILogger<CreateEmbeddingsHandler> logger,
        ApiClient apiClient)
    {
        _logger = logger;
        _apiClient = apiClient;
    }
    
    public async Task<ActionHandlerOutcome> HandleQueuedActionAsync(ActionInstance actionInstance, CancellationToken cancellationToken)
    {
        var input = JsonSerializer.Deserialize<CreateEmbeddingsActionInput>(actionInstance.InputJson);
        if (input == null)
        {
            return ActionHandlerOutcome.Failed(new StandardActionFailure
            {
                Code = "400",
                Errors = new[] { new Error { Source = new[] { nameof(CreateEmbeddingsHandler) }, Text = "Invalid input data" } }
            });
        }

        try
        {
            var response = await _apiClient.CreateEmbeddings(input, cancellationToken)
                .ConfigureAwait(false);

            if (!response.IsSuccessful || response.Data == default)
                return ActionHandlerOutcome.Failed(new StandardActionFailure
                {
                    Code = response.StatusCode.ToString(),
                    Errors = new []
                    {
                        new Error
                        {
                            Source = new [] { nameof(CreateEmbeddingsHandler) },
                            Text = $"Failed to create embeddings with status code {response.StatusCode}"
                        }
                    }
                });

            var operations = new List<SyncOperation>();
            var keyResolver = new DefaultDataObjectKey();
            foreach (var embedding in response.Data.Data)
            {
                var key = keyResolver.BuildKeyResolver()(embedding);
                operations.Add(SyncOperation.CreateSyncOperation(UpdateOperation.Upsert.ToString(), key.UrlPart, key.PropertyNames, embedding));
            }

            var resultList = new List<CacheSyncCollection>
            {
                new CacheSyncCollection() { DataObjectType = typeof(EmbeddingsDataObject), CacheChanges = operations.ToArray() }
            };

            return ActionHandlerOutcome.Successful(response.Data, resultList);
        }
        catch (HttpRequestException exception)
        {
            var errorSource = new List<string> { nameof(CreateEmbeddingsHandler) };
            if (string.IsNullOrEmpty(exception.Source)) errorSource.Add(exception.Source!);
            
            return ActionHandlerOutcome.Failed(new StandardActionFailure
            {
                Code = exception.StatusCode?.ToString() ?? "500",
                Errors = new []
                {
                    new Error
                    {
                        Source = errorSource.ToArray(),
                        Text = exception.Message
                    }
                }
            });
        }
    }
}
