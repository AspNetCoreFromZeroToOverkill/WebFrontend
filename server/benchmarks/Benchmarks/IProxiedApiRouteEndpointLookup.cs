using Microsoft.AspNetCore.Http;

namespace CodingMilitia.PlayBall.WebFrontend.BackForFront.Benchmarks
{
    public interface IProxiedApiRouteEndpointLookup
    {
        bool TryGet(PathString path, out string endpoint);
    }
}