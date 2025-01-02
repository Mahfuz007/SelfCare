using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.ExpenseFeatures.ImportFromExcel
{
    public class ImportExpenseExcelRequest : IRequest<bool>
    {
        public IFormFile? File { get; set; }
    }
}
