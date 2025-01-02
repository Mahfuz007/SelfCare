using Application.Common;
using Application.Repositories;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.CategoryFeatures.DeleteCategory
{
    public sealed class DeleteCategoryHandler : IRequestHandler<DeleteCategoryRequest, CommonResponse>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IValidator<DeleteCategoryRequest> _validator;

        public DeleteCategoryHandler(ICategoryRepository categoryRepository, IValidator<DeleteCategoryRequest> validator)
        {
            _categoryRepository = categoryRepository;
            _validator = validator;
        }

        public async Task<CommonResponse> Handle(DeleteCategoryRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                return new CommonResponse(validationResult);
            }
            return await _categoryRepository.DeleteCategory(request.CategoryId);
        }
    }
}
