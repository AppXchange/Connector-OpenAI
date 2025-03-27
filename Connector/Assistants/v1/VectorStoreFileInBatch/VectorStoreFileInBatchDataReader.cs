using Connector.Client;
using System;
using ESR.Hosting.CacheWriter;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Xchange.Connector.SDK.CacheWriter;
using System.Net.Http;

namespace Connector.Assistants.v1.VectorStoreFileInBatch;

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

public class VectorStoreFileInBatchDataReader : TypedAsyncDataReaderBase<VectorStoreFileInBatchDataObject>
{
    private readonly ILogger<VectorStoreFileInBatchDataReader> _logger;
    private readonly ApiClient _apiClient;

    public VectorStoreFileInBatchDataReader(
        ILogger<VectorStoreFileInBatchDataReader> logger,
        ApiClient apiClient)
    {
        _logger = logger;
        _apiClient = apiClient;
    }

    public override async IAsyncEnumerable<VectorStoreFileInBatchDataObject> GetTypedDataAsync(
        DataObjectCacheWriteArguments? dataObjectRunArguments,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        if (dataObjectRunArguments == null)
        {
            throw new ArgumentException("DataObjectCacheWriteArguments is required");
        }

        if (!dataObjectRunArguments.TryGetParameterValue("vector_store_id", out string? vectorStoreId) || string.IsNullOrEmpty(vectorStoreId))
        {
            throw new ArgumentException("vector_store_id is required");
        }

        if (!dataObjectRunArguments.TryGetParameterValue("batch_id", out string? batchId) || string.IsNullOrEmpty(batchId))
        {
            throw new ArgumentException("batch_id is required");
        }

        VectorStoreFileInBatchDataObject? response = null;
        try
        {
            var apiResponse = await _apiClient.GetVectorStoreFileBatch(
                vectorStoreId: vectorStoreId,
                batchId: batchId,
                cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            if (!apiResponse.IsSuccessful || apiResponse.Data == null)
            {
                throw new HttpRequestException($"Failed to retrieve vector store file batch. Status code: {apiResponse.StatusCode}");
            }

            response = apiResponse.Data;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving vector store file batch");
            throw;
        }

        if (response != null)
        {
            yield return response;
        }
    }
}