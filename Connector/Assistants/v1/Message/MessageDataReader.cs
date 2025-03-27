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

namespace Connector.Assistants.v1.Message;

public class MessageDataReader : TypedAsyncDataReaderBase<MessageDataObject>
{
    private readonly ILogger<MessageDataReader> _logger;
    private readonly ApiClient _apiClient;

    public MessageDataReader(
        ILogger<MessageDataReader> logger,
        ApiClient apiClient)
    {
        _logger = logger;
        _apiClient = apiClient;
    }

    public override async IAsyncEnumerable<MessageDataObject> GetTypedDataAsync(
        DataObjectCacheWriteArguments? dataObjectRunArguments, 
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        if (dataObjectRunArguments == null)
        {
            throw new ArgumentNullException(nameof(dataObjectRunArguments));
        }

        if (!dataObjectRunArguments.TryGetParameterValue("threadId", out string? threadId) || string.IsNullOrEmpty(threadId))
        {
            throw new ArgumentException("Thread ID is required");
        }

        if (!dataObjectRunArguments.TryGetParameterValue("messageId", out string? messageId) || string.IsNullOrEmpty(messageId))
        {
            throw new ArgumentException("Message ID is required");
        }

        MessageDataObject? message = null;
        try
        {
            var response = await _apiClient.GetMessage(threadId, messageId, cancellationToken)
                .ConfigureAwait(false);

            if (!response.IsSuccessful || response.Data == null)
            {
                throw new HttpRequestException($"Failed to retrieve message. Status code: {response.StatusCode}");
            }

            message = response.Data;
        }
        catch (HttpRequestException exception)
        {
            _logger.LogError(exception, "Exception while making a read request to data object 'MessageDataObject'");
            throw;
        }

        if (message != null)
        {
            yield return message;
        }
    }
}