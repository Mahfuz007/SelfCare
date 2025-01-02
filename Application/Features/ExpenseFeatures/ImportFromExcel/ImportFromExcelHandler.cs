using Application.Constants;
using Application.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Features.ExpenseFeatures.ImportFromExcel
{
    public class ImportFromExcelHandler : IRequestHandler<ImportExpenseExcelRequest, bool>
    {
        private readonly IExpenseRepository _repository;
        private readonly IExcelReader _excelReader;

        public ImportFromExcelHandler(IExpenseRepository repository, IExcelReader excelReader)
        {
            _repository = repository;
            _excelReader = excelReader;
        }

        public async Task<bool> Handle(ImportExpenseExcelRequest request, CancellationToken cancellationToken)
        {
            if(request == null || request.File == null) return false;
            var fileStream = await _excelReader.GetStremFromFile(request.File);
            var expenses = _excelReader.GetRecords<Expense>(fileStream, ExpenseConstant.PropertyColumnMap, ExpenseConstant.ExpenseExcelFirstIndex);
            var result = await _repository.ProcessImportedExpense(expenses, request.File.FileName);
            return result;
        }
    }
}
