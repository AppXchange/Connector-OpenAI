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

namespace Connector.Endpoints.v1.Image;

public class ImageDataReader : TypedAsyncDataReaderBase<ImageDataObject>
{
    private readonly ILogger<ImageDataReader> _logger;
    private int _currentPage = 0;

    public ImageDataReader(
        ILogger<ImageDataReader> logger)
    {
        _logger = logger;
    }

    public override async IAsyncEnumerable<ImageDataObject> GetTypedDataAsync(DataObjectCacheWriteArguments ? dataObjectRunArguments, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        while (true)
        {
            var response = new ApiResponse<PaginatedResponse<ImageDataObject>>();
            // If the ImageDataObject does not have the same structure as the Image response from the API, create a new class for it and replace ImageDataObject with it.
            // Example:
            // var response = new ApiResponse<IEnumerable<ImageResponse>>();

            // Make a call to your API/system to retrieve the objects/type for the connector's configuration.
            try
            {
                //response = await _apiClient.GetRecords<ImageDataObject>(
                //    relativeUrl: "images",
                //    page: _currentPage,
                //    cancellationToken: cancellationToken)
                //    .ConfigureAwait(false);
            }
            catch (HttpRequestException exception)
            {
                _logger.LogError(exception, "Exception while making a read request to data object 'ImageDataObject'");
                throw;
            }

            if (!response.IsSuccessful)
            {
                throw new Exception($"Failed to retrieve records for 'ImageDataObject'. API StatusCode: {response.StatusCode}");
            }

            if (response.Data == null || !response.Data.Items.Any()) break;

            // Return the data objects to Cache.
            foreach (var item in response.Data.Items)
            {
                // If new class was created to match the API response, create a new ImageDataObject object, map the properties and return a ImageDataObject.

                // Example:
                //var resource = new ImageDataObject
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