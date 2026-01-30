using Azure.Core;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public IActionResult Register(RegisterDto dto)
    {
        _authService.Register(dto);
        return Ok("User registered");
    }

    [HttpPost("login")]
    public IActionResult Login(LoginDto dto)
    {
        var response = _authService.Login(dto);
        return Ok(response);
    }

    [HttpPost("refresh")]
    public IActionResult Refresh(RefreshTokenDto dto)
    {
        var newToken = _authService.RefreshToken(dto.RefreshToken);
        return Ok(new {accessToken = newToken});
    }
}