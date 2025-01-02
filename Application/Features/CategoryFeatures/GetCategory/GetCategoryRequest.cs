using Application.Common;
using MediatR;

namespace Application.Features.CategoryFeatures.GetCategory
{
    public sealed class GetCategoryRequest : QueryRequestBase, IRequest<CommonResponse>
    {
        public string? CategoryId { get; set; }
    }
}
