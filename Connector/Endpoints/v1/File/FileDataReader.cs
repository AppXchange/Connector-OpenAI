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

namespace Connector.Endpoints.v1.File;

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

public class FileDataReader : TypedAsyncDataReaderBase<FileDataObject>
{
    private readonly ILogger<FileDataReader> _logger;
    private readonly ApiClient _apiClient;

    public FileDataReader(
        ILogger<FileDataReader> logger,
        ApiClient apiClient)
    {
        _logger = logger;
        _apiClient = apiClient;
    }

    public override async IAsyncEnumerable<FileDataObject> GetTypedDataAsync(DataObjectCacheWriteArguments? dataObjectRunArguments, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        FileDataObject? fileData = null;
        try
        {
            if (dataObjectRunArguments == null)
            {
                _logger.LogError("DataObjectRunArguments is null");
                yield break;
            }

            if (!dataObjectRunArguments.TryGetParameterValue("id", out string? fileId) || string.IsNullOrEmpty(fileId))
            {
                _logger.LogError("File ID is null or empty");
                yield break;
            }

            var response = await _apiClient.GetFile(fileId, cancellationToken)
                .ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                _logger.LogError("Failed to retrieve file. Status code: {StatusCode}", response.StatusCode);
                yield break;
            }

            if (response.Data == null)
            {
                _logger.LogWarning("No file data received from API");
                yield break;
            }

            fileData = response.Data;
        }
        catch (HttpRequestException exception)
        {
            _logger.LogError(exception, "Exception while retrieving file from OpenAI API");
            throw;
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Unexpected error while retrieving file");
            throw;
        }

        if (fileData != null)
        {
            yield return fileData;
        }
    }
}