using Connector.Client;
using Connector.Endpoints.v1.Image;
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

namespace Connector.Endpoints.v1.Image.CreateVariation;

public class CreateVariationImageHandler : IActionHandler<CreateVariationImageAction>
{
    private readonly ILogger<CreateVariationImageHandler> _logger;
    private readonly ApiClient _apiClient;

    public CreateVariationImageHandler(
        ILogger<CreateVariationImageHandler> logger,
        ApiClient apiClient)
    {
        _logger = logger;
        _apiClient = apiClient;
    }
    
    public async Task<ActionHandlerOutcome> HandleQueuedActionAsync(ActionInstance actionInstance, CancellationToken cancellationToken)
    {
        var input = JsonSerializer.Deserialize<CreateVariationImageActionInput>(actionInstance.InputJson);
        if (input == null)
        {
            _logger.LogError("Failed to deserialize input");
            return ActionHandlerOutcome.Failed(new StandardActionFailure
            {
                Code = "400",
                Errors = new[]
                {
                    new Xchange.Connector.SDK.Action.Error
                    {
                        Source = new[] { "CreateVariationImageHandler" },
                        Text = "Failed to deserialize input"
                    }
                }
            });
        }

        try
        {
            var response = await _apiClient.CreateVariationImage(input, cancellationToken)
                .ConfigureAwait(false);

            if (!response.IsSuccessful || response.Data == null)
            {
                _logger.LogError("Failed to create image variation. Status code: {StatusCode}", response.StatusCode);
                return ActionHandlerOutcome.Failed(new StandardActionFailure
                {
                    Code = response.StatusCode.ToString(),
                    Errors = new[]
                    {
                        new Xchange.Connector.SDK.Action.Error
                        {
                            Source = new[] { "CreateVariationImageHandler" },
                            Text = "Failed to create image variation"
                        }
                    }
                });
            }

            var operations = new List<SyncOperation>();
            foreach (var imageData in response.Data.Data)
            {
                var imageObject = new ImageDataObject
                {
                    Id = System.Guid.NewGuid().ToString(),
                    Url = imageData.Url,
                    B64Json = imageData.B64Json,
                    CreatedAt = response.Data.Created
                };

                var keyResolver = new DefaultDataObjectKey();
                var key = keyResolver.BuildKeyResolver()(imageObject);
                operations.Add(SyncOperation.CreateSyncOperation(UpdateOperation.Upsert.ToString(), key.UrlPart, key.PropertyNames, imageObject));
            }

            var resultList = new List<CacheSyncCollection>
            {
                new CacheSyncCollection() { DataObjectType = typeof(ImageDataObject), CacheChanges = operations.ToArray() }
            };

            return ActionHandlerOutcome.Successful(response.Data, resultList);
        }
        catch (HttpRequestException exception)
        {
            var errorSource = new List<string> { "CreateVariationImageHandler" };
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
