using AutoMapper;
using Domain.Entities;

namespace Application.Features.ExpenseFeatures.GetAllExpense
{
    public class GetAllExpenseMapper : Profile
    {
        public GetAllExpenseMapper()
        {
            CreateMap<Expense, GetAllExpenseResponse>();
        }
    }
}
