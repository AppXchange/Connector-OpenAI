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

namespace Connector.Endpoints.v1.FineTuningJob;

public class FineTuningJobDataReader : TypedAsyncDataReaderBase<FineTuningJobDataObject>
{
    private readonly ILogger<FineTuningJobDataReader> _logger;
    private readonly ApiClient _apiClient;

    public FineTuningJobDataReader(
        ILogger<FineTuningJobDataReader> logger,
        ApiClient apiClient)
    {
        _logger = logger;
        _apiClient = apiClient;
    }

    public override async IAsyncEnumerable<FineTuningJobDataObject> GetTypedDataAsync(
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

        FineTuningJobDataObject? job = null;
        try
        {
            var response = await _apiClient.GetFineTuningJob(fineTuningJobId, cancellationToken)
                .ConfigureAwait(false);

            if (!response.IsSuccessful || response.Data == null)
            {
                _logger.LogError("Failed to get fine-tuning job. Status code: {StatusCode}", response.StatusCode);
                yield break;
            }

            job = response.Data;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving fine-tuning job");
            yield break;
        }

        if (job != null)
        {
            yield return job;
        }
    }
}