using MediatR;

namespace Application.Features.CategoryFeatures.DeleteCategory
{
    public sealed class DeleteCategoryRequest : IRequest<bool>
    {
        public string CategoryId { get; set; }
    }
}
