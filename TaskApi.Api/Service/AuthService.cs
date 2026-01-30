using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;

public class AuthService : IAuthService
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _config;

    public AuthService(AppDbContext context,IConfiguration config)
    {
        _config = config;
        _context = context;
    }

    public void Register(RegisterDto dto)
    {
        var hash = PasswordHasher.Hash(dto.Password,out var salt);

        var user = new User
        {
            Username = dto.Username,
            PasswordHash  = hash,
            PasswordSalt = salt,
            Role = "User"
        };

        _context.Users.Add(user);
        _context.SaveChanges();
    }

    // public string Login(LoginDto dto)
    // {
    //     var user = _context.Users.FirstOrDefault(u => u.Username == dto.Username);
    //     if (user == null)
    //         throw new UnauthorizedAccessException("Invalid username or password");

    //     var valid = PasswordHasher.Verify(dto.password,user.PasswordHash,user.PasswordSalt);

    //     if (!valid)
    //     {
    //         throw new UnauthorizedAccessException("Invalid username or password");
    //     }

    //     return GenerateToken(user);
    // }


    public AuthResponse Login(LoginDto dto)
    {
        var user = _context.Users.FirstOrDefault(u => u.Username == dto.Username);
        if(user == null)
            throw new UnauthorizedAccessException("Invalid username or password");

        var valid = PasswordHasher.Verify(
            dto.password,
            user.PasswordHash,
            user.PasswordSalt
        );

        if(!valid)
            throw new UnauthorizedAccessException("Invalid username or password");

        user.RefreshToken = GenerateRefreshToken();
        user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);

        _context.SaveChanges();

        return new AuthResponse
        {
            AccessToken = GenerateToken(user),
            RefreshToken = user.RefreshToken
        };
    }

    private string GenerateToken(User user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Name,user.Username),
            new Claim(ClaimTypes.Role,user.Role)
        };


        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));

        var creds = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer:_config["Jwt:Issuer"],
            audience:_config["Jwt:Audience"],
            claims:claims,
            expires:DateTime.Now.AddMinutes(5),
            signingCredentials:creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private string GenerateRefreshToken()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
    }

    public string RefreshToken(string refreshToken)
    {
        var user = _context.Users.FirstOrDefault(u => 
        u.RefreshToken == refreshToken && u.RefreshTokenExpiry > DateTime.UtcNow
        );

        if(user == null)
            throw new UnauthorizedAccessException("Invalid or expired refreshtoken");
        
        return GenerateToken(user);
    }
}