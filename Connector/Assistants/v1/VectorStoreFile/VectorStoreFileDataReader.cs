using Connector.Client;
using System;
using ESR.Hosting.CacheWriter;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Xchange.Connector.SDK.CacheWriter;
using System.Net.Http;

namespace Connector.Assistants.v1.VectorStoreFile;

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

public class VectorStoreFileDataReader : TypedAsyncDataReaderBase<VectorStoreFileDataObject>
{
    private readonly ILogger<VectorStoreFileDataReader> _logger;
    private readonly ApiClient _apiClient;

    public VectorStoreFileDataReader(
        ILogger<VectorStoreFileDataReader> logger,
        ApiClient apiClient)
    {
        _logger = logger;
        _apiClient = apiClient;
    }

    public override async IAsyncEnumerable<VectorStoreFileDataObject> GetTypedDataAsync(
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

        if (!dataObjectRunArguments.TryGetParameterValue("file_id", out string? fileId) || string.IsNullOrEmpty(fileId))
        {
            throw new ArgumentException("file_id is required");
        }

        VectorStoreFileDataObject? response = null;
        ApiResponse<VectorStoreFileDataObject>? apiResponse = null;

        apiResponse = await _apiClient.GetVectorStoreFile(
            vectorStoreId: vectorStoreId,
            fileId: fileId,
            cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        if (!apiResponse.IsSuccessful || apiResponse.Data == null)
        {
            _logger.LogError("Failed to retrieve vector store file. Status code: {StatusCode}", apiResponse.StatusCode);
            throw new HttpRequestException($"Failed to retrieve vector store file. Status code: {apiResponse.StatusCode}");
        }

        response = apiResponse.Data;
        yield return response;
    }
}