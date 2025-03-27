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

namespace Connector.Assistants.v1.Thread;

public class ThreadDataReader : TypedAsyncDataReaderBase<ThreadDataObject>
{
    private readonly ILogger<ThreadDataReader> _logger;
    private readonly ApiClient _apiClient;

    public ThreadDataReader(
        ILogger<ThreadDataReader> logger,
        ApiClient apiClient)
    {
        _logger = logger;
        _apiClient = apiClient;
    }

    public override async IAsyncEnumerable<ThreadDataObject> GetTypedDataAsync(
        DataObjectCacheWriteArguments? dataObjectRunArguments, 
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        ThreadDataObject? result = null;
        try
        {
            if (dataObjectRunArguments == null || !dataObjectRunArguments.TryGetParameterValue("id", out string? threadId))
            {
                throw new ArgumentException("Thread ID is required");
            }

            if (string.IsNullOrEmpty(threadId))
            {
                throw new ArgumentException("Thread ID cannot be null or empty");
            }

            var response = await _apiClient.GetThread(
                threadId: threadId,
                cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                throw new Exception($"Failed to retrieve thread. API StatusCode: {response.StatusCode}");
            }

            if (response.Data == null)
            {
                throw new Exception("No thread data received from API");
            }

            result = response.Data;
        }
        catch (HttpRequestException exception)
        {
            _logger.LogError(exception, "Exception while making a read request to data object 'ThreadDataObject'");
            throw;
        }

        if (result != null)
        {
            yield return result;
        }
    }
}