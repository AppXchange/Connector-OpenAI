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

namespace Connector.Endpoints.v1.FineTuningEvents;

public class FineTuningEventsDataReader : TypedAsyncDataReaderBase<FineTuningEventsDataObject>
{
    private readonly ILogger<FineTuningEventsDataReader> _logger;
    private readonly ApiClient _apiClient;
    private string? _lastId;

    public FineTuningEventsDataReader(
        ILogger<FineTuningEventsDataReader> logger,
        ApiClient apiClient)
    {
        _logger = logger;
        _apiClient = apiClient;
    }

    public override async IAsyncEnumerable<FineTuningEventsDataObject> GetTypedDataAsync(
        DataObjectCacheWriteArguments? dataObjectRunArguments,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        if (dataObjectRunArguments == null)
        {
            _logger.LogError("DataObjectRunArguments is null");
            yield break;
        }

        string? fineTuningJobId = null;
        try
        {
            fineTuningJobId = dataObjectRunArguments.TryGetParameterValue<string>("fine_tuning_job_id", out var id) ? id : null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting fine-tuning job ID from arguments");
            yield break;
        }

        if (string.IsNullOrEmpty(fineTuningJobId))
        {
            _logger.LogError("Fine-tuning job ID is null or empty");
            yield break;
        }

        while (true)
        {
            var events = new List<FineTuningEventsDataObject>();
            try
            {
                var response = await _apiClient.GetFineTuningEvents(fineTuningJobId, _lastId, cancellationToken)
                    .ConfigureAwait(false);

                if (!response.IsSuccessful || response.Data == null)
                {
                    _logger.LogError("Failed to get fine-tuning events. Status code: {StatusCode}", response.StatusCode);
                    yield break;
                }

                if (response.Data.Data == null || !response.Data.Data.Any())
                {
                    break;
                }

                foreach (var evt in response.Data.Data)
                {
                    events.Add(evt);
                    _lastId = evt.Id;
                }

                if (!response.Data.HasMore)
                {
                    break;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving fine-tuning events");
                yield break;
            }

            foreach (var evt in events)
            {
                yield return evt;
            }
        }
    }
}