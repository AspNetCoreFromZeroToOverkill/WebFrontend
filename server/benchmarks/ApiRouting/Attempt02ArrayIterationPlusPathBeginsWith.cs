using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace CodingMilitia.PlayBall.WebFrontend.BackForFront.Benchmarks.ProxiedApiRouteEndpointLookup
{
    public class Attempt02ArrayIterationPlusPathBeginsWith : IProxiedApiRouteEndpointLookup
    {
        private readonly (string route, string endpoint)[] _routeCollection;

        public Attempt02ArrayIterationPlusPathBeginsWith(Dictionary<string, string> routeToEndpointMap)
        {
            _routeCollection = routeToEndpointMap.Select(e => (route: $"/{e.Key}", endpoint: e.Value)).ToArray();
        }

        public bool TryGet(PathString path, out string endpoint)
        {
            foreach (var e in _routeCollection)
            {
                if (path.StartsWithSegments(e.route))
                {
                    endpoint = e.endpoint;
                    return true;
                }
            }

            endpoint = null;
            return false;
        }
    }
}