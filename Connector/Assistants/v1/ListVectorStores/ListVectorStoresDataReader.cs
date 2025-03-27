using Connector.Client;
using System;
using ESR.Hosting.CacheWriter;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Xchange.Connector.SDK.CacheWriter;
using System.Net.Http;

namespace Connector.Assistants.v1.ListVectorStores;

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

public class ListVectorStoresDataReader : TypedAsyncDataReaderBase<ListVectorStoresDataObject>
{
    private readonly ILogger<ListVectorStoresDataReader> _logger;
    private readonly ApiClient _apiClient;
    private string? _lastId;

    public ListVectorStoresDataReader(
        ILogger<ListVectorStoresDataReader> logger,
        ApiClient apiClient)
    {
        _logger = logger;
        _apiClient = apiClient;
    }

    public override async IAsyncEnumerable<ListVectorStoresDataObject> GetTypedDataAsync(
        DataObjectCacheWriteArguments? dataObjectRunArguments,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        if (dataObjectRunArguments == null)
        {
            throw new ArgumentException("DataObjectCacheWriteArguments is required");
        }

        int limit = 20;
        if (dataObjectRunArguments.TryGetParameterValue("limit", out int customLimit) && customLimit > 0 && customLimit <= 100)
        {
            limit = customLimit;
        }

        string order = "desc";
        if (dataObjectRunArguments.TryGetParameterValue("order", out string? customOrder) && 
            (customOrder == "asc" || customOrder == "desc"))
        {
            order = customOrder;
        }

        ListVectorStoresDataObject? response = null;

        while (true)
        {
            try
            {
                var apiResponse = await _apiClient.ListVectorStores(
                    after: _lastId,
                    limit: limit,
                    order: order,
                    cancellationToken: cancellationToken)
                    .ConfigureAwait(false);

                if (!apiResponse.IsSuccessful || apiResponse.Data == null)
                {
                    throw new HttpRequestException($"Failed to retrieve vector stores. Status code: {apiResponse.StatusCode}");
                }

                response = apiResponse.Data;
            }
            catch (HttpRequestException exception)
            {
                _logger.LogError(exception, "Exception while making a read request to data object 'ListVectorStoresDataObject'");
                throw;
            }

            if (response == null) yield break;

            yield return response;

            if (!response.HasMore) break;
            _lastId = response.LastId;
        }
    }
}