using Connector.Client;
using Connector.Extensions;
using System;
using ESR.Hosting.CacheWriter;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Xchange.Connector.SDK.CacheWriter;
using System.Net.Http;

namespace Connector.Assistants.v1.Assistant;

public class AssistantDataReader : TypedAsyncDataReaderBase<AssistantDataObject>
{
    private readonly ILogger<AssistantDataReader> _logger;
    private readonly ApiClient _apiClient;
    private int _currentPage = 0;

    public AssistantDataReader(
        ILogger<AssistantDataReader> logger,
        ApiClient apiClient)
    {
        _logger = logger;
        _apiClient = apiClient;
    }

    public override async IAsyncEnumerable<AssistantDataObject> GetTypedDataAsync(DataObjectCacheWriteArguments? dataObjectRunArguments, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        AssistantDataObject? result = null;
        try
        {
            if (dataObjectRunArguments == null || !dataObjectRunArguments.TryGetParameterValue("id", out string? assistantId))
            {
                throw new ArgumentException("Assistant ID is required");
            }

            if (string.IsNullOrEmpty(assistantId))
            {
                throw new ArgumentException("Assistant ID cannot be null or empty");
            }

            var response = await _apiClient.GetAssistant(
                assistantId: assistantId,
                cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                throw new Exception($"Failed to retrieve assistant. API StatusCode: {response.StatusCode}");
            }

            if (response.Data == null)
            {
                throw new Exception("No assistant data received from API");
            }

            result = response.Data;
        }
        catch (HttpRequestException exception)
        {
            _logger.LogError(exception, "Exception while making a read request to data object 'AssistantDataObject'");
            throw;
        }

        if (result != null)
        {
            yield return result;
        }
    }
}