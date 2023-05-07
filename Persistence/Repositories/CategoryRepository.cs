using Application.Repositories;
using Domain.Entities;
using Persistence.Context;

namespace Persistence.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(IMongoDbSettings context) : base(context)
        {
        }

        public async Task<bool> BeAnExistingCategory(string id)
        {
            var result = await FindByIdAsync(id);
            return result is not null;
        }
    }
}
