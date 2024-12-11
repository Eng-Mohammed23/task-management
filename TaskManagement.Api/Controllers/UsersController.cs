using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurveyBasket.Api.Abstractions;
using TaskManagement.Api.Services;

namespace TaskManagement.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UsersController(IUserService userService) : ControllerBase
{
    private readonly IUserService _userService = userService;

    [HttpPost("login")]
    public async Task<IActionResult> Login(string email, string password, CancellationToken cancellation)
    {
        var result = await _userService.GetTokenAsync(email, password, cancellation);
        return result.IsSuccess? Ok(result.Value) : result.ToProblem();
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(string email, string password, CancellationToken cancellation)
    {
        var result = await _userService.RegisterAsync(email, password, cancellation);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }
    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshToken(string email, string password, CancellationToken cancellation)
    {
        var result = await _userService.GetRefreshTokenAsync(email, password, cancellation);
        return result.IsSuccess ? Ok() : result.ToProblem();
    }

    [HttpPut("")]
    public async Task<IActionResult> ResetPassword(string email, string token, string newPassword, CancellationToken cancellationToken)
    {
        var result = await _userService.ResetPasswordAsync(email, token, newPassword, cancellationToken);

        return result.IsSuccess? NoContent(): result.ToProblem();
    }
}
