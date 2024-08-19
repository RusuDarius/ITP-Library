using AutoMapper;
using ITPLibrary.Core.Dtos.UserDtos;
using ITPLibrary.Core.Services.IServices;
using ITPLibrary.Data.Entities;
using ITPLibrary.Data.Repositories.IRepositories;
using ITPLibrary.Core.Utilities;

namespace ITPLibrary.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly string _jwtSecret;

        public UserService(IMapper mapper, IUserRepository userRepository, string jwtSecret)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _jwtSecret = jwtSecret;
        }

        public async Task<(UserDto user, string errorMessage)> RegisterUserAsync(
            RegisterUserDto registerUserDto
        )
        {
            if (registerUserDto.Password != registerUserDto.ConfirmPassword)
            {
                return (null, "Passwords do not match");
            }

            if (await _userRepository.UserExistsAsync(registerUserDto.Email))
            {
                return (null, "Email already exists");
            }

            var user = _mapper.Map<User>(registerUserDto);
            user.Password = PasswordHasher.HashPassword(registerUserDto.Password, out string salt);
            user.Salt = salt;

            user = await _userRepository.RegisterUserAsync(user);
            return (_mapper.Map<UserDto>(user), null);
        }

        public async Task<(LoginResponseDto response, string errorMessage)> LoginAsync(
            LoginRequestDto loginRequestDto
        )
        {
            var user = await _userRepository.GetUserByEmailAsync(loginRequestDto.Email);
            if (user == null)
            {
                return (null, "Invalid credentials")!;
            }

            if (!PasswordHasher.VerifyPassword(loginRequestDto.Password, user.Password, user.Salt))
            {
                return (null, "Invalid credentials");
            }

            var token = JwtTokenGenerator.GenerateJwtToken(user, _jwtSecret);
            return (new LoginResponseDto { AccessToken = token }, null);
        }

        public async Task<bool> UserExistsAsync(string email)
        {
            return await _userRepository.UserExistsAsync(email);
        }
    }
}
