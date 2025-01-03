using Application.Common;
using Application.Features.ExpenseFeatures.AddExpense;
using Application.Features.ExpenseFeatures.GetExpense;
using Application.Features.ExpenseFeatures.GetTotalExpense;
using Application.Features.ExpenseFeatures.UpdateExpense;
using Domain.Entities;

namespace Application.Repositories
{
    public interface IExpenseRepository
    {
        Task<CommonResponse> AddExpense(AddExpenseRequest request);
        Task<CommonResponse> GetExpenses(GetExpenseRequest request);
        Task<bool> CheckIfExpenseExists(string expenseId);
        Task<CommonResponse> UpdateExpense(UpdateExpenseRequest request);
        Task<GetExpenseResponse> GetExpenseById(string expenseId);
        Task<CommonResponse> DeleteExpense(string expenseId);
        Task<CommonResponse> GetTotalExpenseAmount(GetTotalExpenseRequest request);
        Task<CommonResponse> ProcessImportedExpense(IEnumerable<Expense> expenses, string excelName);
    }
}
