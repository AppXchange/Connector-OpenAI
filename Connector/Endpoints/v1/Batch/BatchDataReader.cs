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

namespace Connector.Endpoints.v1.Batch;

public class BatchDataReader : TypedAsyncDataReaderBase<BatchDataObject>
{
    private readonly ILogger<BatchDataReader> _logger;
    private int _currentPage = 0;

    public BatchDataReader(
        ILogger<BatchDataReader> logger)
    {
        _logger = logger;
    }

    public override async IAsyncEnumerable<BatchDataObject> GetTypedDataAsync(DataObjectCacheWriteArguments ? dataObjectRunArguments, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        while (true)
        {
            var response = new ApiResponse<PaginatedResponse<BatchDataObject>>();
            // If the BatchDataObject does not have the same structure as the Batch response from the API, create a new class for it and replace BatchDataObject with it.
            // Example:
            // var response = new ApiResponse<IEnumerable<BatchResponse>>();

            // Make a call to your API/system to retrieve the objects/type for the connector's configuration.
            try
            {
                //response = await _apiClient.GetRecords<BatchDataObject>(
                //    relativeUrl: "batchs",
                //    page: _currentPage,
                //    cancellationToken: cancellationToken)
                //    .ConfigureAwait(false);
            }
            catch (HttpRequestException exception)
            {
                _logger.LogError(exception, "Exception while making a read request to data object 'BatchDataObject'");
                throw;
            }

            if (!response.IsSuccessful)
            {
                throw new Exception($"Failed to retrieve records for 'BatchDataObject'. API StatusCode: {response.StatusCode}");
            }

            if (response.Data == null || !response.Data.Items.Any()) break;

            // Return the data objects to Cache.
            foreach (var item in response.Data.Items)
            {
                // If new class was created to match the API response, create a new BatchDataObject object, map the properties and return a BatchDataObject.

                // Example:
                //var resource = new BatchDataObject
                //{
                //// TODO: Map properties.      
                //};
                //yield return resource;
                yield return item;
            }

            // Handle pagination per API client design
            _currentPage++;
            if (_currentPage >= response.Data.TotalPages)
            {
                break;
            }
        }
    }
}