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

namespace Connector.Endpoints.v1.Upload;

public class UploadDataReader : TypedAsyncDataReaderBase<UploadDataObject>
{
    private readonly ILogger<UploadDataReader> _logger;
    private readonly ApiClient _apiClient;
    private int _currentPage = 0;

    public UploadDataReader(
        ILogger<UploadDataReader> logger,
        ApiClient apiClient)
    {
        _logger = logger;
        _apiClient = apiClient;
    }

    public override async IAsyncEnumerable<UploadDataObject> GetTypedDataAsync(
        DataObjectCacheWriteArguments? dataObjectRunArguments, 
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        UploadDataObject? upload = null;
        try
        {
            if (dataObjectRunArguments == null || !dataObjectRunArguments.TryGetParameterValue("id", out string? uploadId) || string.IsNullOrEmpty(uploadId))
            {
                _logger.LogError("No upload ID provided");
                yield break;
            }

            var response = await _apiClient.GetUpload(uploadId, cancellationToken)
                .ConfigureAwait(false);

            if (!response.IsSuccessful || response.Data == null)
            {
                _logger.LogError("Failed to retrieve upload. Status code: {StatusCode}", response.StatusCode);
                yield break;
            }

            upload = response.Data;
        }
        catch (HttpRequestException exception)
        {
            _logger.LogError(exception, "Exception while making a read request to data object 'UploadDataObject'");
            throw;
        }

        if (upload != null)
        {
            yield return upload;
        }
    }
}