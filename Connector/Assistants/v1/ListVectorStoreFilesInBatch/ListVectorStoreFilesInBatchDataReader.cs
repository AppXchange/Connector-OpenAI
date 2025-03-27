using Connector.Client;
using ESR.Hosting.CacheWriter;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Xchange.Connector.SDK.CacheWriter;

namespace Connector.Assistants.v1.ListVectorStoreFilesInBatch;

public static class DataObjectExtensions
{
    public static T? TryGetParameterValue<T>(this DataObjectCacheWriteArguments args, string key)
    {
        if (args == null) return default;

        var dict = args.GetType().GetProperty("Arguments")?.GetValue(args) as IDictionary<string, object>;
        if (dict == null || !dict.ContainsKey(key)) return default;

        try
        {
            return (T)dict[key];
        }
        catch
        {
            return default;
        }
    }
}

public class ListVectorStoreFilesInBatchDataReader : TypedAsyncDataReaderBase<ListVectorStoreFilesInBatchDataObject>
{
    private readonly ILogger<ListVectorStoreFilesInBatchDataReader> _logger;
    private readonly ApiClient _apiClient;
    private string? _after = null;
    private bool _hasMore = true;

    public ListVectorStoreFilesInBatchDataReader(
        ILogger<ListVectorStoreFilesInBatchDataReader> logger,
        ApiClient apiClient)
    {
        _logger = logger;
        _apiClient = apiClient;
    }

    public override async IAsyncEnumerable<ListVectorStoreFilesInBatchDataObject> GetTypedDataAsync(
        DataObjectCacheWriteArguments? dataObjectRunArguments,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        if (dataObjectRunArguments == null)
        {
            throw new System.ArgumentNullException(nameof(dataObjectRunArguments), "Data object run arguments cannot be null");
        }

        var vectorStoreId = dataObjectRunArguments.TryGetParameterValue<string>("vector_store_id");
        var batchId = dataObjectRunArguments.TryGetParameterValue<string>("batch_id");

        if (string.IsNullOrEmpty(vectorStoreId))
        {
            throw new System.ArgumentException("vector_store_id parameter is required", nameof(dataObjectRunArguments));
        }

        if (string.IsNullOrEmpty(batchId))
        {
            throw new System.ArgumentException("batch_id parameter is required", nameof(dataObjectRunArguments));
        }

        while (_hasMore)
        {
            var response = await _apiClient.ListVectorStoreFilesInBatch(
                vectorStoreId: vectorStoreId,
                batchId: batchId,
                after: _after,
                cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            if (!response.IsSuccessful || response.Data == null)
            {
                _logger.LogError("Failed to retrieve vector store files in batch. Status code: {StatusCode}", response.StatusCode);
                yield break;
            }

            yield return response.Data;

            _after = response.Data.LastId;
            _hasMore = response.Data.HasMore;
        }
    }
}