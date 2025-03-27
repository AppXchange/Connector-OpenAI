using Connector.Client;
using ESR.Hosting.CacheWriter;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using Xchange.Connector.SDK.CacheWriter;

namespace Connector.Endpoints.v1.ModelList;

public class ModelListDataReader : TypedAsyncDataReaderBase<ModelListDataObject>
{
    private readonly ILogger<ModelListDataReader> _logger;
    private readonly ApiClient _apiClient;

    public ModelListDataReader(
        ILogger<ModelListDataReader> logger,
        ApiClient apiClient)
    {
        _logger = logger;
        _apiClient = apiClient;
    }

    public override async IAsyncEnumerable<ModelListDataObject> GetTypedDataAsync(
        DataObjectCacheWriteArguments? dataObjectRunArguments, 
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        ModelListDataObject? modelList = null;
        try
        {
            var response = await _apiClient.GetModelList(cancellationToken)
                .ConfigureAwait(false);

            if (!response.IsSuccessful || response.Data == null)
            {
                _logger.LogError("Failed to retrieve model list. Status code: {StatusCode}", response.StatusCode);
                yield break;
            }

            modelList = response.Data;
        }
        catch (HttpRequestException exception)
        {
            _logger.LogError(exception, "Exception while making a read request to data object 'ModelListDataObject'");
            throw;
        }

        if (modelList != null)
        {
            yield return modelList;
        }
    }
}