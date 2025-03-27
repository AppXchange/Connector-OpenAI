using Connector.Client;
using ESR.Hosting.CacheWriter;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Xchange.Connector.SDK.CacheWriter;
using System.Net.Http;

namespace Connector.Assistants.v1.ListVectorStoreFiles;

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

public class ListVectorStoreFilesDataReader : TypedAsyncDataReaderBase<ListVectorStoreFilesDataObject>
{
    private readonly ILogger<ListVectorStoreFilesDataReader> _logger;
    private readonly ApiClient _apiClient;
    private string? _after;
    private bool _hasMore = true;

    public ListVectorStoreFilesDataReader(
        ILogger<ListVectorStoreFilesDataReader> logger,
        ApiClient apiClient)
    {
        _logger = logger;
        _apiClient = apiClient;
    }

    public override async IAsyncEnumerable<ListVectorStoreFilesDataObject> GetTypedDataAsync(
        DataObjectCacheWriteArguments? dataObjectRunArguments,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        if (dataObjectRunArguments == null)
        {
            throw new ArgumentNullException(nameof(dataObjectRunArguments));
        }

        var vectorStoreId = dataObjectRunArguments.TryGetParameterValue<string>("vector_store_id");
        if (string.IsNullOrEmpty(vectorStoreId))
        {
            throw new ArgumentException("vector_store_id parameter is required");
        }

        while (_hasMore)
        {
            ListVectorStoreFilesDataObject? responseData = null;
            try
            {
                var response = await _apiClient.ListVectorStoreFiles(
                    vectorStoreId: vectorStoreId,
                    after: _after,
                    before: null,
                    limit: 100,
                    order: "desc",
                    cancellationToken: cancellationToken)
                    .ConfigureAwait(false);

                if (!response.IsSuccessful || response.Data == null)
                {
                    throw new HttpRequestException($"Failed to retrieve vector store files. Status code: {response.StatusCode}");
                }

                responseData = response.Data;
                _hasMore = responseData.HasMore;
                _after = responseData.LastId;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving vector store files");
                throw;
            }

            if (responseData != null)
            {
                yield return responseData;
            }
        }
    }
}