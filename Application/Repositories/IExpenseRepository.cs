using Application.Features.ExpenseFeatures.AddExpense;
using Application.Features.ExpenseFeatures.GetAllExpense;
using Application.Features.ExpenseFeatures.GetExpense;
using Application.Features.ExpenseFeatures.UpdateExpense;

namespace Application.Repositories
{
    public interface IExpenseRepository
    {
        Task<AddExpenseResponse> AddExpense(AddExpenseRequest request);
        Task<List<GetAllExpenseResponse>> GetAllExpense();
        Task<bool> CheckIfExpenseExists(string expenseId);
        Task<UpdateExpenseResponse> UpdateExpense(UpdateExpenseRequest request);
        Task<GetExpenseResponse> GetExpenseById(string expenseId);
        Task<bool> DeleteExpense(string expenseId);
    }
}
