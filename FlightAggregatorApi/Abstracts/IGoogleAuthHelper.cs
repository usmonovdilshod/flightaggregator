using Google.Apis.Auth.OAuth2;

namespace FlightAggregatorApi.Abstracts;

public interface IGoogleAuthHelper
{
    string[] GetScopes();
    string ScopeToString();
    ClientSecrets GetClientSecrets();
}


