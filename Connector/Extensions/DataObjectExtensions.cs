using System.Collections.Generic;
using Xchange.Connector.SDK.CacheWriter;
using ESR.Hosting.CacheWriter;

namespace Connector.Extensions;

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