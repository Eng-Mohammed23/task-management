using TaskManagement.Api.Contracts.Users;

namespace TaskManagement.Api.Services;

public interface IUserService
{
    Task<Result<AuthResponse>> GetTokenAsync(string email, string password, CancellationToken cancellation = default);
    Task<Result<AuthResponse>> GetRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellation = default);
    Task<Result<AuthResponse>> RegisterAsync(string email, string password, CancellationToken cancellationToken = default);
    Task<Result> ResetPasswordAsync(string email, string token, string newPassword, CancellationToken cancellationToken = default);


}
