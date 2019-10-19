using System;
using System.Collections.Generic;
using CodingMilitia.PlayBall.WebFrontend.BackForFront.Web.ApiRouting;
using Xunit;

namespace CodingMilitia.PlayBall.WebFrontend.BackForFront.Web.Test.ApiRouting
{
    public class ProxiedApiRouteEndpointLookupTests
    {
        [Fact]
        public void WhenUsingAnEmptyLookupDictionaryThenNoRouteIsMatched()
        {
            var lookup = new ProxiedApiRouteEndpointLookup(new Dictionary<string, string>());

            var found = lookup.TryGet("/non-existent-route", out _);

            Assert.False(found);
        }
        
        [Fact]
        public void WhenProvidingANullDictionaryThenTheConstructorThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new ProxiedApiRouteEndpointLookup(null));
        }

        [Theory]
        [InlineData("/non-existent-route")]
        [InlineData("/test-route-almost")]
        public void WhenLookingUpANonExistentRouteThenNothingIsFound(string nonExistentRoute)
        {
            var lookup = new ProxiedApiRouteEndpointLookup(new Dictionary<string, string>
            {
                ["test-route"] = "test-endpoint"
            });

            var found = lookup.TryGet(nonExistentRoute, out _);

            Assert.False(found);
        }

        [Theory]
        [InlineData("/test-route", "test-endpoint")]
        [InlineData("/another-test-route/some/more/segments", "another-test-endpoint")]
        public void WhenLookingUpExistingRouteThenItIsFound(string route, string expectedEndpoint)
        {
            var lookup = new ProxiedApiRouteEndpointLookup(
                new Dictionary<string, string>
                {
                    ["test-route"] = "test-endpoint",
                    ["another-test-route"]= "another-test-endpoint"
                });

            var found = lookup.TryGet(route, out var endpoint);

            Assert.True(found);
            Assert.Equal(expectedEndpoint, endpoint);
        }
    }
}