using Application.Common;
using Application.Repositories;
using FluentValidation;
using MediatR;


namespace Application.Features.CategoryFeatures.UpdateCategory
{
    public sealed class UpdateCategoryHandler : IRequestHandler<UpdateCategoryRequest, UpdateCategoryResponse>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IValidator<UpdateCategoryRequest> _validator;
        public UpdateCategoryHandler(ICategoryRepository categoryRepository, IValidator<UpdateCategoryRequest> validator)
        {
            _categoryRepository = categoryRepository;
            _validator = validator;
        }
        public async Task<UpdateCategoryResponse> Handle(UpdateCategoryRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new BadRequestException(validationResult.ToString());
            }
            return await _categoryRepository.UpdateCategory(request);
        }
    }
}
