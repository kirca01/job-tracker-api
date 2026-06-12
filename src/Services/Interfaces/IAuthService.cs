using JobTracker.DTOs;

namespace JobTracker.Services.Interfaces;

public interface IAuthService
{
    Task<AuthResponseDto?> Register(RegisterDto dto);
    Task<AuthResponseDto?> Login(LoginDto dto);
}