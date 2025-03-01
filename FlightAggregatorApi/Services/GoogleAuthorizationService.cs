using FlightAggregatorApi.Abstracts;
using FlightAggregatorApi.Data;
using FlightAggregatorApi.Entity;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Responses;
using Microsoft.EntityFrameworkCore;

namespace FlightAggregatorApi.Services;


public class GoogleAuthorizationService(AppDbContext context, IGoogleAuthHelper googleHelper,
    IConfiguration configuration) : IGoogleAuthorization
{
    private string RedirectUrl = configuration["Google:RedirectUri"]!;
    public async Task<UserCredential> ExchangeCodeForToken(string code)
    {
        var flow = new GoogleAuthorizationCodeFlow(
            new GoogleAuthorizationCodeFlow.Initializer
            {
                ClientSecrets = googleHelper.GetClientSecrets(),
                Scopes = googleHelper.GetScopes()
            });
        var token = await flow.ExchangeCodeForTokenAsync("user", code, RedirectUrl, CancellationToken.None);
        context.Add(new Credential
        {
            AccessToken = token.AccessToken,
            RefreshToken = token.RefreshToken,
            ExpiresInSeconds = token.ExpiresInSeconds,
            IdToken = token.IdToken,
            UserId = Guid.NewGuid(),
            IssuedUtc = token.IssuedUtc
        });
        await context.SaveChangesAsync();
        return new UserCredential(flow, "user", token);
    }

    public string GetAuthorizationUrl() =>
      new GoogleAuthorizationCodeFlow(
          new GoogleAuthorizationCodeFlow.Initializer
          {
              ClientSecrets = googleHelper.GetClientSecrets(),
              Scopes = googleHelper.GetScopes(),
              Prompt = "consent"
          }).CreateAuthorizationCodeRequest(RedirectUrl).Build().ToString();

    public async Task<UserCredential> ValidateToken(string accessToken)
    {
        var _credential = await context.Credentials.FirstOrDefaultAsync(c => c.AccessToken == accessToken) ??
            throw new UnauthorizedAccessException("No Auth. Please login again");

        var flow = new GoogleAuthorizationCodeFlow(
             new GoogleAuthorizationCodeFlow.Initializer
             {
                 ClientSecrets = googleHelper.GetClientSecrets(),
                 Scopes = googleHelper.GetScopes()
             });

        var tokenRes = new TokenResponse
        {
            AccessToken = _credential.AccessToken,
            RefreshToken = _credential.RefreshToken,
            ExpiresInSeconds = _credential.ExpiresInSeconds,
            IdToken = _credential.IdToken,
            IssuedUtc = _credential.IssuedUtc
        };
        return new UserCredential(flow, "user", tokenRes);
    }
}
