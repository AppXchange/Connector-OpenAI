using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xchange.Connector.SDK.Client.AuthTypes;
using Connector.Connections;

namespace Connector.Client;

public class ApiKeyAuthHandler : DelegatingHandler
{
    private readonly IApiKeyAuth _apiKeyAuth;

    public ApiKeyAuthHandler(IApiKeyAuth apiKeyAuth)
    {
        _apiKeyAuth = apiKeyAuth;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        // Remove any existing authorization headers
        request.Headers.Remove("Authorization");
        
        // Add the Bearer token authentication header
        request.Headers.Add("Authorization", $"Bearer {_apiKeyAuth.ApiKey}");

        // Add optional organization ID if available
        if (_apiKeyAuth is ApiKeyAuth apiKeyAuth)
        {
            if (!string.IsNullOrEmpty(apiKeyAuth.OrganizationId))
            {
                request.Headers.Remove("OpenAI-Organization");
                request.Headers.Add("OpenAI-Organization", apiKeyAuth.OrganizationId);
            }

            // Add optional project ID if available
            if (!string.IsNullOrEmpty(apiKeyAuth.ProjectId))
            {
                request.Headers.Remove("OpenAI-Project");
                request.Headers.Add("OpenAI-Project", apiKeyAuth.ProjectId);
            }
        }

        return await base.SendAsync(request, cancellationToken);
    }
}