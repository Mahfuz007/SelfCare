using Application.Features.CategoryFeatures.CreateCategory;
using Application.Features.CategoryFeatures.GetCategory;
using Application.Features.CategoryFeatures.UpdateCategory;
using Domain.Entities;

namespace Application.Repositories
{
    public interface ICategoryRepository
    {
        Task<CreateCategoryResponse> CreateCategory(CreateCategoryRequest createCategoryRequest);
        Task<UpdateCategoryResponse> UpdateCategory(UpdateCategoryRequest updateCategoryRequest);
        Task<bool> BeAnExistingCategory(string id);
        Task<List<GetCategoryResponse>> GetCategory(GetCategoryRequest request);
        Task<bool> DeleteCategory(string categoryId);
        Task<Category> GetSpecificExpenseCategory(Expense expense);
    }
}
