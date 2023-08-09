using Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.CategoryFeatures.GetCategory
{
    public sealed class GetCategoryRequest : QueryRequestBase, IRequest<GetCategoryResponse>
    {
        public string CategoryId { get; set; }
    }
}
