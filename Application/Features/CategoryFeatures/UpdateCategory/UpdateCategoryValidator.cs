using Application.Repositories;
using FluentValidation;

namespace Application.Features.UpdateCategory
{
    public sealed class UpdateCategoryValidator: AbstractValidator<UpdateCategoryRequest>
    {
        private readonly ICategoryRepository _repository;
        public UpdateCategoryValidator(ICategoryRepository repository)
        {
            _repository = repository;
            RuleFor(x => x.ItemId).NotEmpty().WithMessage("Item Id can not be empty");
            RuleFor(x => x.ItemId).MustAsync((ItemId,_)=> this.IsCategoryExists(ItemId)).WithMessage("There is no data found with the give itemId");
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.IsDefault).NotNull();
            RuleFor(x => x.IsExpense).NotNull();
        }

        private async Task<bool> IsCategoryExists(string itemId)
        {
            return await _repository.BeAnExistingCategory(itemId);
        }
    }
}
