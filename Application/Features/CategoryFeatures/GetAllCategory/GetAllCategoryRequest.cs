using Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.CategoryFeatures.GetAllCategory
{
    public sealed class GetAllCategoryRequest : QueryRequestBase, IRequest<List<GetAllCategoryResponse>>   
    {
    }
}
