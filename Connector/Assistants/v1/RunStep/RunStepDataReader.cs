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

namespace Connector.Assistants.v1.RunStep;

internal static class DataObjectExtensions
{
    public static bool TryGetParameterValue<T>(this DataObjectCacheWriteArguments args, string key, out T? value)
    {
        value = default;
        if (args == null) return false;

        var dict = args.GetType().GetProperty("Arguments")?.GetValue(args) as IDictionary<string, object>;
        if (dict == null || !dict.ContainsKey(key)) return false;

        try
        {
            value = (T)dict[key];
            return true;
        }
        catch
        {
            return false;
        }
    }
}

public class RunStepDataReader : TypedAsyncDataReaderBase<RunStepDataObject>
{
    private readonly ILogger<RunStepDataReader> _logger;
    private readonly ApiClient _apiClient;

    public RunStepDataReader(
        ILogger<RunStepDataReader> logger,
        ApiClient apiClient)
    {
        _logger = logger;
        _apiClient = apiClient;
    }

    public override async IAsyncEnumerable<RunStepDataObject> GetTypedDataAsync(
        DataObjectCacheWriteArguments? dataObjectRunArguments,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        if (dataObjectRunArguments == null)
        {
            throw new ArgumentException("DataObjectCacheWriteArguments is required");
        }

        if (!dataObjectRunArguments.TryGetParameterValue("thread_id", out string? threadId) || string.IsNullOrEmpty(threadId))
        {
            throw new ArgumentException("Thread ID is required");
        }

        if (!dataObjectRunArguments.TryGetParameterValue("run_id", out string? runId) || string.IsNullOrEmpty(runId))
        {
            throw new ArgumentException("Run ID is required");
        }

        if (!dataObjectRunArguments.TryGetParameterValue("step_id", out string? stepId) || string.IsNullOrEmpty(stepId))
        {
            throw new ArgumentException("Step ID is required");
        }

        RunStepDataObject? runStep = null;
        try
        {
            var response = await _apiClient.GetRunStep(threadId, runId, stepId, cancellationToken)
                .ConfigureAwait(false);

            if (!response.IsSuccessful || response.Data == null)
            {
                throw new HttpRequestException($"Failed to retrieve run step. Status code: {response.StatusCode}");
            }

            runStep = response.Data;
        }
        catch (HttpRequestException exception)
        {
            _logger.LogError(exception, "Exception while making a read request to data object 'RunStepDataObject'");
            throw;
        }

        if (runStep != null)
        {
            yield return runStep;
        }
    }
}