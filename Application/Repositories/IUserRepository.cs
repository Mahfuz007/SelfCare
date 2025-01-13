using Application.Common;

namespace Application.Repositories
{
    public interface IUserRepository
    {
        Task<CommonResponse> CreateUserAsync();
        Task<CommonResponse> UpdateUserAsync();
        Task<CommonResponse> GetUsers();
        Task<bool> IsUserExists(string email);
    }
}
