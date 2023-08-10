using Application.Common;
using Application.Repositories;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.CategoryFeatures.DeleteCategory
{
    public sealed class DeleteCategoryValidator : AbstractValidator<DeleteCategoryRequest>
    {
        private readonly ICategoryRepository _categoryRepository;
        public DeleteCategoryValidator(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
            RuleFor(x => x.CategoryId).NotEmpty().WithMessage("Category Id can not be empty");
            RuleFor(x => x.CategoryId).MustAsync((categoryId, _)=> this.IsCategoryExists(categoryId)).WithMessage("NO Category found with give Category Id");
        }

        private async Task<bool> IsCategoryExists(string itemId)
        {
            return await _categoryRepository.BeAnExistingCategory(itemId);
        }
    }
}
