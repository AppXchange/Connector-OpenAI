using Connector.Client;
using System;
using ESR.Hosting.CacheWriter;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Xchange.Connector.SDK.CacheWriter;
using System.Net.Http;

namespace Connector.Endpoints.v1.Embeddings;

public class EmbeddingsDataReader : TypedAsyncDataReaderBase<EmbeddingsDataObject>
{
    private readonly ILogger<EmbeddingsDataReader> _logger;
    private readonly ApiClient _apiClient;

    public EmbeddingsDataReader(
        ILogger<EmbeddingsDataReader> logger,
        ApiClient apiClient)
    {
        _logger = logger;
        _apiClient = apiClient;
    }

    public override async IAsyncEnumerable<EmbeddingsDataObject> GetTypedDataAsync(DataObjectCacheWriteArguments? dataObjectRunArguments, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        // Since we can't list embeddings, we'll just yield break
        yield break;
    }
}