using FlightAggregatorApi.Abstracts;
using FlightAggregatorApi.Data;
using FlightAggregatorShared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace FlightAggregatorApi.Controllers;

[Route("api")]
[ApiController]
public class AuthorizeController(IGoogleAuthorization googleAuthorization, AppDbContext context) : ControllerBase
{
    [HttpGet]
    public IActionResult Authorize() => Ok(googleAuthorization.GetAuthorizationUrl());

    [HttpGet("callback")]
    public async Task<IActionResult> Callback(string code)
    {
        var userCredential = await googleAuthorization.ExchangeCodeForToken(code);
        var _credential = await context.Credentials
            .FirstOrDefaultAsync(x => x.AccessToken == userCredential.Token.AccessToken);

        return Redirect($"https://localhost:7002/connect/{_credential!.UserId}");
    }

    [HttpGet("token/{userId}")]
    public async Task<IActionResult> GetAccessToken(string userId)
    {
        Guid _userId = Guid.Empty;
        try
        {
            _userId = Guid.Parse(userId);
        }
        catch
        {
            return Unauthorized();
        }

        var credential = await context.Credentials.FirstOrDefaultAsync(c => c.UserId == _userId);
        return Ok(JsonSerializer.Serialize(new Token(credential!.AccessToken, credential.UserId.ToString())));
    }
}
