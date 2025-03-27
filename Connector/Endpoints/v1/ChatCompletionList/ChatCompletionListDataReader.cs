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

namespace Connector.Endpoints.v1.ChatCompletionList;

public class ChatCompletionListDataReader : TypedAsyncDataReaderBase<ChatCompletionListDataObject>
{
    private readonly ILogger<ChatCompletionListDataReader> _logger;
    private readonly ApiClient _apiClient;
    private string? _lastId;

    public ChatCompletionListDataReader(
        ILogger<ChatCompletionListDataReader> logger,
        ApiClient apiClient)
    {
        _logger = logger;
        _apiClient = apiClient;
    }

    public override async IAsyncEnumerable<ChatCompletionListDataObject> GetTypedDataAsync(DataObjectCacheWriteArguments? dataObjectRunArguments, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        while (true)
        {
            ApiResponse<ChatCompletionListDataObject> response;
            try
            {
                response = await _apiClient.GetChatCompletionList(_lastId, cancellationToken);
            }
            catch (HttpRequestException exception)
            {
                _logger.LogError(exception, "Exception while making a read request to data object 'ChatCompletionListDataObject'");
                throw;
            }

            if (!response.IsSuccessful)
            {
                throw new Exception($"Failed to retrieve chat completion list. API StatusCode: {response.StatusCode}");
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