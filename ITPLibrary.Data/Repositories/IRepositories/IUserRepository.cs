using ITPLibrary.Data.Entities;

namespace ITPLibrary.Data.Repositories.IRepositories
{
    public interface IUserRepository
    {
        Task<User> RegisterUserAsync(User user);
        Task<User> GetUserByEmailAsync(string email);
        Task<bool> UserExistsAsync(string email);
    }
}
