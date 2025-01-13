using Application.Common;
using Application.Repositories;
using Domain.Entities;

namespace Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IBaseRepository<User> _repository;

        public UserRepository(IBaseRepository<User> repository)
        {
            _repository = repository;
        }

        public Task<CommonResponse> CreateUserAsync()
        {
            throw new NotImplementedException();
        }

        public Task<CommonResponse> GetUsers()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IsUserExists(string email)
        {
            var result = await _repository.FindOneAsync(x => x.Email == email);
            return result is not null;
        }

        public Task<CommonResponse> UpdateUserAsync()
        {
            throw new NotImplementedException();
        }
    }
}
