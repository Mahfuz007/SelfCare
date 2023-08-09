using Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.CategoryFeatures.GetAllCategory
{
    public sealed class GetAllCategoryHandler : IRequestHandler<GetAllCategoryRequest, List<GetAllCategoryResponse>>
    {
        private readonly ICategoryRepository _categoryRepository;
        public GetAllCategoryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<List<GetAllCategoryResponse>> Handle(GetAllCategoryRequest request, CancellationToken cancellationToken)
        {
            return await _categoryRepository.GetAllCategory();
        }
    }
}
