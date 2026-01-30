public interface IAuthService
{
    void Register(RegisterDto dto);
    AuthResponse Login(LoginDto dto);
    string RefreshToken(string refreshToken);
}