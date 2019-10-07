using System;
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnostics.Windows.Configs;
using BenchmarkDotNet.Running;
using Microsoft.AspNetCore.Http;

namespace CodingMilitia.PlayBall.WebFrontend.BackForFront.Benchmarks.ProxiedApiRouteEndpointLookup
{
    public class Program
    {
        static void Main(string[] args)
        {
            DoSanityCheck();

            var summary = BenchmarkRunner.Run<ProxiedApiRouteEndpointLookupBenchmark>();
        }

        [RankColumn]
        [MemoryDiagnoser]
        //[InliningDiagnoser(logFailuresOnly: false)] //uncomment to see inlining results
        public class ProxiedApiRouteEndpointLookupBenchmark
        {
            [Params(10, 100, 1000)] public int MaxRoutes { get; set; }

            private PathString _path;
            private static Attempt01DictionaryPlusStringManipulation _attempt01;
            private static Attempt02ArrayIterationPlusPathBeginsWith _attempt02;
            private static Attempt03HashCodeBasedDoubleDictionaryPlusSpanManipulation _attempt03;
            private static Attempt04HashCodeBasedDictionaryWithComplexValuePlusSpanManipulation _attempt04;
            private static Attempt0504WithAggressiveInlining _attempt05;

            [GlobalSetup]
            public void Setup()
            {
                var routeMap = CreateRouteMap(MaxRoutes);
                _path = $"/route{MaxRoutes - 1}/some/more/things/in/the/path";

                _attempt01 = new Attempt01DictionaryPlusStringManipulation(routeMap);
                _attempt02 = new Attempt02ArrayIterationPlusPathBeginsWith(routeMap);
                _attempt03 = new Attempt03HashCodeBasedDoubleDictionaryPlusSpanManipulation(routeMap);
                _attempt04 = new Attempt04HashCodeBasedDictionaryWithComplexValuePlusSpanManipulation(routeMap);
                _attempt05 = new Attempt0504WithAggressiveInlining(routeMap);
            }


            [Benchmark(Baseline = true)]
            public string Attempt01DictionaryPlusStringManipulation()
            {
                _attempt01.TryGet(_path, out var result);
                return result;
            }

            [Benchmark]
            public string Attempt02ArrayIterationPlusPathBeginsWith()
            {
                _attempt02.TryGet(_path, out var result);
                return result;
            }

            [Benchmark]
            public string Attempt03HashCodeBasedDoubleDictionaryPlusSpanManipulation()
            {
                _attempt03.TryGet(_path, out var result);
                return result;
            }

            [Benchmark]
            public string Attempt04HashCodeBasedDictionaryWithComplexValuePlusSpanManipulation()
            {
                _attempt04.TryGet(_path, out var result);
                return result;
            }

            [Benchmark]
            public string Attempt0504WithAggressiveInlining()
            {
                _attempt05.TryGet(_path, out var result);
                return result;
            }
        }

        private static void DoSanityCheck()
        {
            const int maxRoutes = 100;
            var route = $"/route{maxRoutes - 1}/some/more/things/in/the/path";

            var routeMap = CreateRouteMap(maxRoutes);

            var results = typeof(Program)
                .Assembly
                .GetTypes()
                .Where(t => t.IsClass && typeof(IProxiedApiRouteEndpointLookup).IsAssignableFrom(t))
                .ToDictionary(
                    t => t.Name,
                    t =>
                    {
                        var lookup = t
                            .GetConstructor(new[] {routeMap.GetType()})
                            .Invoke(new object[] {routeMap}) as IProxiedApiRouteEndpointLookup;

                        return lookup.TryGet(route, out var endpoint) ? endpoint : null;
                    });


            var expectedEndpoint = $"route{maxRoutes - 1}endpoint";
            if (results.Values.Any(r => r != expectedEndpoint))
            {
                var wrongResults = string.Join(Environment.NewLine,
                    results.Where(r => r.Value != expectedEndpoint).Select(r => $"{r.Key}: {r.Value}"));
                Console.WriteLine(
                    $"The following lookups are not working correctly:{Environment.NewLine}{wrongResults}");

                throw new Exception("Something's not right!");
            }
        }

        private static Dictionary<string, string> CreateRouteMap(int maxRoutes)
            => Enumerable
                .Range(0, maxRoutes)
                .ToDictionary(i => $"route{i}", i => $"route{i}endpoint");
    }
}