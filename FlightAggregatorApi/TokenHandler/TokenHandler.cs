using FlightAggregatorApi.Abstracts;
using FlightAggregatorApi.Data;
using FlightAggregatorApi.Entity;
using FlightAggregatorShared.TokenHandler;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace FlightAggregatorApi.TokenHandler;

public class GoogleAccessTokenAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
    ILoggerFactory logger, UrlEncoder encoder, TimeProvider timeProvider, AppDbContext dbContext,
     IGoogleAuthorization googleAuthorization)
    : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)
{
    protected async override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.ContainsKey("Authorization"))
        {
            return AuthenticateResult.Fail("Missing Auth Header");
        }

        string authHeader = Request.Headers.Authorization!;
        if (!authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
        {
            return AuthenticateResult.Fail("Invalid Auth Header");
        }

        string accessToken = authHeader["Bearer ".Length..].Trim();
        var userCredential = await googleAuthorization.ValidateToken(accessToken);
        Credential? user = await GetUserCredential(userCredential.Token.AccessToken);
        if (user == null)
        {
            AuthenticateResult.Fail("Invalid Access Token Provided");
        }

        List<Claim> claims = [new(ClaimTypes.NameIdentifier, user!.UserId.ToString())];
        var identity = new ClaimsIdentity(claims, Constant.Scheme);

        return AuthenticateResult.Success(
            new AuthenticationTicket(
                new ClaimsPrincipal(identity), Constant.Scheme)
            );
    }

    private async Task<Credential?> GetUserCredential(string accessToken)
    {
        return await dbContext.Credentials.FirstOrDefaultAsync(x => x.AccessToken == accessToken);
    }
}
