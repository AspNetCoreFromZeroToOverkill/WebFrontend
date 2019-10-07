using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace CodingMilitia.PlayBall.WebFrontend.BackForFront.Benchmarks.ProxiedApiRouteEndpointLookup
{
    public class Attempt03HashCodeBasedDoubleDictionaryPlusSpanManipulation : IProxiedApiRouteEndpointLookup
    {
        // The double dictionary strategy can be simplified if we're able to lookup directly with a ReadOnlySpan<char>
        // Work in progress here -> https://github.com/dotnet/corefx/issues/31942

        private readonly Dictionary<string, string> _routeToEndpointMap;
        private readonly Dictionary<int, string[]> _routeMatcher;

        public Attempt03HashCodeBasedDoubleDictionaryPlusSpanManipulation(Dictionary<string, string> routeToEndpointMap)
        {
            _routeToEndpointMap = routeToEndpointMap ?? throw new ArgumentNullException(nameof(routeToEndpointMap));
            _routeMatcher = _routeToEndpointMap
                .Keys
                .GroupBy(
                    r => r.GetHashCode(),
                    r => r)
                .ToDictionary(
                    g => g.Key,
                    g => g.ToArray());
        }

        public bool TryGet(PathString path, out string endpoint)
        {
            endpoint = null;
            var pathSpan = path.Value.AsSpan();
            var basePathEnd = pathSpan.Slice(1, pathSpan.Length - 1).IndexOf('/');
            var basePath = pathSpan.Slice(1, basePathEnd > 0 ? basePathEnd : pathSpan.Length - 1);

            if (_routeMatcher.TryGetValue(string.GetHashCode(basePath), out var routes))
            {
                var route = FindRoute(basePath, routes);
                return !(route is null) && _routeToEndpointMap.TryGetValue(route, out endpoint);
            }

            return false;
        }

        private static string FindRoute(ReadOnlySpan<char> route, string[] routes)
        {
            foreach(var currentRoute in routes)
            {
                if (route.Equals(currentRoute, StringComparison.InvariantCultureIgnoreCase))
                {
                    return currentRoute;
                }
            }

            return null;
        }
    }
}