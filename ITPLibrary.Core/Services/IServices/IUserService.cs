using ITPLibrary.Core.Dtos.UserDtos;

namespace ITPLibrary.Core.Services.IServices
{
    public interface IUserService
    {
        Task<(UserDto user, string errorMessage)> RegisterUserAsync(
            RegisterUserDto registerUserDto
        );
        Task<bool> UserExistsAsync(string email);
        Task<(LoginResponseDto response, string errorMessage)> LoginAsync(
            LoginRequestDto loginRequestDto
        );
        Task<(bool success, string errorMessage)> RecoverPasswordAsync(
            PasswordRecoveryDto passwordRecoveryDto
        );
    }
}
