using Application.Common;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.ExpenseFeatures.ImportFromExcel
{
    public class ImportExpenseExcelRequest : IRequest<CommonResponse>
    {
        public IFormFile? File { get; set; }
    }
}
