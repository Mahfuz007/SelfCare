using Application.Common;
using Application.Repositories;
using FluentValidation;
using MediatR;

namespace Application.Features.CategoryFeatures.CreateCategory
{
    public sealed class CreateCategoryHandler : IRequestHandler<CreateCategoryRequest, CommonResponse>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IValidator<CreateCategoryRequest> _validator;

        public CreateCategoryHandler(ICategoryRepository categoryRepository, IValidator<CreateCategoryRequest> validator)
        {
            _categoryRepository = categoryRepository;
            _validator = validator;
        }

        public async Task<CommonResponse> Handle(CreateCategoryRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return new CommonResponse(validationResult);
            }
            
            var result = await _categoryRepository.CreateCategory(request);
            return result;
        }
    }
}
