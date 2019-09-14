using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace CodingMilitia.PlayBall.WebFrontend.BackForFront.Benchmarks
{
    public class Attempt01DictionaryPlusStringManipulation : IProxiedApiRouteEndpointLookup
    {
        private readonly Dictionary<string, string> _routeToEndpointMap;

        public Attempt01DictionaryPlusStringManipulation(Dictionary<string, string> routeToEndpointMap)
        {
            _routeToEndpointMap = routeToEndpointMap;
        }

        public bool TryGet(PathString path, out string endpoint)
        {
            var pathString = path.Value;
            var basePathEnd = pathString.Substring(1, pathString.Length - 1).IndexOf('/');
            var basePath = pathString.Substring(1, basePathEnd > 0 ? basePathEnd : pathString.Length - 1);
            return _routeToEndpointMap.TryGetValue(basePath, out endpoint);
        }
    }
}