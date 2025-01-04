using Application.Common;
using MediatR;

namespace Application.Features.CategoryFeatures.DeleteCategory
{
    public sealed class DeleteCategoryRequest : IRequest<CommonResponse>
    {
        public string CategoryId { get; set; } = string.Empty;
    }
}
