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

namespace Connector.Endpoints.v1.ChatCompletion;

public class ChatCompletionDataReader : TypedAsyncDataReaderBase<ChatCompletionDataObject>
{
    private readonly ILogger<ChatCompletionDataReader> _logger;
    private readonly ApiClient _apiClient;
    private int _currentPage = 0;

    public ChatCompletionDataReader(
        ILogger<ChatCompletionDataReader> logger,
        ApiClient apiClient)
    {
        _logger = logger;
        _apiClient = apiClient;
    }

    public override async IAsyncEnumerable<ChatCompletionDataObject> GetTypedDataAsync(DataObjectCacheWriteArguments? dataObjectRunArguments, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        ApiResponse<PaginatedResponse<ChatCompletionDataObject>> response;
        try
        {
            response = await _apiClient.GetChatCompletions(_currentPage, cancellationToken);
        }
        catch (HttpRequestException exception)
        {
            _logger.LogError(exception, "Exception while making a read request to data object 'ChatCompletionDataObject'");
            throw;
        }

        if (!response.IsSuccessful)
        {
            throw new Exception($"Failed to retrieve chat completions. API StatusCode: {response.StatusCode}");
        }

        if (response.Data == null || !response.Data.Items.Any())
        {
            yield break;
        }

        foreach (var item in response.Data.Items)
        {
            yield return item;
        }

        _currentPage++;
        if (_currentPage >= response.Data.TotalPages)
        {
            yield break;
        }
    }
}