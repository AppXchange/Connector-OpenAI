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

namespace Connector.Endpoints.v1.ChatCompletion.Update;

public class UpdateChatCompletionHandler : IActionHandler<UpdateChatCompletionAction>
{
    private readonly ILogger<UpdateChatCompletionHandler> _logger;

    public UpdateChatCompletionHandler(
        ILogger<UpdateChatCompletionHandler> logger)
    {
        _logger = logger;
    }
    
    public async Task<ActionHandlerOutcome> HandleQueuedActionAsync(ActionInstance actionInstance, CancellationToken cancellationToken)
    {
        var input = JsonSerializer.Deserialize<UpdateChatCompletionActionInput>(actionInstance.InputJson);
        try
        {
            // Given the input for the action, make a call to your API/system
            var response = new ApiResponse<UpdateChatCompletionActionOutput>();
            // response = await _apiClient.PostChatCompletionDataObject(input, cancellationToken)
            // .ConfigureAwait(false);

            // The full record is needed for SyncOperations. If the endpoint used for the action returns a partial record (such as only returning the ID) then you can either:
            // - Make a GET call using the ID that was returned
            // - Add the ID property to your action input (Assuming this results in the proper data object shape)

            // var resource = await _apiClient.GetChatCompletionDataObject(response.Data.id, cancellationToken);

            // var resource = new UpdateChatCompletionActionOutput
            // {
            //      TODO : map
            // };

            // If the response is already the output object for the action, you can use the response directly

            // Build sync operations to update the local cache as well as the Xchange cache system (if the data type is cached)
            // For more information on SyncOperations and the KeyResolver, check: https://trimble-xchange.github.io/connector-docs/guides/creating-actions/#keyresolver-and-the-sync-cache-operations
            var operations = new List<SyncOperation>();
            var keyResolver = new DefaultDataObjectKey();
            var key = keyResolver.BuildKeyResolver()(response.Data);
            operations.Add(SyncOperation.CreateSyncOperation(UpdateOperation.Upsert.ToString(), key.UrlPart, key.PropertyNames, response.Data));

            var resultList = new List<CacheSyncCollection>
            {
                new CacheSyncCollection() { DataObjectType = typeof(ChatCompletionDataObject), CacheChanges = operations.ToArray() }
            };

            return ActionHandlerOutcome.Successful(response.Data, resultList);
        }
        catch (HttpRequestException exception)
        {
            // If an error occurs, we want to create a failure result for the action that matches
            // the failure type for the action. 
            // Common to create extension methods to map to Standard Action Failure

            var errorSource = new List<string> { "UpdateChatCompletionHandler" };
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
