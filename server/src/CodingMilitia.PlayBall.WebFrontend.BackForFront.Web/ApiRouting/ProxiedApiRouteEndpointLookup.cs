using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace CodingMilitia.PlayBall.WebFrontend.BackForFront.Web.ApiRouting
{
    public class ProxiedApiRouteEndpointLookup
    {
        private readonly Dictionary<string, string> _routeToEndpointMap;

        public ProxiedApiRouteEndpointLookup(Dictionary<string, string> routeToEndpointMap)
        {
            _routeToEndpointMap = routeToEndpointMap ?? throw new ArgumentNullException(nameof(routeToEndpointMap));
            
            // TODO: we should enforce that the routes are actually base routes, having a single segment,
            // otherwise, given the implemented logic, such cases will never be matched. 
        }

        public bool TryGet(PathString path, out string endpoint)
        {
            // If/when we get to index a string keyed dictionary with a span, improve this code to avoid allocations
            // Discussion here -> https://github.com/dotnet/corefx/issues/31942

            if (string.IsNullOrWhiteSpace(path.Value))
            {
                endpoint = null;
                return false;
            }
            
            var pathString = path.Value;
            var basePathEnd = pathString.Substring(1, pathString.Length - 1).IndexOf('/');
            var basePath = pathString.Substring(1, basePathEnd > 0 ? basePathEnd : pathString.Length - 1);
            return _routeToEndpointMap.TryGetValue(basePath, out endpoint);
        }
    }
}