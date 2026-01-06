public interface IAuthService
{
    void Register(RegisterDto dto);
    string Login(LoginDto dto);
}