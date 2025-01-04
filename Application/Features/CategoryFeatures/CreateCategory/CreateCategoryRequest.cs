using Application.Common;
using MediatR;

namespace Application.Features.CategoryFeatures.CreateCategory
{
    public sealed record CreateCategoryRequest : IRequest<CommonResponse>
    {
        public string Name { get; set; } = string.Empty;
        public bool IsExpense { get; set; }
        public bool IsDefault { get; set; }
    }
}
