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

namespace Connector.Assistants.v1.Run;

public class RunDataReader : TypedAsyncDataReaderBase<RunDataObject>
{
    private readonly ILogger<RunDataReader> _logger;
    private readonly ApiClient _apiClient;

    public RunDataReader(
        ILogger<RunDataReader> logger,
        ApiClient apiClient)
    {
        _logger = logger;
        _apiClient = apiClient;
    }

    public override async IAsyncEnumerable<RunDataObject> GetTypedDataAsync(
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

        if (!dataObjectRunArguments.TryGetParameterValue("run_id", out string? runId) || string.IsNullOrEmpty(runId))
        {
            throw new ArgumentException("Run ID is required");
        }

        RunDataObject? run = null;
        try
        {
            var response = await _apiClient.GetRun(threadId, runId, cancellationToken)
                .ConfigureAwait(false);

            if (!response.IsSuccessful || response.Data == null)
            {
                throw new HttpRequestException($"Failed to retrieve run. Status code: {response.StatusCode}");
            }

            run = response.Data;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving run");
            throw;
        }

        if (run != null)
        {
            yield return run;
        }
    }
}