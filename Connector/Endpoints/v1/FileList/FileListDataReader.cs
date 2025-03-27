using Connector.Client;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xchange.Connector.SDK.CacheWriter;
using ESR.Hosting.CacheWriter;
using System.Runtime.CompilerServices;
using System.Linq;

namespace Connector.Endpoints.v1.FileList;

public class FileListDataReader : TypedAsyncDataReaderBase<FileListDataObject>
{
    private readonly ILogger<FileListDataReader> _logger;
    private readonly ApiClient _apiClient;
    private string? _lastId;

    public FileListDataReader(
        ILogger<FileListDataReader> logger,
        ApiClient apiClient)
    {
        _logger = logger;
        _apiClient = apiClient;
    }

    public override async IAsyncEnumerable<FileListDataObject> GetTypedDataAsync(
        DataObjectCacheWriteArguments? dataObjectRunArguments,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        while (true)
        {
            var files = new List<FileListDataObject>();
            try
            {
                var response = await _apiClient.GetFileList(_lastId, cancellationToken)
                    .ConfigureAwait(false);

                if (!response.IsSuccessful || response.Data == null)
                {
                    _logger.LogError("Failed to get file list. Status code: {StatusCode}", response.StatusCode);
                    yield break;
                }

                if (response.Data.Data == null || response.Data.Data.Count() == 0)
                {
                    break;
                }

                foreach (var file in response.Data.Data)
                {
                    files.Add(new FileListDataObject
                    {
                        Id = file.Id,
                        Object = file.Object,
                        Bytes = file.Bytes,
                        CreatedAt = file.CreatedAt,
                        Filename = file.Filename,
                        Purpose = file.Purpose
                    });
                    _lastId = file.Id;
                }

                if (response.Data.Data.Count() < 100) // OpenAI's default limit
                {
                    break;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving file list");
                yield break;
            }

            foreach (var file in files)
            {
                yield return file;
            }
        }
    }
}