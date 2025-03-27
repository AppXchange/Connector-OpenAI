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

namespace Connector.Endpoints.v1.Batch;

internal static class DataObjectExtensions
{
    public static bool TryGetParameterValue<T>(this DataObjectCacheWriteArguments args, string key, out T? value)
    {
        value = default;
        if (args == null) return false;

        var dict = args.GetType().GetProperty("Arguments")?.GetValue(args) as IDictionary<string, object>;
        if (dict == null || !dict.ContainsKey(key)) return false;

        try
        {
            value = (T)dict[key];
            return true;
        }
        catch
        {
            return false;
        }
    }
}

public class BatchDataReader : TypedAsyncDataReaderBase<BatchDataObject>
{
    private readonly ILogger<BatchDataReader> _logger;
    private readonly ApiClient _apiClient;

    public BatchDataReader(
        ILogger<BatchDataReader> logger,
        ApiClient apiClient)
    {
        _logger = logger;
        _apiClient = apiClient;
    }

    public override async IAsyncEnumerable<BatchDataObject> GetTypedDataAsync(DataObjectCacheWriteArguments? dataObjectRunArguments, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        if (dataObjectRunArguments == null)
        {
            _logger.LogError("DataObjectRunArguments is null");
            yield break;
        }

        if (!dataObjectRunArguments.TryGetParameterValue("id", out string? batchId) || string.IsNullOrEmpty(batchId))
        {
            _logger.LogError("Batch ID is null or empty");
            yield break;
        }

        var response = await _apiClient.GetBatch(batchId, cancellationToken).ConfigureAwait(false);
        if (!response.IsSuccessful)
        {
            _logger.LogError("Failed to retrieve batch data. API StatusCode: {StatusCode}", response.StatusCode);
            yield break;
        }

        if (response.Data != null)
        {
            yield return response.Data;
        }
    }
}