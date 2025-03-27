using Connector.Client;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Xchange.Connector.SDK.Client.Testing;

namespace Connector.Connections
{
    public class ConnectionTestHandler : IConnectionTestHandler
    {
        private readonly ILogger<IConnectionTestHandler> _logger;
        private readonly ApiClient _apiClient;

        public ConnectionTestHandler(ILogger<IConnectionTestHandler> logger, ApiClient apiClient)
        {
            _logger = logger;
            _apiClient = apiClient;
        }

        public async Task<TestConnectionResult> TestConnection()
        {
            try
            {
                // Test the connection by making a request to the /models endpoint
                var response = await _apiClient.GetRecords<object>("models", 1);

                if (response == null)
                {
                    return new TestConnectionResult()
                    {
                        Success = false,
                        Message = "Failed to get response from OpenAI API",
                        StatusCode = 500
                    };
                }

                if (response.IsSuccessful)
                {
                    return new TestConnectionResult()
                    {
                        Success = true,
                        Message = "Successfully connected to OpenAI API",
                        StatusCode = response.StatusCode
                    };
                }

                switch (response.StatusCode)
                {
                    case 401:
                        return new TestConnectionResult()
                        {
                            Success = false,
                            Message = "Invalid API key: Unauthorized access",
                            StatusCode = response.StatusCode
                        };
                    case 403:
                        return new TestConnectionResult()
                        {
                            Success = false,
                            Message = "Invalid API key: Forbidden access",
                            StatusCode = response.StatusCode
                        };
                    case 429:
                        return new TestConnectionResult()
                        {
                            Success = false,
                            Message = "Rate limit exceeded. Please try again later",
                            StatusCode = response.StatusCode
                        };
                    default:
                        return new TestConnectionResult()
                        {
                            Success = false,
                            Message = $"Connection test failed with status code {response.StatusCode}",
                            StatusCode = response.StatusCode
                        };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error testing OpenAI API connection");
                return new TestConnectionResult()
                {
                    Success = false,
                    Message = $"Connection test failed: {ex.Message}",
                    StatusCode = 500
                };
            }
        }
    }
}
