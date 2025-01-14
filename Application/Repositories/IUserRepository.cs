using Application.Common;

namespace Application.Repositories
{
    public interface IUserRepository
    {
        Task<CommonResponse> CreateUserAsync(string email, string password);
        Task<CommonResponse> UpdateUserAsync();
        Task<CommonResponse> GetUsers();
        Task<bool> IsUserExists(string email);
    }
}
