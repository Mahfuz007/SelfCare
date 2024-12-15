using Application.Common;
using Application.Repositories;
using MediatR;

namespace Application.Features.CategoryFeatures.GetCategory
{
    public sealed class GetCategoryHandler : IRequestHandler<GetCategoryRequest, List<GetCategoryResponse>>
    {
        private readonly ICategoryRepository _categoryRepository;
        public GetCategoryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public Task<List<GetCategoryResponse>> Handle(GetCategoryRequest request, CancellationToken cancellationToken)
        {
            return _categoryRepository.GetCategory(request);
        }
    }
}
