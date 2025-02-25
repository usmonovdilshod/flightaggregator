using Google.Apis.Auth.OAuth2;

namespace FlightAggregatorApi.Abstracts;

public interface IGoogleAuthorization
{
    string GetAuthorizationUrl();
    Task<UserCredential> ExchangeCodeForToken(string code);
    Task<UserCredential> ValidateToken(string accessToken);
}
