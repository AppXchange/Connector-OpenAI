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

namespace Connector.Assistants.v1.ListAssistants;

public class ListAssistantsDataReader : TypedAsyncDataReaderBase<ListAssistantsDataObject>
{
    private readonly ILogger<ListAssistantsDataReader> _logger;
    private readonly ApiClient _apiClient;
    private string? _lastId;

    public ListAssistantsDataReader(
        ILogger<ListAssistantsDataReader> logger,
        ApiClient apiClient)
    {
        _logger = logger;
        _apiClient = apiClient;
    }

    public override async IAsyncEnumerable<ListAssistantsDataObject> GetTypedDataAsync(
        DataObjectCacheWriteArguments? dataObjectRunArguments, 
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        var hasMore = true;
        while (hasMore)
        {
            ApiResponse<ListAssistantsDataObject> response;
            try
            {
                response = await _apiClient.ListAssistants(
                    after: _lastId,
                    limit: 20,
                    order: "desc",
                    cancellationToken: cancellationToken)
                    .ConfigureAwait(false);
            }
            catch (HttpRequestException exception)
            {
                _logger.LogError(exception, "Exception while making a read request to data object 'ListAssistantsDataObject'");
                throw;
            }

            if (!response.IsSuccessful || response.Data == null)
            {
                throw new HttpRequestException($"Failed to retrieve assistants. Status code: {response.StatusCode}");
            }

            yield return response.Data;

            hasMore = response.Data.HasMore;
            if (hasMore)
            {
                _lastId = response.Data.LastId;
            }
        }
    }
}