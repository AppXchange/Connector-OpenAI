using Connector.Client;
using Connector.Endpoints.v1.FineTuningJob;
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

namespace Connector.Endpoints.v1.FineTuningJob.Cancel;

public class CancelFineTuningJobHandler : IActionHandler<CancelFineTuningJobAction>
{
    private readonly ILogger<CancelFineTuningJobHandler> _logger;
    private readonly ApiClient _apiClient;

    public CancelFineTuningJobHandler(
        ILogger<CancelFineTuningJobHandler> logger,
        ApiClient apiClient)
    {
        _logger = logger;
        _apiClient = apiClient;
    }
    
    public async Task<ActionHandlerOutcome> HandleQueuedActionAsync(ActionInstance actionInstance, CancellationToken cancellationToken)
    {
        var input = JsonSerializer.Deserialize<CancelFineTuningJobActionInput>(actionInstance.InputJson);
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
                        Source = new[] { "CancelFineTuningJobHandler" },
                        Text = "Failed to deserialize input"
                    }
                }
            });
        }

        try
        {
            var response = await _apiClient.CancelFineTuningJob(input.FineTuningJobId, cancellationToken)
                .ConfigureAwait(false);

            if (!response.IsSuccessful || response.Data == null)
            {
                _logger.LogError("Failed to cancel fine-tuning job. Status code: {StatusCode}", response.StatusCode);
                return ActionHandlerOutcome.Failed(new StandardActionFailure
                {
                    Code = response.StatusCode.ToString(),
                    Errors = new[]
                    {
                        new Xchange.Connector.SDK.Action.Error
                        {
                            Source = new[] { "CancelFineTuningJobHandler" },
                            Text = "Failed to cancel fine-tuning job"
                        }
                    }
                });
            }

            var operations = new List<SyncOperation>();
            var keyResolver = new DefaultDataObjectKey();
            var key = keyResolver.BuildKeyResolver()(response.Data);
            operations.Add(SyncOperation.CreateSyncOperation(UpdateOperation.Upsert.ToString(), key.UrlPart, key.PropertyNames, response.Data));

            var resultList = new List<CacheSyncCollection>
            {
                new CacheSyncCollection() { DataObjectType = typeof(FineTuningJobDataObject), CacheChanges = operations.ToArray() }
            };

            return ActionHandlerOutcome.Successful(response.Data, resultList);
        }
        catch (HttpRequestException exception)
        {
            var errorSource = new List<string> { "CancelFineTuningJobHandler" };
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
