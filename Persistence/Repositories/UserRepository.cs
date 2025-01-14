using Application.Common;
using Application.Repositories;
using Domain.Entities;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IBaseRepository<User> _repository;
        private readonly static byte[] passwordSaltBytes = Encoding.UTF8.GetBytes("mahfuz_837hhdsfjh#lkjfgl@003@4kjgfsdlg&dfjkads$faklf!!*%8384dskfjdskf");

        public UserRepository(IBaseRepository<User> repository)
        {
            _repository = repository;
        }

        public async Task<CommonResponse> CreateUserAsync(string email, string password)
        {
            var user = new User 
            { 
                Email = email, 
                Password = HashedPassword(password),
                Roles = new List<string> { "app-user" }
            };
            await _repository.InsertOneAsync(user);
            return new CommonResponse(HttpStatusCode.Created, "User Created");
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

        private string HashedPassword(string password)
        {
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            var saltedValue = passwordSaltBytes.Concat(passwordBytes).ToArray();
            var hashBytes = SHA256.Create().ComputeHash(saltedValue);
            return Encoding.UTF8.GetString(hashBytes, 0, hashBytes.Length);
        }
    }
}
