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

namespace Connector.Endpoints.v1.FineTuningJobs;

public class FineTuningJobsDataReader : TypedAsyncDataReaderBase<FineTuningJobsDataObject>
{
    private readonly ILogger<FineTuningJobsDataReader> _logger;
    private readonly ApiClient _apiClient;
    private string? _lastId;

    public FineTuningJobsDataReader(
        ILogger<FineTuningJobsDataReader> logger,
        ApiClient apiClient)
    {
        _logger = logger;
        _apiClient = apiClient;
    }

    public override async IAsyncEnumerable<FineTuningJobsDataObject> GetTypedDataAsync(
        DataObjectCacheWriteArguments? dataObjectRunArguments,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        if (dataObjectRunArguments == null)
        {
            _logger.LogError("DataObjectRunArguments is null");
            yield break;
        }

        while (true)
        {
            var jobs = new List<FineTuningJobsDataObject>();
            try
            {
                var response = await _apiClient.GetFineTuningJobs(_lastId, cancellationToken)
                    .ConfigureAwait(false);

                if (!response.IsSuccessful || response.Data == null)
                {
                    _logger.LogError("Failed to get fine-tuning jobs. Status code: {StatusCode}", response.StatusCode);
                    yield break;
                }

                if (response.Data.Data == null || !response.Data.Data.Any())
                {
                    break;
                }

                foreach (var job in response.Data.Data)
                {
                    jobs.Add(job);
                    _lastId = job.Id;
                }

                if (!response.Data.HasMore)
                {
                    break;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving fine-tuning jobs");
                yield break;
            }

            foreach (var job in jobs)
            {
                yield return job;
            }
        }
    }
}