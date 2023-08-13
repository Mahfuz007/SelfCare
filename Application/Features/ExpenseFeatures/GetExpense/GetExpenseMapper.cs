using AutoMapper;
using Domain.Entities;

namespace Application.Features.ExpenseFeatures.GetExpense
{
    public class GetExpenseMapper : Profile
    {
        public GetExpenseMapper()
        {
            CreateMap<Expense, GetExpenseResponse>();
        }
    }
}
