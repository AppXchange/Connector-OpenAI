using Connector.Client;
using ESR.Hosting.CacheWriter;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Xchange.Connector.SDK.CacheWriter;
using System.Net.Http;

namespace Connector.Endpoints.v1.Model;

public class ModelDataReader : TypedAsyncDataReaderBase<ModelDataObject>
{
    private readonly ILogger<ModelDataReader> _logger;
    private readonly ApiClient _apiClient;
    private readonly string _modelId;

    public ModelDataReader(
        ILogger<ModelDataReader> logger,
        ApiClient apiClient,
        string modelId)
    {
        _logger = logger;
        _apiClient = apiClient;
        _modelId = modelId;
    }

    public override async IAsyncEnumerable<ModelDataObject> GetTypedDataAsync(
        DataObjectCacheWriteArguments? dataObjectRunArguments, 
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        ModelDataObject? model = null;
        try
        {
            var response = await _apiClient.GetModel(_modelId, cancellationToken)
                .ConfigureAwait(false);

            if (!response.IsSuccessful || response.Data == null)
            {
                _logger.LogError("Failed to retrieve model. Status code: {StatusCode}", response.StatusCode);
                yield break;
            }

            model = response.Data;
        }
        catch (HttpRequestException exception)
        {
            _logger.LogError(exception, "Exception while making a read request to data object 'ModelDataObject'");
            throw;
        }

        if (model != null)
        {
            yield return model;
        }
    }
}