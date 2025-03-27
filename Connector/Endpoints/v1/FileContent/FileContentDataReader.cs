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

namespace Connector.Endpoints.v1.FileContent;

public class FileContentDataReader : TypedAsyncDataReaderBase<FileContentDataObject>
{
    private readonly ILogger<FileContentDataReader> _logger;
    private readonly ApiClient _apiClient;
    private string? _lastId;

    public FileContentDataReader(
        ILogger<FileContentDataReader> logger,
        ApiClient apiClient)
    {
        _logger = logger;
        _apiClient = apiClient;
    }

    public override async IAsyncEnumerable<FileContentDataObject> GetTypedDataAsync(
        DataObjectCacheWriteArguments? dataObjectRunArguments,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        if (dataObjectRunArguments == null)
        {
            _logger.LogError("DataObjectRunArguments is null");
            yield break;
        }

        string? fileId = null;
        try
        {
            fileId = dataObjectRunArguments.TryGetParameterValue<string>("file_id", out var id) ? id : null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting file ID from arguments");
            yield break;
        }

        if (string.IsNullOrEmpty(fileId))
        {
            _logger.LogError("File ID is null or empty");
            yield break;
        }

        FileContentDataObject? result = null;
        try
        {
            var response = await _apiClient.GetFileContent(fileId, cancellationToken)
                .ConfigureAwait(false);

            if (!response.IsSuccessful || response.Data == null)
            {
                _logger.LogError("Failed to get file content. Status code: {StatusCode}", response.StatusCode);
                yield break;
            }

            result = new FileContentDataObject
            {
                Id = fileId,
                Content = response.Data,
                FileId = fileId,
                CreatedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving file content");
            yield break;
        }

        if (result != null)
        {
            yield return result;
        }
    }
}