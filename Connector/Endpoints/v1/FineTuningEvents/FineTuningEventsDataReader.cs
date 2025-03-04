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

namespace Connector.Endpoints.v1.FineTuningEvents;

public class FineTuningEventsDataReader : TypedAsyncDataReaderBase<FineTuningEventsDataObject>
{
    private readonly ILogger<FineTuningEventsDataReader> _logger;
    private int _currentPage = 0;

    public FineTuningEventsDataReader(
        ILogger<FineTuningEventsDataReader> logger)
    {
        _logger = logger;
    }

    public override async IAsyncEnumerable<FineTuningEventsDataObject> GetTypedDataAsync(DataObjectCacheWriteArguments ? dataObjectRunArguments, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        while (true)
        {
            var response = new ApiResponse<PaginatedResponse<FineTuningEventsDataObject>>();
            // If the FineTuningEventsDataObject does not have the same structure as the FineTuningEvents response from the API, create a new class for it and replace FineTuningEventsDataObject with it.
            // Example:
            // var response = new ApiResponse<IEnumerable<FineTuningEventsResponse>>();

            // Make a call to your API/system to retrieve the objects/type for the connector's configuration.
            try
            {
                //response = await _apiClient.GetRecords<FineTuningEventsDataObject>(
                //    relativeUrl: "fineTuningEvents",
                //    page: _currentPage,
                //    cancellationToken: cancellationToken)
                //    .ConfigureAwait(false);
            }
            catch (HttpRequestException exception)
            {
                _logger.LogError(exception, "Exception while making a read request to data object 'FineTuningEventsDataObject'");
                throw;
            }

            if (!response.IsSuccessful)
            {
                throw new Exception($"Failed to retrieve records for 'FineTuningEventsDataObject'. API StatusCode: {response.StatusCode}");
            }

            if (response.Data == null || !response.Data.Items.Any()) break;

            // Return the data objects to Cache.
            foreach (var item in response.Data.Items)
            {
                // If new class was created to match the API response, create a new FineTuningEventsDataObject object, map the properties and return a FineTuningEventsDataObject.

                // Example:
                //var resource = new FineTuningEventsDataObject
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