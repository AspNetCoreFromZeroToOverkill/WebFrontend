using System;
using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using IdentityModel;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CodingMilitia.PlayBall.WebFrontend.BackForFront.Web.AuthTokenHelpers
{
    /// <inheritdoc />
    public class TokenRefresher : ITokenRefresher
    {
        private static readonly TimeSpan TokenRefreshThreshold = TimeSpan.FromSeconds(30);

        private readonly HttpClient _httpClient;
        private readonly IDiscoveryCache _discoveryCache;
        private readonly ILogger<TokenRefresher> _logger;

        public TokenRefresher(HttpClient httpClient,
            IDiscoveryCache discoveryCache,
            ILogger<TokenRefresher> logger)
        {
            _httpClient = httpClient;
            _discoveryCache = discoveryCache;
            _logger = logger;
        }

        /// <inheritdoc />
        public async Task<TokenRefreshResult> TryRefreshTokenIfRequiredAsync(
            string refreshToken,
            string expiresAt,
            CancellationToken ct)
        {
            if (string.IsNullOrWhiteSpace(refreshToken))
            {
                return TokenRefreshResult.Failed();
            }
            
            if (!DateTime.TryParse(expiresAt, out var expiresAtDate) || expiresAtDate >= GetRefreshThreshold())
            {
                return TokenRefreshResult.NoRefreshNeeded();
            }

            var discovered = await _discoveryCache.GetAsync();
            var tokenResult = await _httpClient.RequestRefreshTokenAsync(
                new RefreshTokenRequest
                {
                    Address = discovered.TokenEndpoint,
                    ClientId = "WebFrontend",
                    ClientSecret = "secret",
                    RefreshToken = refreshToken
                }, ct);

            if (tokenResult.IsError)
            {
                _logger.LogDebug(
                    "Unable to refresh token, reason: {refreshTokenErrorDescription}",
                    tokenResult.ErrorDescription);
                return TokenRefreshResult.Failed();
            }

            var newAccessToken = tokenResult.AccessToken;
            var newRefreshToken = tokenResult.RefreshToken;
            var newExpiresAt = CalculateNewExpiresAt(tokenResult.ExpiresIn);

            return TokenRefreshResult.Success(newAccessToken, newRefreshToken, newExpiresAt);
        }

        private static string CalculateNewExpiresAt(int expiresIn)
        {
            // TODO: abstract usages of DateTime to ease unit tests
            return (DateTime.UtcNow + TimeSpan.FromSeconds(expiresIn)).ToString("o", CultureInfo.InvariantCulture);
        }

        private static DateTime GetRefreshThreshold()
        {
            // TODO: abstract usages of DateTime to ease unit tests
            return DateTime.UtcNow + TokenRefreshThreshold;
        }
    }
}