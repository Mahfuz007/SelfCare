using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.CategoryFeatures.CreateCategory
{
    public sealed record CreateCategoryRequest : IRequest<CreateCategoryResponse>
    {
        public string ItemId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public bool IsExpense { get; set; }
        public bool IsDefault { get; set; }
    }
}
