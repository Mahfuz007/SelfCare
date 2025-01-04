using Application.Common;
using Application.Features.CategoryFeatures.CreateCategory;
using Application.Features.CategoryFeatures.GetCategory;
using Application.Features.CategoryFeatures.UpdateCategory;
using Domain.Entities;

namespace Application.Repositories
{
    public interface ICategoryRepository
    {
        Task<CommonResponse> CreateCategory(CreateCategoryRequest createCategoryRequest);
        Task<CommonResponse> UpdateCategory(UpdateCategoryRequest updateCategoryRequest);
        Task<bool> BeAnExistingCategory(string id);
        Task<CommonResponse> GetCategory(GetCategoryRequest request);
        Task<CommonResponse> DeleteCategory(string categoryId);
        Task<Category> GetSpecificExpenseCategory(Expense expense);
    }
}
