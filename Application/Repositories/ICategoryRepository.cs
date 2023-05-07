using Domain.Entities;

namespace Application.Repositories
{
    public interface ICategoryRepository: IBaseRepository<Category>
    {
        Task<bool> BeAnExistingCategory(string id); 
    }
}
