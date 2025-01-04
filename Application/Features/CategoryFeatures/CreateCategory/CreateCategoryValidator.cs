using Application.Repositories;
using FluentValidation;

namespace Application.Features.CategoryFeatures.CreateCategory
{
    public sealed class CreateCategoryValidator: AbstractValidator<CreateCategoryRequest>
    {
        private readonly ICategoryRepository _categoryRepository;
        public CreateCategoryValidator(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
            RuleFor(x => x.Name).NotEmpty().MinimumLength(3).MaximumLength(20);
            RuleFor(x => x.IsExpense).NotNull();
            RuleFor(x => x.IsDefault).NotNull();
        }

        private async Task<bool> IsCategoryExists(string itemId)
        {
            return !await _categoryRepository.BeAnExistingCategory(itemId);
        }
    }
}
