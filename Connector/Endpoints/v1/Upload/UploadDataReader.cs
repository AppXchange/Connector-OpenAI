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

namespace Connector.Endpoints.v1.Upload;

public class UploadDataReader : TypedAsyncDataReaderBase<UploadDataObject>
{
    private readonly ILogger<UploadDataReader> _logger;
    private int _currentPage = 0;

    public UploadDataReader(
        ILogger<UploadDataReader> logger)
    {
        _logger = logger;
    }

    public override async IAsyncEnumerable<UploadDataObject> GetTypedDataAsync(DataObjectCacheWriteArguments ? dataObjectRunArguments, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        while (true)
        {
            var response = new ApiResponse<PaginatedResponse<UploadDataObject>>();
            // If the UploadDataObject does not have the same structure as the Upload response from the API, create a new class for it and replace UploadDataObject with it.
            // Example:
            // var response = new ApiResponse<IEnumerable<UploadResponse>>();

            // Make a call to your API/system to retrieve the objects/type for the connector's configuration.
            try
            {
                //response = await _apiClient.GetRecords<UploadDataObject>(
                //    relativeUrl: "uploads",
                //    page: _currentPage,
                //    cancellationToken: cancellationToken)
                //    .ConfigureAwait(false);
            }
            catch (HttpRequestException exception)
            {
                _logger.LogError(exception, "Exception while making a read request to data object 'UploadDataObject'");
                throw;
            }

            if (!response.IsSuccessful)
            {
                throw new Exception($"Failed to retrieve records for 'UploadDataObject'. API StatusCode: {response.StatusCode}");
            }

            if (response.Data == null || !response.Data.Items.Any()) break;

            // Return the data objects to Cache.
            foreach (var item in response.Data.Items)
            {
                // If new class was created to match the API response, create a new UploadDataObject object, map the properties and return a UploadDataObject.

                // Example:
                //var resource = new UploadDataObject
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