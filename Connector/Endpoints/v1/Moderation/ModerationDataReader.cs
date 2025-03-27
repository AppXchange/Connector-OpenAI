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
using System.Threading.Tasks;

namespace Connector.Endpoints.v1.Moderation;

public class ModerationDataReader : TypedAsyncDataReaderBase<ModerationDataObject>
{
    private readonly ILogger<ModerationDataReader> _logger;
    private readonly ApiClient _apiClient;

    public ModerationDataReader(
        ILogger<ModerationDataReader> logger,
        ApiClient apiClient)
    {
        _logger = logger;
        _apiClient = apiClient;
    }

    public override async IAsyncEnumerable<ModerationDataObject> GetTypedDataAsync(DataObjectCacheWriteArguments? dataObjectRunArguments, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        yield break;
    }
}