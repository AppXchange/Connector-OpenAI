using Connector.Client;
using System;
using ESR.Hosting.CacheWriter;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Xchange.Connector.SDK.CacheWriter;
using System.Net.Http;
using Connector.Endpoints.v1.BatchList;

namespace Connector.Endpoints.v1.BatchList;

public class BatchListDataReader : TypedAsyncDataReaderBase<BatchListDataObject>
{
    private readonly ILogger<BatchListDataReader> _logger;
    private readonly ApiClient _apiClient;
    private string? _lastId;

    public BatchListDataReader(
        ILogger<BatchListDataReader> logger,
        ApiClient apiClient)
    {
        _logger = logger;
        _apiClient = apiClient;
    }

    public override async IAsyncEnumerable<BatchListDataObject> GetTypedDataAsync(DataObjectCacheWriteArguments? dataObjectRunArguments, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        while (true)
        {
            var response = await GetBatchListResponse(_lastId, cancellationToken);
            if (response?.Data?.Data == null || !response.Data.Data.Any())
            {
                yield break;
            }

            foreach (var batch in response.Data.Data)
            {
                yield return batch;
            }

            _lastId = response.Data.LastId;
            if (!response.Data.HasMore)
            {
                break;
            }
        }
    }

    private async Task<ApiResponse<BatchListResponse>?> GetBatchListResponse(string? lastId, CancellationToken cancellationToken)
    {
        try
        {
            var response = await _apiClient.GetBatchList(lastId, cancellationToken).ConfigureAwait(false);
            if (!response.IsSuccessful)
            {
                _logger.LogError("Failed to retrieve batch list. API StatusCode: {StatusCode}", response.StatusCode);
                return null;
            }

            if (response.Data == null || !response.Data.Data.Any())
            {
                return null;
            }

            return response;
        }
        catch (HttpRequestException exception)
        {
            _logger.LogError(exception, "Exception while making a read request to data object 'BatchListDataObject'");
            throw;
        }
    }
}