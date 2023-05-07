using MediatR;

namespace Application.Features.UpdateCategory
{
    public sealed record UpdateCategoryRequest : IRequest<UpdateCategoryResponse>
    {
        public string ItemId { get; set; }
        public string Name { get; set; }
        public bool IsExpense { get; set; }
        public bool IsDefault { get; set; }
    }
}
