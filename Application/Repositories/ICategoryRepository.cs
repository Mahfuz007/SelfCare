using Application.Features.CategoryFeatures.CreateCategory;
using Application.Features.UpdateCategory;

namespace Application.Repositories
{
    public interface ICategoryRepository
    {
        Task<CreateCategoryResponse> CreateCategory(CreateCategoryRequest createCategoryRequest);
        Task<UpdateCategoryResponse> UpdateCategory(UpdateCategoryRequest updateCategoryRequest);
        Task<bool> BeAnExistingCategory(string id); 
    }
}
