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

namespace Connector.Endpoints.v1.Speech;

public class SpeechDataReader : TypedAsyncDataReaderBase<SpeechDataObject>
{
    private readonly ILogger<SpeechDataReader> _logger;
    private readonly ApiClient _apiClient;
    private int _currentPage = 0;

    public SpeechDataReader(
        ILogger<SpeechDataReader> logger,
        ApiClient apiClient)
    {
        _logger = logger;
        _apiClient = apiClient;
    }

    public override async IAsyncEnumerable<SpeechDataObject> GetTypedDataAsync(DataObjectCacheWriteArguments? dataObjectRunArguments, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        while (true)
        {
            ApiResponse<PaginatedResponse<SpeechDataObject>> response;
            try
            {
                response = await _apiClient.GetSpeeches(_currentPage, cancellationToken);
            }
            catch (HttpRequestException exception)
            {
                _logger.LogError(exception, "Exception while making a read request to data object 'SpeechDataObject'");
                throw;
            }

            if (!response.IsSuccessful)
            {
                throw new Exception($"Failed to retrieve speeches. API StatusCode: {response.StatusCode}");
            }

            if (response.Data == null || !response.Data.Items.Any())
            {
                break;
            }

            foreach (var item in response.Data.Items)
            {
                yield return item;
            }

            _currentPage++;
            if (_currentPage >= response.Data.TotalPages)
            {
                break;
            }
        }
    }
}