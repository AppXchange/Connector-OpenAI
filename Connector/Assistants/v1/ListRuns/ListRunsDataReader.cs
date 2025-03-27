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

namespace Connector.Assistants.v1.ListRuns;

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

public class ListRunsDataReader : TypedAsyncDataReaderBase<ListRunsDataObject>
{
    private readonly ILogger<ListRunsDataReader> _logger;
    private readonly ApiClient _apiClient;
    private string? _lastId;

    public ListRunsDataReader(
        ILogger<ListRunsDataReader> logger,
        ApiClient apiClient)
    {
        _logger = logger;
        _apiClient = apiClient;
    }

    public override async IAsyncEnumerable<ListRunsDataObject> GetTypedDataAsync(
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

        ApiResponse<ListRunsDataObject>? response = null;
        try
        {
            while (true)
            {
                response = await _apiClient.ListRuns(
                    threadId: threadId,
                    after: _lastId,
                    before: null,
                    limit: 20,
                    order: "desc",
                    cancellationToken: cancellationToken)
                    .ConfigureAwait(false);

                if (!response.IsSuccessful || response.Data == null)
                {
                    throw new HttpRequestException($"Failed to retrieve runs. Status code: {response.StatusCode}");
                }

                if (!response.Data.HasMore)
                {
                    break;
                }

                _lastId = response.Data.LastId;
            }
        }
        catch (HttpRequestException exception)
        {
            _logger.LogError(exception, "Exception while making a read request to data object 'ListRunsDataObject'");
            throw;
        }

        if (response?.Data != null)
        {
            yield return response.Data;
        }
    }
}