using Application.Common;
using Application.Repositories;
using MediatR;

namespace Application.Features.CategoryFeatures.GetCategory
{
    public sealed class GetCategoryHandler : IRequestHandler<GetCategoryRequest, GetCategoryResponse>
    {
        private readonly ICategoryRepository _categoryRepository;
        public GetCategoryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public Task<GetCategoryResponse> Handle(GetCategoryRequest request, CancellationToken cancellationToken)
        {
            return _categoryRepository.GetCategory(request.CategoryId);
        }
    }
}
