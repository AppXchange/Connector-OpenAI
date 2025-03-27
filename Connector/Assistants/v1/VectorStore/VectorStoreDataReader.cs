using Connector.Client;
using System;
using ESR.Hosting.CacheWriter;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Xchange.Connector.SDK.CacheWriter;
using System.Net.Http;

namespace Connector.Assistants.v1.VectorStore;

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

public class VectorStoreDataReader : TypedAsyncDataReaderBase<VectorStoreDataObject>
{
    private readonly ILogger<VectorStoreDataReader> _logger;
    private readonly ApiClient _apiClient;

    public VectorStoreDataReader(
        ILogger<VectorStoreDataReader> logger,
        ApiClient apiClient)
    {
        _logger = logger;
        _apiClient = apiClient;
    }

    public override async IAsyncEnumerable<VectorStoreDataObject> GetTypedDataAsync(
        DataObjectCacheWriteArguments? dataObjectRunArguments,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        if (dataObjectRunArguments == null)
        {
            throw new ArgumentException("DataObjectCacheWriteArguments is required");
        }

        if (!dataObjectRunArguments.TryGetParameterValue("vector_store_id", out string? vectorStoreId) || string.IsNullOrEmpty(vectorStoreId))
        {
            throw new ArgumentException("Vector Store ID is required");
        }

        VectorStoreDataObject? response = null;

        try
        {
            var apiResponse = await _apiClient.GetVectorStore(
                vectorStoreId: vectorStoreId,
                cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            if (!apiResponse.IsSuccessful || apiResponse.Data == null)
            {
                throw new HttpRequestException($"Failed to retrieve vector store. Status code: {apiResponse.StatusCode}");
            }

            response = apiResponse.Data;
        }
        catch (HttpRequestException exception)
        {
            _logger.LogError(exception, "Exception while making a read request to data object 'VectorStoreDataObject'");
            throw;
        }

        if (response != null)
        {
            yield return response;
        }
    }
}