namespace CodingMilitia.PlayBall.WebFrontend.BackForFront.Web.AuthTokenHelpers
{
    public class TokenRefreshResult
    {
        private static readonly TokenRefreshResult NoRefreshNeededResult =
            new TokenRefreshResult(true, false, null, null, null);

        private static readonly TokenRefreshResult FailedResult =
            new TokenRefreshResult(false, false, null, null, null);

        protected TokenRefreshResult(
            bool isSuccessResult,
            bool tokensRenewed,
            string accessToken,
            string refreshToken,
            string expiresAt)
        {
            IsSuccessResult = isSuccessResult;
            TokensRenewed = tokensRenewed;
            AccessToken = accessToken;
            RefreshToken = refreshToken;
            ExpiresAt = expiresAt;
        }

        public bool IsSuccessResult { get; }
        public bool TokensRenewed { get; }
        public string AccessToken { get; }
        public string RefreshToken { get; }
        public string ExpiresAt { get; }

        public static TokenRefreshResult Success(
            string accessToken,
            string refreshToken,
            string expiresAt)
        {
            return new TokenRefreshResult(true, true, accessToken, refreshToken, expiresAt);
        }

        public static TokenRefreshResult Failed() => FailedResult;

        public static TokenRefreshResult NoRefreshNeeded() => NoRefreshNeededResult;
    }
}