using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace CodingMilitia.PlayBall.WebFrontend.BackForFront.Benchmarks
{
    public class Attempt04HashCodeBasedDictionaryWithComplexValuePlusSpanManipulation : IProxiedApiRouteEndpointLookup
    {
        // The int dictionary + complex value strategy can be simplified if we're able to lookup directly with a ReadOnlySpan<char>
        // Work in progress here -> https://github.com/dotnet/corefx/issues/31942
        
        private readonly Dictionary<int, Holder[]> _routeMatcher;

        public Attempt04HashCodeBasedDictionaryWithComplexValuePlusSpanManipulation(Dictionary<string, string> routeToEndpointMap)
        {
            var tempRouteMatcher = new Dictionary<int, List<Holder>>();
            foreach (var entry in routeToEndpointMap)
            {
                var hashCode = entry.Key.GetHashCode();
                if (tempRouteMatcher.TryGetValue(hashCode, out var route))
                {
                    route.Add(new Holder(entry.Key, entry.Value));
                }
                else
                {
                    tempRouteMatcher.Add(hashCode, new List<Holder> {new Holder(entry.Key, entry.Value)});
                }
            }

            _routeMatcher = tempRouteMatcher.ToDictionary(e => e.Key, e => e.Value.ToArray());
        }

        public bool TryGet(PathString path, out string endpoint)
        {
            endpoint = null;
            var pathSpan = path.Value.AsSpan();
            var basePathEnd = pathSpan.Slice(1, pathSpan.Length - 1).IndexOf('/');
            var basePath = pathSpan.Slice(1, basePathEnd > 0 ? basePathEnd : pathSpan.Length - 1);

            if (_routeMatcher.TryGetValue(string.GetHashCode(basePath), out var routes))
            {
                endpoint = FindRoute(basePath, routes);
                return endpoint != null;
            }

            return false;
        }

        private static string FindRoute(ReadOnlySpan<char> route, Holder[] routes)
        {
            foreach(var currentRoute in routes)
            {
                if (route.Equals(currentRoute.route, StringComparison.InvariantCultureIgnoreCase))
                {
                    return currentRoute.endpoint;
                }
            }
            return null;
        }

        private class Holder
        {
            public readonly string route;
            public readonly string endpoint;

            public Holder(string route, string endpoint)
            {
                this.route = route;
                this.endpoint = endpoint;
            }
        }
    }
}