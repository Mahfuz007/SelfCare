using AutoMapper;
using Domain.Entities;

namespace Application.Features.ExpenseFeatures.AddExpense
{
    public class AddExpenseMapper : Profile
    {
        public AddExpenseMapper()
        {
            CreateMap<AddExpenseRequest, Expense>();
            CreateMap<Expense, AddExpenseResponse>();
        }
    }
}
