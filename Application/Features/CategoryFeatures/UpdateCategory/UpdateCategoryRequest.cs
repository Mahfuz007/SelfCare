using MediatR;

namespace Application.Features.CategoryFeatures.UpdateCategory
{
    public sealed record UpdateCategoryRequest : IRequest<UpdateCategoryResponse>
    {
        public string ItemId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public bool IsExpense { get; set; }
        public bool IsDefault { get; set; }
    }
}
