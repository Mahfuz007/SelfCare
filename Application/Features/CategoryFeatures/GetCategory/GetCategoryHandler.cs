using Application.Common;
using Application.Repositories;
using MediatR;

namespace Application.Features.CategoryFeatures.GetCategory
{
    public sealed class GetCategoryHandler : IRequestHandler<GetCategoryRequest, CommonResponse>
    {
        private readonly ICategoryRepository _categoryRepository;
        public GetCategoryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public Task<CommonResponse> Handle(GetCategoryRequest request, CancellationToken cancellationToken)
        {
            return _categoryRepository.GetCategory(request);
        }
    }
}
