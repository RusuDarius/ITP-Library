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

        public UserService(IMapper mapper, IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _mapper = mapper;
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
            user.Password = PasswordHasher.HashPassword(registerUserDto.Password);

            user = await _userRepository.RegisterUserAsync(user);
            return (_mapper.Map<UserDto>(user), null);
        }

        public async Task<bool> UserExistsAsync(string email)
        {
            return await _userRepository.UserExistsAsync(email);
        }
    }
}
