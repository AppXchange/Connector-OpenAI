using Connector.Client;
using System;
using ESR.Hosting.CacheWriter;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Xchange.Connector.SDK.CacheWriter;
using System.Net.Http;

namespace Connector.Endpoints.v1.ChatMessages;

public class ChatMessagesDataReader : TypedAsyncDataReaderBase<ChatMessagesDataObject>
{
    private readonly ILogger<ChatMessagesDataReader> _logger;
    private readonly ApiClient _apiClient;
    private string? _lastId;

    public ChatMessagesDataReader(
        ILogger<ChatMessagesDataReader> logger,
        ApiClient apiClient)
    {
        _logger = logger;
        _apiClient = apiClient;
    }

    public override async IAsyncEnumerable<ChatMessagesDataObject> GetTypedDataAsync(DataObjectCacheWriteArguments? dataObjectRunArguments, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        while (true)
        {
            ApiResponse<ChatMessagesDataObject> response;
            try
            {
                response = await _apiClient.GetChatMessages(_lastId, cancellationToken);
            }
            catch (HttpRequestException exception)
            {
                _logger.LogError(exception, "Exception while making a read request to data object 'ChatMessagesDataObject'");
                throw;
            }

            if (!response.IsSuccessful)
            {
                throw new Exception($"Failed to retrieve chat messages. API StatusCode: {response.StatusCode}");
            }

            if (response.Data == null)
            {
                break;
            }

            yield return response.Data;

            if (!response.Data.HasMore)
            {
                break;
            }

            _lastId = response.Data.LastId;
        }
    }
}