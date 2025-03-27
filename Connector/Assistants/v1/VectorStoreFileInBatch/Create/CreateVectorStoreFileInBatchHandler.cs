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

namespace Connector.Assistants.v1.VectorStoreFileInBatch.Create;

public class CreateVectorStoreFileInBatchHandler : IActionHandler<CreateVectorStoreFileInBatchAction>
{
    private readonly ILogger<CreateVectorStoreFileInBatchHandler> _logger;
    private readonly ApiClient _apiClient;

    public CreateVectorStoreFileInBatchHandler(
        ILogger<CreateVectorStoreFileInBatchHandler> logger,
        ApiClient apiClient)
    {
        _logger = logger;
        _apiClient = apiClient;
    }
    
    public async Task<ActionHandlerOutcome> HandleQueuedActionAsync(ActionInstance actionInstance, CancellationToken cancellationToken)
    {
        var input = JsonSerializer.Deserialize<CreateVectorStoreFileInBatchActionInput>(actionInstance.InputJson);
        if (input == null)
        {
            return ActionHandlerOutcome.Failed(new StandardActionFailure
            {
                Code = "400",
                Errors = new[]
                {
                    new Error
                    {
                        Source = new[] { "CreateVectorStoreFileInBatchHandler" },
                        Text = "Invalid input data"
                    }
                }
            });
        }

        try
        {
            var response = await _apiClient.CreateVectorStoreFileBatch(
                vectorStoreId: input.VectorStoreId,
                input: input,
                cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            if (!response.IsSuccessful || response.Data == null)
            {
                throw new HttpRequestException($"Failed to create vector store file batch. Status code: {response.StatusCode}");
            }

            return ActionHandlerOutcome.Successful(response.Data);
        }
        catch (HttpRequestException exception)
        {
            _logger.LogError(exception, "Error creating vector store file batch");
            
            var errorSource = new List<string> { "CreateVectorStoreFileInBatchHandler" };
            if (!string.IsNullOrEmpty(exception.Source)) errorSource.Add(exception.Source);
            
            return ActionHandlerOutcome.Failed(new StandardActionFailure
            {
                Code = exception.StatusCode?.ToString() ?? "500",
                Errors = new[]
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
