using Connector.Client;
using Connector.Extensions;
using System;
using ESR.Hosting.CacheWriter;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Xchange.Connector.SDK.CacheWriter;
using System.Net.Http;

namespace Connector.Assistants.v1.ListMessages;

public class ListMessagesDataReader : TypedAsyncDataReaderBase<ListMessagesDataObject>
{
    private readonly ILogger<ListMessagesDataReader> _logger;
    private readonly ApiClient _apiClient;
    private string? _lastId;

    public ListMessagesDataReader(
        ILogger<ListMessagesDataReader> logger,
        ApiClient apiClient)
    {
        _logger = logger;
        _apiClient = apiClient;
    }

    public override async IAsyncEnumerable<ListMessagesDataObject> GetTypedDataAsync(
        DataObjectCacheWriteArguments? dataObjectRunArguments, 
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        if (dataObjectRunArguments == null)
        {
            throw new ArgumentException("DataObjectCacheWriteArguments is required");
        }

        if (!dataObjectRunArguments.TryGetParameterValue("thread_id", out string? threadId) || string.IsNullOrEmpty(threadId))
        {
            throw new ArgumentException("Thread ID is required");
        }

        while (true)
        {
            ApiResponse<ListMessagesDataObject>? response = null;
            try
            {
                response = await _apiClient.ListMessages(
                    threadId: threadId,
                    after: _lastId,
                    cancellationToken: cancellationToken)
                    .ConfigureAwait(false);

                if (!response.IsSuccessful || response.Data == null)
                {
                    throw new HttpRequestException($"Failed to retrieve messages. Status code: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving messages");
                throw;
            }

            if (!response.Data.HasMore)
            {
                yield return response.Data;
                break;
            }

            yield return response.Data;
            _lastId = response.Data.LastId;
        }
    }
}