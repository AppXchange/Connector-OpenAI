using System;
using Xchange.Connector.SDK.Client.AuthTypes;
using Xchange.Connector.SDK.Client.ConnectionDefinitions.Attributes;

namespace Connector.Connections;

[ConnectionDefinition(title: "OpenAI API Key Auth", description: "Authentication for OpenAI API using API key")]
public class ApiKeyAuth : IApiKeyAuth
{
    [ConnectionProperty(title: "API Key", description: "Your OpenAI API key", isRequired: true, isSensitive: true)]
    public string ApiKey { get; init; } = string.Empty;

    [ConnectionProperty(title: "Organization ID", description: "Your OpenAI Organization ID (optional)", isRequired: false, isSensitive: false)]
    public string? OrganizationId { get; init; }

    [ConnectionProperty(title: "Project ID", description: "Your OpenAI Project ID (optional)", isRequired: false, isSensitive: false)]
    public string? ProjectId { get; init; }

    [ConnectionProperty(title: "Connection Environment", description: "The environment to connect to", isRequired: true, isSensitive: false)]
    public ConnectionEnvironmentApiKeyAuth ConnectionEnvironment { get; set; } = ConnectionEnvironmentApiKeyAuth.Unknown;

    public string BaseUrl
    {
        get
        {
            switch (ConnectionEnvironment)
            {
                case ConnectionEnvironmentApiKeyAuth.Production:
                    return "https://api.openai.com/v1";
                case ConnectionEnvironmentApiKeyAuth.Test:
                    return "https://api.openai.com/v1";
                default:
                    throw new Exception("No base url was set.");
            }
        }
    }
}

public enum ConnectionEnvironmentApiKeyAuth
{
    Unknown = 0,
    Production = 1,
    Test = 2
}