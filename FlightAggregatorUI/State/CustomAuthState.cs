using FlightAggregatorShared;
using FlightAggregatorShared.TokenHandler;
using Microsoft.AspNetCore.Components.Authorization;
using NetcodeHub.Packages.Extensions.LocalStorage;
using System.Security.Claims;
using System.Text.Json;

namespace FlightAggregatorUI.State;

public class CustomAuthState(ILocalStorageService localStorageService) : AuthenticationStateProvider
{
    private ClaimsPrincipal claimsPrincipal = new(new ClaimsPrincipal());
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            var token = await localStorageService.GetItemAsStringAsync(Constant.Key);
            if (!string.IsNullOrEmpty(token))
            {
                var tokenModel = JsonSerializer.Deserialize<Token>(token);
                claimsPrincipal = SetClaimPrincipal(tokenModel!.UserId);
                return await Task.FromResult(new AuthenticationState(claimsPrincipal));
            }
            return await Task.FromResult(new AuthenticationState(claimsPrincipal));
        }
        catch
        {
            return await Task.FromResult(new AuthenticationState(claimsPrincipal));
        }
    }

    private ClaimsPrincipal SetClaimPrincipal(string userId)
    {
        try
        {
            Claim[] claims = [new(ClaimTypes.NameIdentifier, userId)];
            return new ClaimsPrincipal(new ClaimsIdentity(claims, Constant.Scheme));
        }
        catch
        {
            return new(new ClaimsIdentity());
        }
    }

    public void NotifyAuthStateChanged() =>
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
}
